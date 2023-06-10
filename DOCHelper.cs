using System.Windows.Forms;
using Xceed.Words.NET;
using System.Data;

namespace DockFlow
{
    public class DOCHelper
    {
        public const string Symbol1 = "<";
        public const string Symbol2 = ">";

        public void FileDialogDOC()
        {
            using (var file = new OpenFileDialog())
            {
                file.Title = "Выберите документ:";
                file.InitialDirectory = @"%HOMEPATH%";
                file.Filter = "Документ | *.doc*";
                byte[]? readText;

                if (file.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        readText = File.ReadAllBytes(file.FileName);
                        AddDOC(file.SafeFileName, readText);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Файл открыт в другой программе");
                    }
                }
            }
        }

        public void AddDOC(string name, byte[] file)
        {
            var db = new ApplicationContext();
            var sample = new DocumentSample();

            var sampleDOC = db.DocumentSample.ToList().Where(x => x.Name == name);

            if (sampleDOC != null && sampleDOC.Any())
            {
                MessageBox.Show("Документ уже существует");
            }
            else
            {
                using (var fileStream = new FileStream("tempDocs.docx", FileMode.Create, FileAccess.ReadWrite))
                {
                    fileStream.Write(file);
                    using (var DOC = DocX.Load(fileStream))
                    {
                        var txt = DOC.Text;
                        var textSpace = txt.Replace($"{Symbol1}", $" {Symbol1}").Replace($"{Symbol2}", $"{Symbol2} ");
                        var parameterList = textSpace.Split(" ").Where(x => x.StartsWith($"{Symbol1}") && x.EndsWith($"{Symbol2}"));

                        if (parameterList != null && parameterList.Any())
                        {
                            sample.Name = name;
                            sample.File = file;
                            sample.ValueParseDoc = string.Join(",", parameterList);

                            db.DocumentSample.Add(sample);
                            db.SaveChanges();
                            MessageBox.Show($"Файл '{name}' добавлен");
                        }

                        else MessageBox.Show($"Файл '{name}' не имеет параметров");
                    }
                    fileStream.Dispose();
                    File.Delete(fileStream.Name);
                }
            }
        }

        public void ExportDOC(string item)
        {
            var db = new ApplicationContext();
            var currentFile = db.DocumentSample.First();

            using (var file = new SaveFileDialog())
            {
                file.Title = "Сохранить документ:";
                file.InitialDirectory = @"%HOMEPATH%";
                file.Filter = "Документ | *.doc*";
                file.AddExtension = true;
                file.FileName = currentFile.Name;

                if (file.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(file.FileName, currentFile.File);
                }
            }
        }

        /*
        public void DeleteDOC()
        {
            var db = new ApplicationContext();

            if (comboBox1.Text != "")
            {
                var documentSample = db.DocumentSample.ToList().Where(x => x.Name == comboBox1.Text);

                DialogResult result = MessageBox.Show(
                    "Вы действительно хотите удалить?",
                    "Удалить",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    db.DocumentSample.RemoveRange(documentSample);
                    db.SaveChanges();

                    comboBox1.Text = default;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Документ не выбран");
            }
        }

        public void ReplaceAndSaveDOC(int idDOC, int idParameter)
        {
            var db = new ApplicationContext();

            var currentDOC = db.DocumentSample.First(x => x.Id == idDOC);
            var parameters = db.Parameter.ToList().Where(x => x.NameTableId == idParameter);

            using (FileStream fileStream = new FileStream("tempDocs.docx", FileMode.Create, FileAccess.ReadWrite))
            {
                fileStream.Write(currentDOC.File);
                fileStream.Close();
                using (Stream streamFile = File.Open(fileStream.Name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var DOC = DocX.Load(streamFile))
                    {
                        var parameterValueList = new Dictionary<string, string>();
                        var templateParameterList = currentDOC.ValueParseDoc.Split(",");

                        for (var i = 0; i < templateParameterList.Length; i++)
                        {
                            parameterValueList.Add(templateParameterList[i], dataGridView1.Rows[i].Cells[1].Value!.ToString());
                        }

                        foreach (var parameter in parameterValueList)
                        {
                            DOC.ReplaceText(parameter.Key, parameter.Value);
                        }

                        using (var saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Title = "Сохранить документ:";
                            saveFileDialog.Filter = "Документ | *.doc*";
                            saveFileDialog.FileName = currentDOC.Name;
                            saveFileDialog.DefaultExt = "docx";

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                DOC.SaveAs(saveFileDialog.FileName);
                            }
                        }
                    }
                    streamFile.Dispose();
                    File.Delete(fileStream.Name);
                }
            }
        }
        */
    }
}
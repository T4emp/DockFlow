using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualBasic;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace DockFlow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var db = new ApplicationContext();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            var db = new ApplicationContext();
            var documentSample = db.DocumentSample.ToList();

            foreach (var selectedSample in documentSample)
            {
                var item = new ComboboxItem();
                item.Text = selectedSample.Name;

                comboBox1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuDOC.Items.Clear();

            if (comboBox1.Text != "")
            {
                contextMenuDOC.Items.Add("Добавить");
                contextMenuDOC.Items.Add("Экспортировать");
                contextMenuDOC.Items.Add("Удалить");
            }
            else contextMenuDOC.Items.Add("Добавить");

            contextMenuDOC.Show(button1, new Point(0, button1.Height));
        }

        private void contextMenuDOC_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                contextMenuDOC.Hide();
                FileDialog();
            }

            if (e.ClickedItem.Text == "Экспортировать")
            {
                contextMenuDOC.Hide();
                exportDOC();
            }

            if (e.ClickedItem.Text == "Удалить")
            {
                contextMenuDOC.Hide();
                DeleteDOC();
            }
        }

        private void FileDialog()
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

        private void AddDOC(string name, byte[] file)
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
                    using (WordprocessingDocument doc = WordprocessingDocument.Open(fileStream, false))
                    {
                        Body body = doc.MainDocumentPart.Document.Body;
                        var text = body.InnerText;
                        var textSpace = text.Replace("<", " <").Replace(">", "> ");
                        var parameterList = textSpace.Split(" ").Where(x => x.StartsWith("<") && x.EndsWith(">"));
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

        private void exportDOC()
        {
            var db = new ApplicationContext();
            var currentFile = db.DocumentSample.First(x => x.Name == comboBox1.Text);

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

        private void DeleteDOC()
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
                }
            }
            else
            {
                MessageBox.Show("Документ не выбран");
            }
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            var db = new ApplicationContext();
            var nameTable = db.NameTable.ToList();

            foreach (var selectedName in nameTable)
            {
                var item = new ComboboxItem();
                item.Text = selectedName.Name;

                comboBox2.Items.Add(item);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            contextMenuTable.Items.Clear();

            if (comboBox2.Text != "")
            {
                contextMenuTable.Items.Add("Добавить");
                contextMenuTable.Items.Add("Изменить имя");
                contextMenuTable.Items.Add("Изменить данные");
                contextMenuTable.Items.Add("Удалить");
            }
            else
            {
                contextMenuTable.Items.Add("Добавить");
            }

            contextMenuTable.Show(button2, new Point(0, button2.Height));
        }

        private void contextMenuTable_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                contextMenuTable.Hide();
                AddTable();
            }

            if (e.ClickedItem.Text == "Изменить имя")
            {
                contextMenuTable.Hide();
                EditTableName();
            }

            if (e.ClickedItem.Text == "Изменить данные")
            {
                contextMenuTable.Hide();
                EditTableData();
            }

            if (e.ClickedItem.Text == "Удалить")
            {
                contextMenuTable.Hide();
                DeleteTable();
            }
        }

        private void AddTable()
        {
            var db = new ApplicationContext();
            var nameTable = new NameTable();

            var text1 = "Название таблицы";
            var text2 = "Добавить";

            do
            {
                nameTable.Name = Interaction.InputBox($"{text1}", $"{text2}", "");

                var nameTableName = db.NameTable.ToList().Where(x => x.Name == nameTable.Name);

                if (nameTable.Name != "")
                {
                    if (nameTableName != null && nameTableName.Any())
                    {
                        MessageBox.Show("Таблица уже существует");
                    }
                    else
                    {
                        db.NameTable.Add(nameTable);
                        db.SaveChanges();
                        nameTable.Name = "";
                    }
                }
            }
            while (nameTable.Name != "");
        }

        private void EditTableName()
        {
            var db = new ApplicationContext();

            if (comboBox2.Text != "")
            {
                var currentTableName = db.NameTable.First(x => x.Name == comboBox2.Text).Name;

                var text1 = "Название таблицы";
                var text2 = "Изменить";

                string newName = Interaction.InputBox($"{text1}", $"{text2}", $"{currentTableName}");
                if (newName != "")
                {
                    db.Update(currentTableName = newName);
                    db.SaveChanges();

                    comboBox2.Text = default;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Таблица не выбрана");
            }
        }

        private void EditTableData()
        {
            Form2 form = new Form2($"{comboBox2.Text}");
            form.ShowDialog();
        }

        private void DeleteTable()
        {
            var db = new ApplicationContext();

            if (comboBox2.Text != "")
            {
                var currentTableName = db.NameTable.First(x => x.Name == comboBox2.Text);

                DialogResult result = MessageBox.Show(
                    "Вы действительно хотите удалить?",
                    "Удалить",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    db.NameTable.RemoveRange(currentTableName);
                    db.SaveChanges();

                    comboBox2.Text = default;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Таблица не выбрана");
            }
        }

        private void loadDataGrid()
        {
            var db = new ApplicationContext();

            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Параметр в документе"));
            dataTable.Columns.Add(new DataColumn("Значение параметра"));

            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox2.Text))
            {
                var currentDOC = db.DocumentSample.First(x => x.Name == comboBox1.Text);

                var currentNameTable = db.NameTable.First(x => x.Name == comboBox2.Text);
                var currentParameters = db.Parameter.Where(x => x.NameTableId == currentNameTable.Id).ToList();

                foreach (var param in currentDOC.ValueParseDoc.Split(","))
                {
                    var clearParamValue = param.Replace(">", string.Empty).Replace("<", string.Empty);

                    var paramValue = currentParameters.FirstOrDefault(x => x.Name.ToLower() == clearParamValue.ToLower());
                    if (paramValue != null)
                    {
                        dataTable.Rows.Add(paramValue.Name, paramValue.Value);
                    }
                }

                dataGridView1.DataSource = dataTable;

                refreshDataGrid();
            }
        }

        private void refreshDataGrid()
        {
            var headerRow = dataGridView1.RowHeadersWidth / 2;
            dataGridView1.Columns[0].Width = dataGridView1.Width / 2 - headerRow;
            dataGridView1.Columns[1].Width = dataGridView1.Width / 2 - headerRow;
        }

        public void replaceAndSaveDOC(int idDOC, int idParameter)
        {
            var db = new ApplicationContext();

            var DOCS = db.DocumentSample.ToList().Where(x => x.Id == idDOC);
            var DOC = DOCS.FirstOrDefault().File;

            var parameters = db.Parameter.ToList().Where(x => x.NameTableId == idParameter);

            using FileStream fileStream = new FileStream("tempDocs.docx", FileMode.Create, FileAccess.ReadWrite);

            fileStream.Write(DOC);
            fileStream.Close();

            using Stream streamFile = File.Open(fileStream.Name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using WordprocessingDocument doc = WordprocessingDocument.Open(streamFile, false);
            WordprocessingDocument reportDOC = (WordprocessingDocument)doc.Clone();
            reportDOC.ChangeDocumentType(WordprocessingDocumentType.Document);

            var body = reportDOC.MainDocumentPart!.Document.Body;

            foreach (var text in body.Descendants<Text>())
            {
                foreach (var parameter in parameters)
                {
                    text.Text = text.Text.Replace(parameter.Name, parameter.Value);
                }
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = Guid.NewGuid().ToString();
                saveFileDialog.DefaultExt = "docx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    WordprocessingDocument outputDOC = (WordprocessingDocument)reportDOC.SaveAs(saveFileDialog.FileName);
                    outputDOC.Close();
                }
            }

            reportDOC.Close();
            streamFile.Close();

            File.Delete(fileStream.Name);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            var db = new ApplicationContext();

            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                var documentSample = db.DocumentSample.ToList().Where(x => x.Name == $"{comboBox1.Text}");
                int currentDOC = documentSample.FirstOrDefault().Id;

                var nameTable = db.NameTable.ToList().Where(x => x.Name == $"{comboBox2.Text}");
                int currentNameTable = nameTable.FirstOrDefault().Id;

                replaceAndSaveDOC(currentDOC, currentNameTable);
            }
            else
            {
                MessageBox.Show($"Document or table not selected");
            }
        }

        public class ComboboxItem
        {
            public string? Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataGrid();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataGrid();
        }
    }
}
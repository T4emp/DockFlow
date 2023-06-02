using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.VisualBasic;
using System.Data;
using Color = System.Drawing.Color;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace DockFlow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label1.Text = Localization.Translate("Home");
            label2.Text = Localization.Translate("Data");
            label3.Text = Localization.Translate("Upload");

            label4.Text = Localization.Translate("Save");
            label5.Text = Localization.Translate("Choose DOC:");
            label6.Text = Localization.Translate("Choose table:");

            button1.Text = Localization.Translate("Delete");

            toolStripMenuItem1.Text = Localization.Translate("Tools");

            addToolStripMenuItem.Text = Localization.Translate("Add");
            editToolStripMenuItem.Text = Localization.Translate("Edit");
            deleteToolStripMenuItem.Text = Localization.Translate("Delete");

            panel5.Dock = DockStyle.Fill;
            panel5.Visible = false;

            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Visible = false;

            var db = new ApplicationContext();
        }

        //Label Add file to DB
        private void panel3_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = Localization.Translate("Choose DOC:");
            file.InitialDirectory = @"%HOMEPATH%";
            file.Filter = "DOC | *.doc*";
            byte[]? readText;

            if (file.ShowDialog() == DialogResult.OK)
            {
                readText = File.ReadAllBytes(file.FileName);
                AddDOC(file.SafeFileName, readText);
            }
        }

        //Add DOCX
        public void AddDOC(string name, byte[] file)
        {
            var db = new ApplicationContext();
            var sample = new DocumentSample();

            var sampleDOC = db.DocumentSample.ToList().Where(x => x.Name == name);

            if (sampleDOC != null && sampleDOC.Any())
            {
                MessageBox.Show($"{Localization.Translate("DOCExists")}");
            }
            else
            {
                using (var fileStream = new FileStream("tempDocs.docx", FileMode.Create, FileAccess.ReadWrite))
                {
                    fileStream.Write(file);
                    {
                        sample.Name = name;
                        sample.File = file;

                        db.DocumentSample.Add(sample);
                        db.SaveChanges();
                        MessageBox.Show($"'{name}', {Localization.Translate("file added successfully")}");
                    }
                    fileStream.Close();
                    File.Delete(fileStream.Name);
                }
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

        //Label2 Add NameTable
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var db = new ApplicationContext();
            var nameTable = new NameTable();

            var text1 = Localization.Translate("Title for the table");
            var text2 = Localization.Translate("Add");

            do
            {
                nameTable.Name = Interaction.InputBox($"{text1}", $"{text2}", "");

                var nameTableName = db.NameTable.ToList().Where(x => x.Name == nameTable.Name);

                if (nameTable.Name != "")
                {
                    if (nameTableName != null && nameTableName.Any())
                    {
                        MessageBox.Show($"{Localization.Translate("TableExists")}");
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

        //Label2 choose table from DB "NameTable"
        private void toolStripComboBox1_DropDown(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Clear();

            var db = new ApplicationContext();
            var nameTables = db.NameTable.ToList();

            foreach (var selectedSample in nameTables)
            {
                var item = new ComboboxItem();
                item.Text = selectedSample.Name;

                toolStripComboBox1.Items.Add(item);
            }
        }

        //Label2 Edit NameTable
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var db = new ApplicationContext();
            var nameTable = new NameTable();

            if (toolStripComboBox1.Text != "")
            {
                var listLine = db.NameTable.ToList().Where(x => x.Name == $"{toolStripComboBox1.Text}");
                string currentValue = listLine.FirstOrDefault().Name;

                var text1 = Localization.Translate("Title for the table");
                var text2 = Localization.Translate("Add");

                string newName = Interaction.InputBox($"{text1}", $"{text2}", $"{currentValue}");
                if (newName != "")
                {
                    foreach (var item in listLine)
                    {
                        item.Name = newName;
                        db.Update(item);
                    }
                    db.SaveChanges();

                    toolStripComboBox1.Text = default;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Refresh();
                }
            }
            else
            {
                MessageBox.Show($"{Localization.Translate("NoSelect")}");
            }
        }

        //Label2 Delete NameTable
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var db = new ApplicationContext();

            if (toolStripComboBox1.Text != "")
            {
                var listLine = db.NameTable.ToList().Where(x => x.Name == $"{toolStripComboBox1.Text}");
                string currentValue = listLine.FirstOrDefault().Name;

                DialogResult result = MessageBox.Show(
                    Localization.Translate("Check: delete?"),
                    Localization.Translate("Delete"),
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    db.NameTable.RemoveRange(listLine);
                    db.SaveChanges();

                    toolStripComboBox1.Text = default;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Refresh();
                }
            }
            else
            {
                MessageBox.Show($"{Localization.Translate("NoSelect")}");
            }
        }

        //dataGrid from NameTable from Id
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text != "")
            {
                var db = new ApplicationContext();

                var nameTable = db.NameTable.ToList().Where(x => x.Name == $"{toolStripComboBox1.Text}");
                var parameters = db.Parameter.ToList().Where(x => x.NameTableId == nameTable.FirstOrDefault().Id);

                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Parameter Name");
                dataTable.Columns.Add("Parameter Value");

                DataRow dataRow = dataTable.NewRow();

                foreach (var parameter in parameters)
                {
                    dataTable.Rows.Add(parameter.Name, parameter.Value);
                }
                dataGridView1.DataSource = dataTable;
            }
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

        private void panel6_Click(object sender, EventArgs e)
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
                MessageBox.Show($"{Localization.Translate("Document or table not selected")}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var db = new ApplicationContext();

            if (comboBox1.Text != "")
            {
                var documentSample = db.DocumentSample.ToList().Where(x => x.Name == comboBox1.Text);

                DialogResult result = MessageBox.Show(
                    Localization.Translate("Check: delete?"),
                    Localization.Translate("Delete"),
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
                MessageBox.Show($"{Localization.Translate("NoSelect")}");
            }
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var db = new ApplicationContext();
            var parameter = new Parameter();

            if (toolStripComboBox1.Text != "")
            {
                var currentNameTable = db.NameTable.ToList().Where(x => x.Name == $"{toolStripComboBox1.Text}");
                int nameTableId = currentNameTable.FirstOrDefault().Id;

                var currentParameterId = db.Parameter.ToList().Where(x => x.NameTableId == nameTableId);

                db.Parameter.RemoveRange(currentParameterId);

                for (var j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    var cells1 = dataGridView1.Rows[j].Cells[0].Value.ToString();
                    var cells2 = dataGridView1.Rows[j].Cells[1].Value.ToString();

                    parameter.Id = Guid.NewGuid().ToString();
                    parameter.NameTableId = nameTableId;
                    parameter.Name = cells1;
                    parameter.Value = cells2;

                    db.AddRange(parameter);
                    db.SaveChanges();
                }
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(18, 65, 121);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(43, 86, 154);
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(18, 65, 121);
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(43, 86, 154);
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(18, 65, 121);
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(43, 86, 154);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel3.Visible == true)
            {
                tableLayoutPanel3.Visible = false;
                panel5.Visible = false;
            }
            else
            {
                tableLayoutPanel3.Visible = true;
                panel5.Visible = false;
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
                tableLayoutPanel3.Visible = false;
            }
            else
            {
                panel5.Visible = true;
                tableLayoutPanel3.Visible = false;
            }
        }
    }
}
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Data;

namespace DockFlow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //var culture = System.Globalization.CultureInfo.CurrentUICulture;
        }

        public void sendToDBDocument(string Name, int IdDocTemp)
        {
            var newDoc = new Document();
            var db = new ApplicationContext();
            newDoc.Name = Name;
            newDoc.DocumentTemplateId = IdDocTemp;
            db.Document.Add(newDoc);
            db.SaveChanges();
        }

        public class ComboboxItem
        {
            public string? Text { get; set; }
            public object? Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Choose doc";
            file.InitialDirectory = @"%HOMEPATH%";
            file.Filter = "DOC | *.doc*";
            byte[] readText = null;
            if (file.ShowDialog() == DialogResult.OK)
            {
                readText = File.ReadAllBytes(file.FileName);
                label3.Text = $"{file.FileName}";
            }

            var db = new ApplicationContext();
            var newTep = new DocumentTemplate();
            var parameter = new Parameter();

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(file.FileName, true))
            {
                Body body = wordDoc.MainDocumentPart.Document.Body;
                var text = body.InnerText;
                var textSpace1 = text.Replace("<", " <");
                var textSpace2 = text.Replace(">", "> ");
                var parameterList = textSpace2.Split(" ").Where(x => x.StartsWith("<") && x.EndsWith(">"));

                newTep.Name = file.SafeFileName;
                newTep.File = readText;
                newTep.ParameterNames = string.Join(",", parameterList);

                db.DocumentTemplate.Add(newTep);
                db.SaveChanges();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedIndex = (Int32.Parse(comboBox1.GetItemText(comboBox1.SelectedIndex)) + 1);
            sendToDBDocument(textBox1.Text, selectedIndex);
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            var db = new ApplicationContext();
            var templates = db.DocumentTemplate.ToList();

            foreach (var template in templates)
            {
                var item = new ComboboxItem();
                item.Text = template.Name;
                item.Value = template.Id;

                comboBox1.Items.Add(item);
            }
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            var db = new ApplicationContext();
            var templates = db.DocumentTemplate.ToList();

            foreach (var template in templates)
            {
                var item = new ComboboxItem();
                item.Text = template.Name;
                item.Value = template.Id;

                comboBox2.Items.Add(item);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIndex = (Int32.Parse(comboBox2.GetItemText(comboBox2.SelectedIndex)) + 1);

            var db = new ApplicationContext();
            var templates = db.DocumentTemplate.ToList().Where(x => x.Id == selectedIndex);

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ValueDOC", typeof(string));
            dataTable.Columns.Add("Value", typeof(string));

            foreach (var template in templates)
            {
                string[] obj =  template.ParameterNames.Split(",");
                foreach (var item in obj)
                {
                    dataTable.Rows.Add(item);
                }
            }
            dataGridView1.DataSource = dataTable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
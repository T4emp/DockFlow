using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Выберите документ";
            file.InitialDirectory = @"%HOMEPATH%";
            file.Filter = "Документ | *.doc*";
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

        private void button6_Click(object sender, EventArgs e)
        {
            var selectedIndex = (Int32.Parse(comboBox1.GetItemText(comboBox1.SelectedIndex)) + 1);
            sendToDBDocument(textBox1.Text, selectedIndex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        public class ComboboxItem
        {
            public string? Text { get; set; }
            public object? Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
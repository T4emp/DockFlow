namespace DockFlow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var culture = System.Globalization.CultureInfo.CurrentUICulture;
        }

        public void sendToDBDocumentTemplate(string Name, byte[] File)
        {
            var newTep = new DocumentTemplate();
            var db = new ApplicationContext();
            newTep.Name = $"{Name}";
            newTep.File = File;
            db.DocumentTemplate.Add(newTep);
            db.SaveChanges();
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
            if (file.ShowDialog() == DialogResult.OK)
            {
                byte[] readText = File.ReadAllBytes(file.FileName);
                sendToDBDocumentTemplate(file.SafeFileName, readText);
                label3.Text = $"{file.FileName}";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var selectedIndex =  (Int32.Parse(comboBox2.GetItemText(comboBox2.SelectedIndex)) + 1);
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

                comboBox2.Items.Add(item);
            }
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
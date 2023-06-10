using DockFlow.Properties;

namespace DockFlow
{
    public partial class Form1 : Form
    {
        DOCHelper doc = new DOCHelper();
        TableHelper table = new TableHelper();

        public Form1()
        {
            InitializeComponent();
            var db = new ApplicationContext();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc.FileDialogDOC();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParseItemView(listView1);

            //doc.ExportDOC();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //doc.DeleteDOC();
        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table.AddTable();
        }

        private void editTableNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //table.EditTableName();
        }

        private void deleteTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //table.DeleteTable();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //doc.ReplaceAndSaveDOC();
        }

        private void UpdateListViewDOC()
        {
            var db = new ApplicationContext();
            var DOCS = db.DocumentSample.ToList();

            foreach (var doc in DOCS)
            {

            }
        }

        private void UpdateListViewTable()
        {
            var db = new ApplicationContext();
            var parameters = db.Parameter.ToList();

            foreach (var parameter in parameters)
            {
                ListViewItem item = new ListViewItem();
                item.Text = parameter.Value;

                listView1.Items.Add(item);
            }
        }

        public static string ParseItemView(ListView list)
        {
            ListView.SelectedListViewItemCollection lists = list.SelectedItems;
            string value = null;

            foreach (ListViewItem item in lists)
            {
                value = item.SubItems[1].Text;
            }
            return value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateListViewDOC();
            UpdateListViewTable();
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            findTextEnter();
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            findTextLeave();
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            findTextEnter();
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            findTextLeave();
        }

        private void findTextEnter()
        {
            if (textBox1.Text == "Поиск" && textBox2.Text == "Поиск")
            {
                textBox1.ForeColor = Color.Black;
                textBox2.ForeColor = Color.Black;

                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void findTextLeave()
        {
            if (textBox1.Text == "Поиск" && textBox2.Text == "Поиск")
            {
                textBox1.ForeColor = Color.Silver;
                textBox2.ForeColor = Color.Silver;

                textBox1.Text = "Поиск";
                textBox2.Text = "Поиск";
            }
        }
    }
}
namespace DockFlow
{
    public partial class Form1 : Form
    {
        DOCHelper doc = new DOCHelper();
        TableHelper table = new TableHelper();
        dataGrid grid = new dataGrid();

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

            doc.ExportDOC(listView1.FocusedItem.Text);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc.DeleteDOC(listView1.FocusedItem.Text);
        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table.AddTable();
        }

        private void editTableNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table.EditTableName(listView2.FocusedItem.Text);
        }

        private void deleteTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table.DeleteTable(listView2.FocusedItem.Text);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc.ReplaceAndSaveDOC();
        }

        private void listViewRefresh()
        {
            var db = new ApplicationContext();
            var DocumentSample = db.DocumentSample.ToList();
            var NameTable = db.NameTable.ToList();

            foreach (var doc in DocumentSample)
            {
                var name = doc.Name;
                listView1.Items.Add(name);
            }

            foreach (var table in NameTable)
            {
                var name = table.Name;
                listView2.Items.Add(name);
            }
        }

        public void refreshDataGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listViewRefresh();
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
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                textBox1.ForeColor = Color.Silver;
                textBox2.ForeColor = Color.Silver;

                textBox1.Text = "Поиск";
                textBox2.Text = "Поиск";
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null && listView2.FocusedItem != null)
                dataGridView1.DataSource = grid.loadDataGrid(listView1.FocusedItem.Text, listView2.FocusedItem.Text);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
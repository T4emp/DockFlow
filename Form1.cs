using System.Diagnostics;

namespace DockFlow
{
    public partial class Form1 : Form
    {
        public const string textFind = "Поиск";
        public bool checkView = false;

        TableHelper table = new TableHelper();

        public Form1()
        {
            InitializeComponent();
            var db = new ApplicationContext();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DOCHelper doc = new DOCHelper(dataGridView1);
            doc.fileDialogDOC();
            listViewRefresh(true);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                DOCHelper doc = new DOCHelper(dataGridView1);
                doc.exportDOC(listView1.FocusedItem.Text);
            }
            else MessageBox.Show("Шаблон не выбран");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                DOCHelper doc = new DOCHelper(dataGridView1);
                doc.deleteDOC(listView1.FocusedItem.Text);
                listViewRefresh(true);
                dataGridRefresh();
            }
            else MessageBox.Show("Шаблон не выбран");
        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table.addTable();
            listViewRefresh(true);
        }

        private void editTableNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView2.FocusedItem != null)
            {
                table.editTableName(listView2.FocusedItem.Text);
                listViewRefresh(true);
                dataGridRefresh();
            }
            else MessageBox.Show("Таблица не выбрана");
        }

        private void deleteTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView2.FocusedItem != null)
            {
                table.deleteTable(listView2.FocusedItem.Text);
                listViewRefresh(true);
                dataGridRefresh();
            }
            else MessageBox.Show("Таблица не выбрана");
        }

        [Obsolete]
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DOCHelper doc = new DOCHelper(dataGridView1);
            if (listView1.FocusedItem != null && listView2.FocusedItem != null)
                doc.replaceAndSaveDOC(listView1.FocusedItem.Text, listView2.FocusedItem.Text);
            else MessageBox.Show("Шаблон или таблица не выбрана");
        }

        private void listViewRefresh(bool list)
        {
            var db = new ApplicationContext();
            var DocumentSample = db.DocumentSample.ToList();
            var NameTable = db.NameTable.ToList();

            if (list == true)
            {
                listView1.Items.Clear();
                listView2.Items.Clear();

                foreach (var doc in DocumentSample)
                {
                    listView1.Items.Add(doc.Name);
                }

                foreach (var table in NameTable)
                {
                    listView2.Items.Add(table.Name);
                }
            }

            else
            {
                listView1.Items.Clear();

                foreach (var doc in DocumentSample)
                {
                    listView1.Items.Add(doc.Name);
                }
            }
            resizeAll();
        }

        public void dataGridRefresh()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            resizeAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = textFind;
            textBox2.Text = textFind;

            textBox1.ForeColor = Color.Silver;
            textBox2.ForeColor = Color.Silver;

            listViewRefresh(true);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridRefresh();
            dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);
            if (listView1.FocusedItem != null)
            {
                if (listView2.FocusedItem != null)
                    dataGridView1.DataSource = grid.loadDataGridSampleAndParameter();
                else dataGridView1.DataSource = grid.loadDataGridSample();
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkView = false;
            dataGridRefresh();
            dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);
            if (listView2.FocusedItem != null)
                if (listView1.FocusedItem != null)
                    dataGridView1.DataSource = grid.loadDataGridSampleAndParameter();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);
            if (listView1.FocusedItem == null)
                grid.saveChangedDataGridParameter();
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            checkView = false;
            checkView = true;
            dataGridRefresh();
            dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);
            listViewRefresh(false);
            dataGridView1.DataSource = grid.loadDataGridParameter();
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            resizeAll();
        }

        private void resizeAll()
        {
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView2.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"DockFlow.pdf")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (checkView == true)
            {
                dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);
                grid.saveChangedDataGridParameter();
            }
        }
    }
}
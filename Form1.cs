using DockFlow.Properties;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DockFlow
{
    public partial class Form1 : Form
    {
        public const string textFind = "Поиск";
        public bool list2Selected = false;
        private readonly ApplicationContext _db;

        ObjectHelper @object = new ObjectHelper();

        private readonly ApplicationContext _context;

        public Form1()
        {
            InitializeComponent();
            _db = new ApplicationContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonToolStripMenuItem.Visible = false;
            listView();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DOCHelper doc = new DOCHelper(dataGridView1);
            doc.fileDialogDOC();
            listView();
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
                listView();
                dataGrid();
            }
            else MessageBox.Show("Шаблон не выбран");
        }

        private void createObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            @object.addObject();
            listView();
        }

        private void duplicateObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView2.FocusedItem != null)
            {
                @object.duplicateObject(listView2.FocusedItem.Text);
                listView();
            }
            else MessageBox.Show("Объект не выбран");
        }

        private void editObjectNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView2.FocusedItem != null)
            {
                @object.editObjectName(listView2.FocusedItem.Text);
                listView();
                dataGrid();
            }
            else MessageBox.Show("Объект не выбран");
        }

        private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView2.FocusedItem != null)
            {
                @object.deleteOject(listView2.FocusedItem.Text);
                listView();
                dataGrid();
            }
            else MessageBox.Show("Объект не выбран");
        }

        [Obsolete]
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DOCHelper doc = new DOCHelper(dataGridView1);
            if (listView1.FocusedItem != null && listView2.FocusedItem != null)
                doc.replaceAndSaveDOC(listView1.FocusedItem.Text, listView2.FocusedItem.Text);
            else MessageBox.Show("Шаблон или объект не выбран");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("DockFlow.pdf", FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(Resources.DockFlow);
                fs.Close();
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(fs.Name)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
        }

        void listView()
        {
            var db = new ApplicationContext();
            var DocumentSample = db.DocumentSample.ToList();
            var Object = db.Object.ToList();

            listView1.Items.Clear();
            listView2.Items.Clear();

            foreach (var doc in DocumentSample)
            {
                listView1.Items.Add(doc.Name);
            }

            foreach (var obj in Object)
            {
                listView2.Items.Add(obj.Name);
            }

            buttonToolStripMenuItem.Visible = false;
            resizeAll();
        }

        void dataGrid()
        {
            var list1 = listView1.FocusedItem;
            var list2 = listView2.FocusedItem;

            dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);

            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();

            if (list1 != null)
            {
                list2Selected = false;

                buttonToolStripMenuItem.Visible = false;

                dataGridView1.DataSource = grid.loadDataGridSample();
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;

                if (list2 != null)
                {
                    buttonToolStripMenuItem.Visible = true;

                    dataGridView1.DataSource = grid.loadDataGridSampleAndParameter();
                }
            }
            if (list2 != null)
                buttonToolStripMenuItem.Visible = true;

            resizeAll();
        }

        private void resizeAll()
        {
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView2.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            list2Selected = false;
            dataGrid();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonToolStripMenuItem.Visible = true;
            list2Selected = false;
            dataGrid();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);
            if (list2Selected == true)
                grid.saveChangedDataGridParameter();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            resizeAll();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (list2Selected == true)
            {
                dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);
                grid.saveChangedDataGridParameter();
            }
        }

        private void buttonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGrid grid = new dataGrid(dataGridView1, listView1, listView2);

            dataGrid();

            list2Selected = true;
            dataGridView1.DataSource = grid.loadDataGridParameter();
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var searchText = textBox1.Text.ToLower();

            var templates = _db.DocumentSample.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{searchText}%")).ToList();

            listView1.Items.Clear();

            foreach (var doc in templates)
            {
                listView1.Items.Add(doc.Name);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var searchText = textBox2.Text.ToLower();

            var templates = _db.Object.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{searchText}%")).ToList();

            listView2.Items.Clear();

            foreach (var doc in templates)
            {
                listView2.Items.Add(doc.Name);
            }
        }

        private void templateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list2Selected = false;
        }

        private void objectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list2Selected = false;
        }

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Delete)
                deleteObjectToolStripMenuItem_Click(sender, e);
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Delete)
                deleteToolStripMenuItem_Click(sender, e);
        }
    }
}
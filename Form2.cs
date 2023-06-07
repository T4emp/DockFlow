using System.Data;

namespace DockFlow
{
    public partial class Form2 : Form
    {
        public string name;

        public Form2(string nameTable)
        {
            InitializeComponent();
            name = nameTable;
            loadDataGrid();
        }

        private void loadDataGrid()
        {
            var db = new ApplicationContext();

            var currentNameTable = db.NameTable.ToList().Where(x => x.Name == name);
            var currentParameters = db.Parameter.ToList().Where(x => x.NameTable.Id == currentNameTable.FirstOrDefault().Id);

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Название параметра");
            dataTable.Columns.Add("Значение параметра");

            DataRow dataRow = dataTable.NewRow();

            foreach (var parameter in currentParameters)
            {
                dataTable.Rows.Add(parameter.Name, parameter.Value);
            }
            dataGridView2.DataSource = dataTable;
            refreshDataGrid();
        }

        private void changeDataGrid()
        {
            var db = new ApplicationContext();
            var parameter = new Parameter();

            var currentNameTable = db.NameTable.ToList().Where(x => x.Name == name);
            var currentParameter = db.Parameter.ToList().Where(x => x.NameTable.Id == currentNameTable.FirstOrDefault().Id);

            db.Parameter.RemoveRange(currentParameter);

            for (var i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                var cells1 = dataGridView2.Rows[i].Cells[0].Value.ToString();
                var cells2 = dataGridView2.Rows[i].Cells[1].Value.ToString();

                parameter.Id = Guid.NewGuid();
                parameter.NameTableId = currentNameTable.FirstOrDefault().Id;
                parameter.Name = cells1;
                parameter.Value = cells2;

                db.AddRange(parameter);
                db.SaveChanges();
            }
        }

        private void refreshDataGrid()
        {
            var headerRow = dataGridView2.RowHeadersWidth / 2;
            dataGridView2.Columns[0].Width = dataGridView2.Width / 2 - headerRow;
            dataGridView2.Columns[1].Width = dataGridView2.Width / 2 - headerRow;
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            changeDataGrid();
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            changeDataGrid();
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            refreshDataGrid();
        }
    }
}
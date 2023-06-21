using System.Data;

namespace DockFlow
{
    public class dataGrid
    {
        public const string Symbol1 = "<";
        public const string Symbol2 = ">";

        public DataGridView DataGridView;
        public ListView ListViewSample;
        public ListView ListViewTable;

        public dataGrid(DataGridView dataGridView, ListView list1, ListView list2)
        {
            DataGridView = dataGridView;
            ListViewSample = list1;
            ListViewTable = list2;
        }

        public dataGrid(DataGridView dataGridView)
        {
            DataGridView = dataGridView;
        }

        public DataTable loadDataGridSample()
        {
            var db = new ApplicationContext();

            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Параметр в документе"));
            dataTable.Columns.Add(new DataColumn("Значение параметра"));

            var currentSample = db.DocumentSample.First(x => x.Name == ListViewSample.FocusedItem.Text);

            foreach (var item in currentSample.ValueParseDoc.Split(","))
            {
                var clearParamValue = item.Replace($"{Symbol1}", string.Empty).Replace($"{Symbol2}", string.Empty);
                dataTable.Rows.Add(clearParamValue);
            }

            return dataTable;
        }

        public DataTable loadDataGridParameter()
        {
            var db = new ApplicationContext();

            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Параметр"));
            dataTable.Columns.Add(new DataColumn("Значение параметра"));

            var currentObject = db.Object.First(x => x.Name == ListViewTable.FocusedItem.Text);
            var currentParameters = db.Parameter.Where(x => x.ObjectId == currentObject.Id);

            foreach (var item in currentParameters)
            {
                dataTable.Rows.Add(item.Name, item.Value);
            }

            return dataTable;
        }

        public DataTable loadDataGridSampleAndParameter()
        {
            var db = new ApplicationContext();

            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Параметр"));
            dataTable.Columns.Add(new DataColumn("Значение параметра"));

            var currentSample = db.DocumentSample.First(x => x.Name == ListViewSample.FocusedItem.Text);
            var currentObject = db.Object.First(x => x.Name == ListViewTable.FocusedItem.Text);
            var currentParameters = db.Parameter.Where(x => x.ObjectId == currentObject.Id).ToList();

            foreach (var item in currentSample.ValueParseDoc.Split(","))
            {
                var clearParamValue = item.Replace($"{Symbol1}", string.Empty).Replace($"{Symbol2}", string.Empty);
                var paramValue = currentParameters.FirstOrDefault(x => x.Name.ToLower() == clearParamValue.ToLower());

                dataTable.Rows.Add(clearParamValue, paramValue?.Value ?? string.Empty);
            }

            return dataTable;
        }

        public void saveChangedDataGridParameter()
        {
            var db = new ApplicationContext();
            var parameter = new Parameter();

            var currentObject = db.Object.First(x => x.Name == ListViewTable.FocusedItem.Text);
            var parameters = db.Parameter.ToList().Where(x => x.ObjectId == currentObject.Id);

            db.Parameter.RemoveRange(parameters);

            for (var i = 0; i < DataGridView.RowCount - 1; i++)
            {
                var cells1 = DataGridView.Rows[i].Cells[0].Value.ToString();
                var cells2 = DataGridView.Rows[i].Cells[1].Value.ToString();

                parameter.Id = Guid.NewGuid();
                parameter.ObjectId = currentObject.Id;
                parameter.Name = cells1;
                parameter.Value = cells2;

                db.AddRange(parameter);
                db.SaveChanges();
            }
        }
    }
}
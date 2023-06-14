using System.Data;

namespace DockFlow
{
    public class dataGrid
    {
        public const string Symbol1 = "<";
        public const string Symbol2 = ">";
        public Form1 form;

        public DataTable loadDataGrid(string item, string item2)
        {
            var db = new ApplicationContext();

            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Параметр в документе"));
            dataTable.Columns.Add(new DataColumn("Значение параметра"));

            var currentDOC = db.DocumentSample.First(x => x.Name == item);

            var currentNameTable = db.NameTable.First(x => x.Name == item2);
            var currentParameters = db.Parameter.Where(x => x.NameTableId == currentNameTable.Id).ToList();

            foreach (var param in currentDOC.ValueParseDoc.Split(","))
            {
                var clearParamValue = param.Replace($"{Symbol1}", string.Empty).Replace($"{Symbol2}", string.Empty);
                var paramValue = currentParameters.FirstOrDefault(x => x.Name.ToLower() == clearParamValue.ToLower());

                dataTable.Rows.Add(clearParamValue, paramValue?.Value ?? string.Empty);
            }

            return dataTable;
        }

        public void changeDataGrid(string item)
        {
            var db = new ApplicationContext();
            var parameter = new Parameter();

            var currentNameTable = db.NameTable.ToList().Where(x => x.Name == item);
            var currentParameter = db.Parameter.ToList().Where(x => x.NameTable.Id == currentNameTable.FirstOrDefault().Id);
            db.Parameter.RemoveRange(currentParameter);

            for (var i = 0; i < form.dataGridView1.RowCount - 1; i++)
            {
                var cells1 = form.dataGridView1.Rows[i].Cells[0].Value.ToString();
                var cells2 = form.dataGridView1.Rows[i].Cells[1].Value.ToString();

                parameter.Id = Guid.NewGuid();
                parameter.NameTableId = currentNameTable.FirstOrDefault().Id;
                parameter.Name = cells1;
                parameter.Value = cells2;

                db.AddRange(parameter);
                db.SaveChanges();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;

namespace DockFlow
{
    public class dataGrid
    {/*
        private void loadDataGrid()
        {
            var db = new ApplicationContext();

            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Параметр в документе"));
            dataTable.Columns.Add(new DataColumn("Значение параметра"));

            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox2.Text))
            {
                var currentDOC = db.DocumentSample.First(x => x.Name == comboBox1.Text);

                var currentNameTable = db.NameTable.First(x => x.Name == comboBox2.Text);
                var currentParameters = db.Parameter.Where(x => x.NameTableId == currentNameTable.Id).ToList();

                foreach (var param in currentDOC.ValueParseDoc.Split(","))
                {
                    var clearParamValue = param.Replace($"{Symbol1}", string.Empty).Replace($"{Symbol2}", string.Empty);
                    var paramValue = currentParameters.FirstOrDefault(x => x.Name.ToLower() == clearParamValue.ToLower());

                    dataTable.Rows.Add(clearParamValue, paramValue?.Value ?? string.Empty);
                }

                dataGridView1.DataSource = dataTable;
            }
        }
    }*/
    }
}

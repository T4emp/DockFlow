using Microsoft.VisualBasic;

namespace DockFlow
{
    public class TableHelper
    {
        public void addTable()
        {
            var db = new ApplicationContext();
            var nameTable = new NameTable();

            var text1 = "Название таблицы";
            var text2 = "Добавить";

            do
            {
                nameTable.Name = Interaction.InputBox($"{text1}", $"{text2}", "");

                var nameTableName = db.NameTable.ToList().Where(x => x.Name == nameTable.Name);

                if (nameTable.Name != "")
                {
                    if (nameTableName != null && nameTableName.Any())
                    {
                        MessageBox.Show("Таблица уже существует");
                    }
                    else
                    {
                        db.NameTable.Add(nameTable);
                        db.SaveChanges();
                        nameTable.Name = "";
                    }
                }
            }
            while (nameTable.Name != "");
        }

        public void editTableName(string item)
        {
            var db = new ApplicationContext();

            if (item != "")
            {
                var currentTableName = db.NameTable.First(x => x.Name == item);

                var text1 = "Название таблицы";
                var text2 = "Изменить";

                string newName = Interaction.InputBox($"{text1}", $"{text2}", $"{currentTableName.Name}");
                if (newName != "")
                {
                    currentTableName.Name = newName;
                    db.Update(currentTableName);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Таблица не выбрана");
            }
        }

        public void deleteTable(string item)
        {
            var db = new ApplicationContext();

            if (item != "")
            {
                var currentTableName = db.NameTable.First(x => x.Name == item);

                DialogResult result = MessageBox.Show(
                    "Вы действительно хотите удалить?",
                    "Удалить",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    db.NameTable.RemoveRange(currentTableName);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Таблица не выбрана");
            }
        }
    }
}
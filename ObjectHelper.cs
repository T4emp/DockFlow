using Microsoft.VisualBasic;
using System.Reflection.Metadata;
using Xceed.Document.NET;

namespace DockFlow
{
    public class ObjectHelper
    {
        public void addObject()
        {
            var db = new ApplicationContext();
            var @object = new Object();

            var text1 = "Название объекта";
            var text2 = "Добавить";

            do
            {
                @object.Name = Interaction.InputBox($"{text1}", $"{text2}", "");

                var objectName = db.Object.ToList().Where(x => x.Name == @object.Name);

                if (@object.Name != "")
                {
                    if (objectName != null && objectName.Any())
                    {
                        MessageBox.Show("Объект уже существует");
                    }
                    else
                    {
                        db.Object.Add(@object);
                        db.SaveChanges();
                        @object.Name = "";
                    }
                }
            }
            while (@object.Name != "");
        }

        public void duplicateObject(string item)
        {
            var db = new ApplicationContext();
            var @object = new Object();
            var parameters = new Parameter();

            var obj = db.Object.First(x => x.Name == item);
            var paramObj = db.Parameter.Where(x => x.ObjectId == obj.Id);

            var name = obj.Name + $" - дубликат";

            @object.Name = name;

            db.Add(@object);
            db.SaveChanges();

            foreach (var parameter in paramObj)
            {
                parameters.Id = Guid.NewGuid();
                parameters.ObjectId = db.Object.First(x => x.Name == name).Id;
                parameters.Name = parameter.Name;

                db.AddRange(parameters);
                db.SaveChanges();
            }
        }

        public void editObjectName(string item)
        {
            var db = new ApplicationContext();

            if (item != "")
            {
                var currentObject = db.Object.First(x => x.Name == item);

                var text1 = "Название объекта";
                var text2 = "Изменить";

                string newName = Interaction.InputBox($"{text1}", $"{text2}", $"{currentObject.Name}");
                if (newName != "")
                {
                    currentObject.Name = newName;
                    db.Update(currentObject);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Объект не выбран");
            }
        }

        public void deleteOject(string item)
        {
            var db = new ApplicationContext();

            if (item != "")
            {
                var currentObject = db.Object.First(x => x.Name == item);

                DialogResult result = MessageBox.Show(
                    "Вы действительно хотите удалить?",
                    "Удалить",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    db.Object.RemoveRange(currentObject);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Объект не выбран");
            }
        }
    }
}
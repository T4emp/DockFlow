using System.Globalization;

namespace DockFlow
{
    public static class Localization
    {
        public static string Translate(string line)
        {
            var culture = CultureInfo.CurrentUICulture.ToString();
            if (culture == "ru-RU")
            {
                return rus[line];
            }
            else return eng[line];
        }

        public static Dictionary<string, string> rus = new Dictionary<string, string>
        {
            ["Home"] = "Главная",
            ["Data"] = "Данные",
            ["Upload"] = "Загрузить",
            ["Save"] = "Сохранить",
            ["Choose DOC:"] = "Выбрать документ:",
            ["Choose table:"] = "Выбрать таблицу:",
            ["Tools"] = "Инструменты",
            ["file added successfully"] = "файл успешно добавлен",
            ["file has no parameters"] = "файл не имеет параметров",
            ["Title for the table"] = "Название для таблицы",
            ["Add to table"] = "Добавить в таблицу",
            ["Name cannot be empty"] = "Имя не может быть пустым",
            ["No table selected"] = "Не выбрана таблица",
            ["Check: delete?"] = "Действительно хотите удалить?",
            ["Delete"] = "Удалено",
            ["Document or table not selected"] = "Документ или таблица не выбрана",
            ["Add"] = "Добавить",
            ["Edit"] = "Изменить",
            ["Delete"] = "Удалить",
        };

        public static Dictionary<string, string> eng = new Dictionary<string, string>
        {
            ["Home"] = "Home",
            ["Data"] = "Data",
            ["Upload"] = "Upload",
            ["Save"] = "Save",
            ["Choose DOC:"] = "Choose DOC:",
            ["Choose table:"] = "Choose table:",
            ["Tools"] = "Tools",
            ["file added successfully"] = "file added successfully",
            ["file has no parameters"] = "file has no parameters",
            ["Title for the table"] = "Title for the table",
            ["Add to table"] = "Add to table",
            ["Name cannot be empty"] = "Name cannot be empty",
            ["No table selected"] = "No table selected",
            ["Check: delete?"] = "Check: delete?",
            ["Delete table"] = "Delete table",
            ["Document or table not selected"] = "Document or table not selected",
            ["Add"] = "Add",
            ["Edit"] = "Edit",
            ["Delete"] = "Delete",
        };
    }
}
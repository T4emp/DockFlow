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
            ["Upload"] = "Загрузить документ",
            ["Save"] = "Сохранить",
            ["Choose DOC:"] = "Документ:",
            ["Choose table:"] = "Таблица с данными:",
            ["Tools"] = "Инструменты",
            ["file added successfully"] = "файл успешно добавлен",
            ["Title for the table"] = "Название таблицы",
            ["NoSelect"] = "Значение не выбрано",
            ["Check: delete?"] = "Действительно хотите удалить?",
            ["Document or table not selected"] = "Документ или таблица не выбрана",
            ["Add"] = "Добавить",
            ["Edit"] = "Изменить",
            ["Delete"] = "Удалить",
            ["TableExists"] = "Таблица уже существует",
            ["DOCExists"] = "Документ уже существует",
        };

        public static Dictionary<string, string> eng = new Dictionary<string, string>
        {
            ["Home"] = "Home",
            ["Data"] = "Data",
            ["Upload"] = "Upload document",
            ["Save"] = "Save",
            ["Choose DOC:"] = "Document:",
            ["Choose table:"] = "Table with data:",
            ["Tools"] = "Tools",
            ["file added successfully"] = "file added successfully",
            ["Title for the table"] = "Title the table",
            ["NoSelect"] = "No value selected",
            ["Check: delete?"] = "Check: delete?",
            ["Document or table not selected"] = "Document or table not selected",
            ["Add"] = "Add",
            ["Edit"] = "Edit",
            ["Delete"] = "Delete",
            ["TableExists"] = "Table already exists",
            ["DOCExists"] = "Document already exists",
        };
    }
}
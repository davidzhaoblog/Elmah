using SQLite;

namespace Elmah.SQLite.TableModels
{
    public class CodeListCaching
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string TableName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}


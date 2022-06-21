using Framework.Models;

namespace Elmah.Models
{

    public class ElmahUserAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; }

        // PredicateType:Contains
        public string? User { get; set; }
        public TextSearchTypes UserSearchType { get; set; }

    }

}


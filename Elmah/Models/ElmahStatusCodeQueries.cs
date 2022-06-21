using Framework.Models;

namespace Elmah.Models
{

    public class ElmahStatusCodeAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; }

        // PredicateType:Contains
        public string? Name { get; set; }
        public TextSearchTypes NameSearchType { get; set; }

    }

}


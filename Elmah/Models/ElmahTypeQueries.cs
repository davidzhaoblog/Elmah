using Framework.Models;

namespace Elmah.Models
{

    public class ElmahTypeIdentifier
    {

        // PredicateType:Equals
        public string? Type { get; set; }
    }

    public class ElmahTypeAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? Type { get; set; }
        public TextSearchTypes TypeSearchType { get; set; } = TextSearchTypes.Contains;
    }
}


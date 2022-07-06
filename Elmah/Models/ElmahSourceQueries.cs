using Framework.Models;

namespace Elmah.Models
{

    public class ElmahSourceAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? Source { get; set; }
        public TextSearchTypes SourceSearchType { get; set; } = TextSearchTypes.Contains;
    }

    public class ElmahSourceIdentifier
    {

        // PredicateType:Equals
        public string? Source { get; set; }
    }

}


using Framework.Models;

namespace Elmah.Models
{

    public class ElmahApplicationAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? Application { get; set; }
        public TextSearchTypes ApplicationSearchType { get; set; } = TextSearchTypes.Contains;

    }

}


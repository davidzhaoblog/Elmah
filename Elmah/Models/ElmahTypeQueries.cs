using Framework.Models;

namespace Elmah.Models
{

    public class ElmahTypeAdvancedQuery: BaseQuery
    {
        // will query all text columns in database
        public string? TextSearch { get; set; }

        // PredicateType:Contains
        public string? Type { get; set; }

    }

}


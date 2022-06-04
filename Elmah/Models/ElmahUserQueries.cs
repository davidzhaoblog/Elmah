using Framework.Models;

namespace Elmah.Models
{

    public class ElmahUserAdvancedQuery: BaseQuery
    {
        // will query all text columns in database
        public string? TextSearch { get; set; }

        // PredicateType:Contains
        public string? User { get; set; }

    }

}


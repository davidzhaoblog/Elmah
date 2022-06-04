using Framework.Models;

namespace Elmah.Models
{

    public class ElmahApplicationAdvancedQuery: BaseQuery
    {
        // will query all text columns in database
        public string? TextSearch { get; set; }

        // PredicateType:Contains
        public string? Application { get; set; }

    }

}


namespace Elmah.Models
{

    public class ElmahApplicationAdvancedQuery
    {
        // will query all text columns in database
        public string? TextSearch { get; set; }

        // PredicateType:Contains
        public string? Application { get; set; }

    }

}


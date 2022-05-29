namespace Elmah.Models
{

    public class ElmahUserAdvancedQuery
    {
        // will query all text columns in database
        public string? TextSearch { get; set; }

        // PredicateType:Contains
        public string? User { get; set; }

    }

}


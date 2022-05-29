namespace Elmah.Models
{

    public class ElmahTypeAdvancedQuery
    {
        // will query all text columns in database
        public string? TextSearch { get; set; }

        // PredicateType:Contains
        public string? Type { get; set; }

    }

}


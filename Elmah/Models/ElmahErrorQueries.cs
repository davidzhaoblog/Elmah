using Framework.Models;

namespace Elmah.Models
{

    public class ElmahErrorAdvancedQuery: BaseQuery
    {
        // will query all text columns in database
        public string? TextSearch { get; set; }

        // PredicateType:Equals
        public string? Application { get; set; }

        // PredicateType:Equals
        public string? Host { get; set; }

        // PredicateType:Equals
        public string? Source { get; set; }

        // PredicateType:Equals
        public int? StatusCode { get; set; }

        // PredicateType:Equals
        public string? Type { get; set; }

        // PredicateType:Equals
        public string? User { get; set; }

        // PredicateType:Contains
        public string? Message { get; set; }

        // PredicateType:Contains
        public string? AllXml { get; set; }

        // PredicateType:Range - Lower Bound
        public System.DateTime? TimeUtcRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        public System.DateTime? TimeUtcRangeUpper { get; set; }

    }

}


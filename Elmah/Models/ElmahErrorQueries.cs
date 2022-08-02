using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{

    public class ElmahErrorIdentifier
    {

        // PredicateType:Equals
        public System.Guid? ErrorId { get; set; }
    }

    public class ElmahErrorAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; } = TextSearchTypes.Contains;

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

        public string? TimeUtcRange { get; set; }
        // PredicateType:Range - Lower Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? TimeUtcRangeLower { get; set; }
        // PredicateType:Range - Upper Bound
        [DataType(DataType.DateTime)]
        public System.DateTime? TimeUtcRangeUpper { get; set; }

        // PredicateType:Contains
        public string? Message { get; set; }
        public TextSearchTypes MessageSearchType { get; set; } = TextSearchTypes.Contains;

        // PredicateType:Contains
        public string? AllXml { get; set; }
        public TextSearchTypes AllXmlSearchType { get; set; } = TextSearchTypes.Contains;
    }
}


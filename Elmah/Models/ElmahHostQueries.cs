using Framework.Models;

namespace Elmah.Models
{

    public class ElmahHostAdvancedQuery: BaseQuery
    {
        // will query all text columns in this table, ||
        public string? TextSearch { get; set; }
        public TextSearchTypes TextSearchType { get; set; }

        // PredicateType:Contains
        public string? Host { get; set; }
        public TextSearchTypes HostSearchType { get; set; }

        // PredicateType:GeographyRange - ReferencePoint
        public Microsoft.Spatial.Geography? SpatialLocation { get; set; }
        // PredicateType:GeographyRange - Radius
        public long? SpatialLocationRadius { get; set; }

        // PredicateType:GeographyIntersects
        public Microsoft.Spatial.GeographyPolygon? SpatialLocationGeographyIntersects { get; set; }

    }

}


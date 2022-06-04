using Framework.Models;

namespace Elmah.Models
{

    public class ElmahHostAdvancedQuery: BaseQuery
    {
        // will query all text columns in database
        public string? TextSearch { get; set; }

        // PredicateType:Contains
        public string? Host { get; set; }

        // PredicateType:GeographyRange - ReferencePoint
        public Microsoft.Spatial.Geography? SpatialLocation { get; set; }
        // PredicateType:GeographyRange - Radius
        public long? SpatialLocationRadius { get; set; }

        // PredicateType:GeographyIntersects
        public Microsoft.Spatial.GeographyPolygon? SpatialLocationGeographyIntersects { get; set; }

    }

}


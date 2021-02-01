using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.CommonBLLEntities
{
    public class GeoSearchCriteria
    {
        //public string AnyTextInNonAddressPart { get; set; }
        //public Elmah.DataSourceEntities.MapItem TextSearchInEachAddressPart { get; set; }

        public Elmah.EntityContracts.MapItemCategory[] MapItemCategories { get; set; }
        public Framework.Queries.QuerySystemStringContainsCriteria AnyText { get; set; }

        /// <summary>
        /// Gets or sets the geography range.
        /// This will be implemented now.
        /// </summary>
        /// <value>
        /// The geography range.
        /// </value>
        public Framework.Queries.QueryGeographyRangeCriteria GeographyRange { get; set; }
        public Framework.Queries.QueryGeographyIntersectsCriteria GeographyIntersects { get; set; }

        public bool CanQueryWhenNoQuery { get; set; }
        public bool HasQuery
        {
            get
            {
                return this.AnyText.IsToCompare || this.GeographyRange.IsToCompare || this.GeographyIntersects.IsToCompare;
            }
        }
    }

    public partial class GeoSearchRequestMessage
        : Framework.Services.BusinessLogicLayerRequestMessageBase<GeoSearchCriteria>
    {
    }

    /// <summary>
    /// BusinessLogicLayerResponseMessage of <see cref="Elmah.DataSourceEntities.MapItem"/>
    /// </summary>
    public class BusinessLogicLayerResponseMessageMapItemCollection
        : Framework.Services.BusinessLogicLayerResponseMessageBase<Elmah.DataSourceEntities.MapItemCollection>
    {
    }
}


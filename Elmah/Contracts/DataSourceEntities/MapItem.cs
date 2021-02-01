using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Elmah.DataSourceEntities
{
    public class MapItem
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public string SpatialLocation____Gml
        {
            get
            {
                return Framework.Helpers.GeoHelperSinglton.Instance.GeographyToGml(this.SpatialLocation);
            }
            set
            {
                this.SpatialLocation = (Microsoft.Spatial.GeographyPoint)Framework.Helpers.GeoHelperSinglton.Instance.GeographyFromGml(value);
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        //[System.Xml.Serialization.XmlIgnore]
        public string SpatialLocation____GeoJson
        {
            get
            {
                if (this.SpatialLocation == null)
                    return null;
                return Framework.Helpers.GeoHelperSinglton.Instance.GeographyToGeoJSON<Microsoft.Spatial.GeographyPoint>(this.SpatialLocation);
            }
            set
            {
                if (value == null)
                    this.SpatialLocation = null;
                else
                    this.SpatialLocation = Framework.Helpers.GeoHelperSinglton.Instance.GeographyFromGeoJSON<Microsoft.Spatial.GeographyPoint>(value);
            }
        }

        public Elmah.EntityContracts.MapItemCategory MapItemCategory { get; set; }
        public string Identifiers { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        //[JsonConverter(typeof(Framework.Helpers.GeoJsonConverter<Microsoft.Spatial.Geography>))]
        public Microsoft.Spatial.GeographyPoint SpatialLocation { get; set; }

        public string AddressType { get; set; }
        public string Address { get; set; }
        //public string AddressLine1 { get; set; }
        //public string AddressLine2 { get; set; }
        //public string City { get; set; }
        //public string StateProvince { get; set; }
        //public string CountryRegion { get; set; }
        //public string PostalCode { get; set; }
    }

    public class MapItemCollection : List<MapItem>
    {
    }

        /// <summary>
    /// Built-in DataAccessLayerMessage of <see cref="MapItemCollection"/>
    /// </summary>
    public class DataAccessLayerMessageOfMapItemCollection
        : Framework.Models.DataAccessLayerMessageBase<MapItemCollection>
    {
    }
}


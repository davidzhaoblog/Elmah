using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EFCoreContext
{

    public partial class ElmahError
    {

    }

    public partial class ElmahApplication
    {

    }

    public partial class ElmahHost
    {
        public Microsoft.Spatial.Geography? SpatialLocation
        {
            get
            {
                if (this.SpatialLocation____ == null)
                    return null;
                string geoJson = Framework.Helpers.IGeometryHelper.ToGeoJson(this.SpatialLocation____);
                return (Microsoft.Spatial.GeographyPoint)Framework.Helpers.GeoHelperSinglton.Instance.GeographyFromGeoJSON<Microsoft.Spatial.GeographyPoint>(geoJson);
            }
            set
            {
                if (value == null)
                    this.SpatialLocation____ = null;
                else
                {
                    var gml = Framework.Helpers.GeoHelperSinglton.Instance.GeographyToGeoJSON(value);
                    this.SpatialLocation____ = Framework.Helpers.IGeometryHelper.FromGeoJson(gml);
                }
            }
        }

    }

    public partial class ElmahSource
    {

    }

    public partial class ElmahStatusCode
    {

    }

    public partial class ElmahType
    {

    }

    public partial class ElmahUser
    {

    }

}


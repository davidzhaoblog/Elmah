using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using Elmah.ModelContracts;

namespace Elmah.EFCoreContext
{

    public partial class ElmahError : IElmahError
    {

    }

    public partial class ElmahApplication : IElmahApplication
    {

    }

    public partial class ElmahHost : IElmahHost
    {
        public Microsoft.Spatial.Geography SpatialLocation
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

    public partial class ElmahSource : IElmahSource
    {

    }

    public partial class ElmahStatusCode : IElmahStatusCode
    {

    }

    public partial class ElmahType : IElmahType
    {

    }

    public partial class ElmahUser : IElmahUser
    {

    }

}


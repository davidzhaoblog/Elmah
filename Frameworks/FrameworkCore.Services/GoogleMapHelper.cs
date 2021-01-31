using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using Microsoft.Extensions.Options;
using Microsoft.Spatial;

namespace FrameworkCore.Services
{
    public class GoogleMapOptions
    {
        public string ApiKey { get; set; }
    }

    public class GoogleMapHelper
    {
        public GoogleMapOptions Options { get; set; }

        public GoogleMapHelper()
        {
        }

        public GoogleMapHelper(IOptionsMonitor<GoogleMapOptions> options)
        {
            this.Options = options.CurrentValue;
        }

        //public MapPoint GetLatLongFromAddress(string strAddress)
        //{
        //    var locationService = new GoogleLocationService(this.Options.ApiKey);
        //    var point = locationService.GetLatLongFromAddress(strAddress);
        //    return point;
        //}

        public GeographyPoint GetLatLongFromAddress(string strAddress)
        {
            var locationService = new GoogleLocationService(this.Options.ApiKey);
            var point = locationService.GetLatLongFromAddress(strAddress);
            return GeographyPoint.Create(point.Latitude, point.Longitude);
        }

        public GeographyPoint GetLatLongFromAddress(string addressLine1, string addressLine2, string city, string stateProvince, string countryRegion, string postalCode)
        {
            var address = Framework.Helpers.GeoHelperSinglton.Instance.GetAddress(addressLine1, addressLine2, city, stateProvince, countryRegion, postalCode);
            return GetLatLongFromAddress(address);
        }
    }
}


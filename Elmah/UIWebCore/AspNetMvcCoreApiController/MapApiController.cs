using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Elmah.AspNetMvcCoreApiController
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    public partial class MapApiController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        //public MapApiController()
        //{
        //}

        public MapApiController(IServiceProvider serviceProvider, ILogger<MapApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        public async Task<Elmah.ViewModelData.MapVM> GetMapVMRange(double lat, double longitude, string anyText = null, long? distance1 = null, long? distance2 = 5000)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var retval = (Elmah.AspNetMvcCoreViewModel.MapVM)scope.ServiceProvider.GetRequiredService(typeof(Elmah.AspNetMvcCoreViewModel.MapVM));
                await retval.LoadData(lat, longitude, anyText, distance1, distance2);
                retval.Criteria = null;
                return retval;
            }
        }

        // [Authorize]
        [HttpGet]
        public async Task<Elmah.ViewModelData.MapVM> GetMapVM(double lat1, double lon1, double lat2, double lon2, string anyText = null)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var retval = (Elmah.AspNetMvcCoreViewModel.MapVM)scope.ServiceProvider.GetRequiredService(typeof(Elmah.AspNetMvcCoreViewModel.MapVM));
                await retval.LoadData(lat1, lon1, lat2, lon2, anyText);
                retval.Criteria = null;
                return retval;
            }
        }

/*
        /// <summary>
        /// HearBeat.
        /// http://[host]/api/AddressApi/HearBeat
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("HeartBeat")]
        public bool HeartBeat()
        {
            return true;
        }
*/
    }
}


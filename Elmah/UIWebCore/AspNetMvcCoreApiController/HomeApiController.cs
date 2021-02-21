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
using Elmah.EntityFrameworkContext;
using Elmah.EntityFrameworkDAL;

namespace Elmah.AspNetMvcCoreApiController
{
    public partial class HomeApiController : Controller
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private readonly ElmahModelEntities _linqContext;

        public HomeApiController(IServiceProvider serviceProvider, ILogger<HomeApiController> logger, ElmahModelEntities linqContext)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
            this._linqContext = linqContext;
        }

        // [Authorize]
        [HttpGet("/api/HomeApi/GetSystemDashboard", Name = "SystemDashboard")]
        public async Task<Elmah.ViewModelData.SystemDashboardVM> GetSystemDashboardVM()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var retval = (Elmah.AspNetMvcCoreViewModel.SystemDashboardVM)scope.ServiceProvider.GetRequiredService(typeof(Elmah.AspNetMvcCoreViewModel.SystemDashboardVM));
                await retval.LoadData();
                return retval;
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/api/HomeApi/GetDashboard", Name = "Dashboard")]
        public async Task<Elmah.ViewModelData.DashboardVM> GetDashboardVM(string shortGuid)
        {
            //example on how to get EntityID
            //var user = await _userManager.FindByIdAsync(HttpContext.User.Identity.Name);
            //var entityID = user.EntityID;

            using (var scope = _serviceProvider.CreateScope())
            {
                //var entityID =
                //    from t in this._linqContext.Entity
                //    where t.ShortGuid == shortGuid
                //    select t.EntityID;
                //if (!entityID.Any())
                //return new Elmah.ViewModelData.DashboardVM { SomeData = "Welcome" };
                var retval = scope.ServiceProvider.GetRequiredService<Elmah.AspNetMvcCoreViewModel.DashboardVM>();
                await retval.LoadData(default(long));
                return retval;
            }
        }

        /// <summary>
        /// HearBeat.
        /// http://[host]/api/HomeApi/HearBeat
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/HomeApi/HeartBeat", Name = "HeartBeat")]
        public bool HeartBeat()
        {
            return true;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Elmah.AspNetMvcCoreViewModel;
using Elmah.MvcCore.Models;

namespace Elmah.MvcCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public HomeController(IServiceProvider serviceProvider, ILogger<HomeController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = Framework.Resx.UIStringResource.Common_AboutPageTitle;

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = Framework.Resx.UIStringResource.Common_ContactPageTitle;

            return View();
        }

        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public ActionResult Map()
        {
            return View();
        }

        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<IActionResult> SystemDashboard()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var vm = (SystemDashboardVM)scope.ServiceProvider.GetRequiredService(typeof(SystemDashboardVM));
                await vm.LoadData();
                return View(vm);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


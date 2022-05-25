using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace WebApiControllers_ElmahApplicationController
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class WebApiControllers_ElmahApplicationController : Controller
    {
        IElmahApplicationService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WebApiControllers_ElmahApplicationController> _logger;

        public WebApiControllers_ElmahApplicationController(IElmahApplicationService thisService, IServiceProvider serviceProvider, ILogger<WebApiControllers_ElmahApplicationController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        [HttpDelete]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahApplicationModel>>> Delete(ElmahApplicationIdModel id)
        {
            return await _thisService.Delete(id);
        }

        [HttpGet]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahApplicationModel>>> Get(ElmahApplicationIdModel id)
        {
            return await _thisService.Get(id);
        }

        [HttpPut]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahApplicationModel>>> Put(ElmahApplicationModel input)
        {
            return await _thisService.Create(input);
        }

        [HttpPost]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahApplicationModel>>> Post(ElmahApplicationModel input)
        {
            return await _thisService.Update(input);
        }

        /*
        /// <summary>
        /// HearBeat.
        /// http://[host]/api/ElmahUserApi/HearBeat
        /// </summary>
        /// <returns></returns>
        // [Authorize]
        [HttpGet, ActionName("HeartBeat")]
        public bool HeartBeat()
        {
            return true;
        }
        */
    }
}


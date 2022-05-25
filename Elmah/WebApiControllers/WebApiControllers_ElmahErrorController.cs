using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace WebApiControllers_ElmahErrorController
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class WebApiControllers_ElmahErrorController : Controller
    {
        IElmahErrorService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WebApiControllers_ElmahErrorController> _logger;

        public WebApiControllers_ElmahErrorController(IElmahErrorService thisService, IServiceProvider serviceProvider, ILogger<WebApiControllers_ElmahErrorController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        [HttpDelete]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahErrorModel>>> Delete(ElmahErrorIdModel id)
        {
            return await _thisService.Delete(id);
        }

        [HttpGet]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahErrorModel>>> Get(ElmahErrorIdModel id)
        {
            return await _thisService.Get(id);
        }

        [HttpPut]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahErrorModel>>> Put(ElmahErrorModel input)
        {
            return await _thisService.Create(input);
        }

        [HttpPost]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahErrorModel>>> Post(ElmahErrorModel input)
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


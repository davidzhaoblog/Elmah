using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ElmahUserController
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class ElmahUserController : Controller
    {
        IElmahUserService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahUserController> _logger;

        public ElmahUserController(IElmahUserService thisService, IServiceProvider serviceProvider, ILogger<ElmahUserController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        [HttpDelete]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahUserModel>>> Delete(ElmahUserIdModel id)
        {
            return await _thisService.Delete(id);
        }

        [HttpGet]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahUserModel>>> Get(ElmahUserIdModel id)
        {
            return await _thisService.Get(id);
        }

        [HttpPut]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahUserModel>>> Put(ElmahUserModel input)
        {
            return await _thisService.Create(input);
        }

        [HttpPost]
        public async Task<ActionResult<Response<HttpStatusCode, ElmahUserModel>>> Post(ElmahUserModel input)
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


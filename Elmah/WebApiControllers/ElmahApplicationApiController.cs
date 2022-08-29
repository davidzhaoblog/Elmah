using Elmah.ServiceContracts;
using Framework.Mvc;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.WebApiControllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public partial class ElmahApplicationApiController : BaseApiController
    {
        IElmahApplicationService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahApplicationApiController> _logger;

        public ElmahApplicationApiController(IElmahApplicationService thisService, IServiceProvider serviceProvider, ILogger<ElmahApplicationApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<ElmahApplicationDataModel[]>>> Search(
            ElmahApplicationAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        /*
        // [Authorize]
        [HttpGet, ActionName("HeartBeat")]
        public Task<ActionResult> HeartBeat()
        {
            return Ok();
        }
        */
    }
}


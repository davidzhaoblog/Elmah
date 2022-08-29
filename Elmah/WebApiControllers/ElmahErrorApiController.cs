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
    public partial class ElmahErrorApiController : BaseApiController
    {
        IElmahErrorService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahErrorApiController> _logger;

        public ElmahErrorApiController(IElmahErrorService thisService, IServiceProvider serviceProvider, ILogger<ElmahErrorApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<ListResponse<ElmahErrorDataModel.DefaultView[]>>> Search(
            ElmahErrorAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<ListResponse<ElmahErrorDataModel.DefaultView[]>>> BulkUpdate(BatchActionRequest<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> data)
        {
            var serviceResponse = await _thisService.BulkUpdate(data);
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


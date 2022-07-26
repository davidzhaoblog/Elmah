using Elmah.ServiceContracts;
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
    public partial class ElmahStatusCodeApiController : BaseApiController
    {
        IElmahStatusCodeService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahStatusCodeApiController> _logger;

        public ElmahStatusCodeApiController(IElmahStatusCodeService thisService, IServiceProvider serviceProvider, ILogger<ElmahStatusCodeApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<PagedResponse<ElmahStatusCodeModel[]>>> Search(
            ElmahStatusCodeAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<ElmahStatusCodeCompositeModel>> GetCompositeModel(ElmahStatusCodeIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<ElmahStatusCodeModel>> Post(ElmahStatusCodeIdentifier id, ElmahStatusCodeModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<ElmahStatusCodeModel>> Put(ElmahStatusCodeModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete(ElmahStatusCodeIdentifier id)
        {
            var serviceResponse = await _thisService.Delete(id);
            return ReturnWithoutBodyActionResult(serviceResponse);
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


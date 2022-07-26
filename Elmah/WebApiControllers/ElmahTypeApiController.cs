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
    public partial class ElmahTypeApiController : BaseApiController
    {
        IElmahTypeService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahTypeApiController> _logger;

        public ElmahTypeApiController(IElmahTypeService thisService, IServiceProvider serviceProvider, ILogger<ElmahTypeApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<PagedResponse<ElmahTypeModel[]>>> Search(
            ElmahTypeAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<ElmahTypeCompositeModel>> GetCompositeModel(ElmahTypeIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<ElmahTypeModel>> Post(ElmahTypeIdentifier id, ElmahTypeModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<ElmahTypeModel>> Get(ElmahTypeIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<ElmahTypeModel>> Put(ElmahTypeModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete(ElmahTypeIdentifier id)
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


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
    public partial class ElmahErrorController : BaseApiController
    {
        IElmahErrorService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahErrorController> _logger;

        public ElmahErrorController(IElmahErrorService thisService, IServiceProvider serviceProvider, ILogger<ElmahErrorController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete(ElmahErrorIdModel id)
        {
            var serviceResponse = await _thisService.Delete(id);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdModel id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<ElmahErrorModel.DefaultView>> Put(ElmahErrorModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<ElmahErrorModel.DefaultView>> Post(ElmahErrorModel input)
        {
            var serviceResponse = await _thisService.Update(input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<PagedResponse<ElmahErrorModel.DefaultView[]>>> Search(
            ElmahErrorAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<PagedResponse<NameValuePair[]>>> GetCodeList(
            ElmahErrorAdvancedQuery query)
        {
            var serviceResponse = await _thisService.GetCodeList(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<ElmahErrorCompositeModel>> GetCompositeModel(ElmahErrorIdModel id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
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


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
        public async Task<ActionResult<PagedResponse<ElmahTypeDataModel[]>>> Search(
            ElmahTypeAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{Type}")]
        [HttpGet]
        public async Task<ActionResult<ElmahTypeCompositeModel>> GetCompositeModel(ElmahTypeIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> BulkDelete(List<ElmahTypeIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<Response<MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel> input)
        {
            var serviceResponse = await _thisService.MultiItemsCUD(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{Type}")]
        [HttpPut]
        public async Task<ActionResult<ElmahTypeDataModel>> Put([FromRoute]ElmahTypeIdentifier id, [FromBody]ElmahTypeDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{Type}")]
        [HttpGet]
        public async Task<ActionResult<ElmahTypeDataModel>> Get([FromRoute]ElmahTypeIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<ElmahTypeDataModel>> Post(ElmahTypeDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{Type}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]ElmahTypeIdentifier id)
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


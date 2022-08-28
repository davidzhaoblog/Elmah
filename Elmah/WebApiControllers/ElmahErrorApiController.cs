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
        public async Task<ActionResult<PagedResponse<ElmahErrorDataModel.DefaultView[]>>> Search(
            ElmahErrorAdvancedQuery query)
        {
            var serviceResponse = await _thisService.Search(query);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ErrorId}")]
        [HttpGet]
        public async Task<ActionResult<ElmahErrorCompositeModel>> GetCompositeModel(ElmahErrorIdentifier id)
        {
            var serviceResponse = await _thisService.GetCompositeModel(id, null);
            return Ok(serviceResponse);
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult> BulkDelete(List<ElmahErrorIdentifier> ids)
        {
            var serviceResponse = await _thisService.BulkDelete(ids);
            return ReturnWithoutBodyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<PagedResponse<ElmahErrorDataModel.DefaultView[]>>> BulkUpdate(BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> data)
        {
            var serviceResponse = await _thisService.BulkUpdate(data);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> input)
        {
            var serviceResponse = await _thisService.MultiItemsCUD(input);
            return ReturnActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ErrorId}")]
        [HttpPut]
        public async Task<ActionResult<ElmahErrorDataModel.DefaultView>> Put([FromRoute]ElmahErrorIdentifier id, [FromBody]ElmahErrorDataModel input)
        {
            var serviceResponse = await _thisService.Update(id, input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ErrorId}")]
        [HttpGet]
        public async Task<ActionResult<ElmahErrorDataModel.DefaultView>> Get([FromRoute]ElmahErrorIdentifier id)
        {
            var serviceResponse = await _thisService.Get(id);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<ElmahErrorDataModel.DefaultView>> Post(ElmahErrorDataModel input)
        {
            var serviceResponse = await _thisService.Create(input);
            return ReturnResultOnlyActionResult(serviceResponse);
        }

        // [Authorize]
        [Route("{ErrorId}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute]ElmahErrorIdentifier id)
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


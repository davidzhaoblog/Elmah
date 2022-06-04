using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ElmahApplicationController
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class ElmahApplicationController : Controller
    {
        IElmahApplicationService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahApplicationController> _logger;

        public ElmahApplicationController(IElmahApplicationService thisService, IServiceProvider serviceProvider, ILogger<ElmahApplicationController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Response<ElmahApplicationModel>>> Delete(ElmahApplicationIdModel id)
        {
            return await _thisService.Delete(id);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<Response<ElmahApplicationModel>>> Get(ElmahApplicationIdModel id)
        {
            return await _thisService.Get(id);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<Response<ElmahApplicationModel>>> Put(ElmahApplicationModel input)
        {
            return await _thisService.Create(input);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ElmahApplicationModel>>> Post(ElmahApplicationModel input)
        {
            return await _thisService.Update(input);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<PagedResponse<ElmahApplicationModel[]>> Search(
            ElmahApplicationAdvancedQuery query)
        {
            return await _thisService.Search(query);
        }

        // [Authorize]
        [HttpGet]
        [HttpPost]
        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query)
        {
            return await _thisService.GetCodeList(query);
        }

        /*
        // [Authorize]
        [HttpGet, ActionName("HeartBeat")]
        public bool HeartBeat()
        {
            return true;
        }
        */
    }
}


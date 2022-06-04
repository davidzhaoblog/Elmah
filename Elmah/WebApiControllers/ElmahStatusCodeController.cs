using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ElmahStatusCodeController
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class ElmahStatusCodeController : Controller
    {
        IElmahStatusCodeService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahStatusCodeController> _logger;

        public ElmahStatusCodeController(IElmahStatusCodeService thisService, IServiceProvider serviceProvider, ILogger<ElmahStatusCodeController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Response<ElmahStatusCodeModel>>> Delete(ElmahStatusCodeIdModel id)
        {
            return await _thisService.Delete(id);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<Response<ElmahStatusCodeModel>>> Get(ElmahStatusCodeIdModel id)
        {
            return await _thisService.Get(id);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<Response<ElmahStatusCodeModel>>> Put(ElmahStatusCodeModel input)
        {
            return await _thisService.Create(input);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ElmahStatusCodeModel>>> Post(ElmahStatusCodeModel input)
        {
            return await _thisService.Update(input);
        }

        public async Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query)
        {
            return await _thisService.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query)
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


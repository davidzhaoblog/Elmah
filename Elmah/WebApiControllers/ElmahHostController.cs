using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ElmahHostController
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class ElmahHostController : Controller
    {
        IElmahHostService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahHostController> _logger;

        public ElmahHostController(IElmahHostService thisService, IServiceProvider serviceProvider, ILogger<ElmahHostController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Response<ElmahHostModel>>> Delete(ElmahHostIdModel id)
        {
            return await _thisService.Delete(id);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<Response<ElmahHostModel>>> Get(ElmahHostIdModel id)
        {
            return await _thisService.Get(id);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<Response<ElmahHostModel>>> Put(ElmahHostModel input)
        {
            return await _thisService.Create(input);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ElmahHostModel>>> Post(ElmahHostModel input)
        {
            return await _thisService.Update(input);
        }

        public async Task<PagedResponse<ElmahHostModel[]>> Search(
            ElmahHostAdvancedQuery query)
        {
            return await _thisService.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query)
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


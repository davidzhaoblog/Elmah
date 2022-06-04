using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ElmahUserController
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class ElmahUserController : Controller
    {
        IElmahUserService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahUserController> _logger;

        public ElmahUserController(IElmahUserService thisService, IServiceProvider serviceProvider, ILogger<ElmahUserController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Response<ElmahUserModel>>> Delete(ElmahUserIdModel id)
        {
            return await _thisService.Delete(id);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<Response<ElmahUserModel>>> Get(ElmahUserIdModel id)
        {
            return await _thisService.Get(id);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<Response<ElmahUserModel>>> Put(ElmahUserModel input)
        {
            return await _thisService.Create(input);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ElmahUserModel>>> Post(ElmahUserModel input)
        {
            return await _thisService.Update(input);
        }

        public async Task<PagedResponse<ElmahUserModel[]>> Search(
            ElmahUserAdvancedQuery query)
        {
            return await _thisService.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query)
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


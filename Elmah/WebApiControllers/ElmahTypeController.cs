using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ElmahTypeController
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class ElmahTypeController : Controller
    {
        IElmahTypeService _thisService { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElmahTypeController> _logger;

        public ElmahTypeController(IElmahTypeService thisService, IServiceProvider serviceProvider, ILogger<ElmahTypeController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._thisService = thisService;
            this._logger = logger;
        }

        // [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Response<ElmahTypeModel>>> Delete(ElmahTypeIdModel id)
        {
            return await _thisService.Delete(id);
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<Response<ElmahTypeModel>>> Get(ElmahTypeIdModel id)
        {
            return await _thisService.Get(id);
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<Response<ElmahTypeModel>>> Put(ElmahTypeModel input)
        {
            return await _thisService.Create(input);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<ElmahTypeModel>>> Post(ElmahTypeModel input)
        {
            return await _thisService.Update(input);
        }

        public async Task<PagedResponse<ElmahTypeModel[]>> Search(
            ElmahTypeAdvancedQuery query)
        {
            return await _thisService.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query)
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


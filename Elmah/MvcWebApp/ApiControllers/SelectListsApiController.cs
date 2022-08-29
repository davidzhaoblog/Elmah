using Elmah.Models;
using Elmah.RepositoryContracts;
using Framework.Models;

using Microsoft.AspNetCore.Mvc;

namespace Elmah.MvcWebApp.ApiControllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class SelectListsApiController : Controller
    {
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<SelectListsApiController> _logger;

        public SelectListsApiController(
            IServiceScopeFactory serviceScopeFactor,
            ILogger<SelectListsApiController> logger)
        {
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        // [Authorize]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetElmahApplicationCodeList(
            [FromQuery]ElmahApplicationAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var elmahApplicationRepository = scope.ServiceProvider.GetRequiredService<IElmahApplicationRepository>();
                var serviceResponse = await elmahApplicationRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetElmahHostCodeList(
            [FromQuery]ElmahHostAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var elmahHostRepository = scope.ServiceProvider.GetRequiredService<IElmahHostRepository>();
                var serviceResponse = await elmahHostRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetElmahSourceCodeList(
            [FromQuery]ElmahSourceAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var elmahSourceRepository = scope.ServiceProvider.GetRequiredService<IElmahSourceRepository>();
                var serviceResponse = await elmahSourceRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetElmahStatusCodeCodeList(
            [FromQuery]ElmahStatusCodeAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var elmahStatusCodeRepository = scope.ServiceProvider.GetRequiredService<IElmahStatusCodeRepository>();
                var serviceResponse = await elmahStatusCodeRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetElmahTypeCodeList(
            [FromQuery]ElmahTypeAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var elmahTypeRepository = scope.ServiceProvider.GetRequiredService<IElmahTypeRepository>();
                var serviceResponse = await elmahTypeRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

        // [Authorize]
        public async Task<ActionResult<ListResponse<NameValuePair[]>>> GetElmahUserCodeList(
            [FromQuery]ElmahUserAdvancedQuery query)
        {
            using (var scope = _serviceScopeFactor.CreateScope())
            {
                var elmahUserRepository = scope.ServiceProvider.GetRequiredService<IElmahUserRepository>();
                var serviceResponse = await elmahUserRepository.GetCodeList(query);
                return serviceResponse;
            }
        }

    }
}


using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahApplicationService
        : IElmahApplicationService
    {
        private readonly ILogger<ElmahApplicationService> _logger;
        private readonly IElmahApplicationRepository _thisRepository;

        public ElmahApplicationService(IElmahApplicationRepository thisRepository, ILogger<ElmahApplicationService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahApplicationModel>> Delete(ElmahApplicationIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<HttpStatusCode, ElmahApplicationModel>> Get(ElmahApplicationIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<HttpStatusCode, ElmahApplicationModel>> Create(ElmahApplicationModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<HttpStatusCode, ElmahApplicationModel>> Update(ElmahApplicationModel input)
        {
            return await _thisRepository.Update(input);
        }
    }
}


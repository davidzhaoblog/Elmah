using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahSourceService
        : IElmahSourceService
    {
        private readonly ILogger<ElmahSourceService> _logger;
        private readonly IElmahSourceRepository _thisRepository;

        public ElmahSourceService(IElmahSourceRepository thisRepository, ILogger<ElmahSourceService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahSourceModel>> Delete(ElmahSourceIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<HttpStatusCode, ElmahSourceModel>> Get(ElmahSourceIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<HttpStatusCode, ElmahSourceModel>> Create(ElmahSourceModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<HttpStatusCode, ElmahSourceModel>> Update(ElmahSourceModel input)
        {
            return await _thisRepository.Update(input);
        }
    }
}


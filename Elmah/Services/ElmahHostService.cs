using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahHostService
        : IElmahHostService
    {
        private readonly ILogger<ElmahHostService> _logger;
        private readonly IElmahHostRepository _thisRepository;

        public ElmahHostService(IElmahHostRepository thisRepository, ILogger<ElmahHostService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahHostModel>> Delete(ElmahHostIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<HttpStatusCode, ElmahHostModel>> Get(ElmahHostIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<HttpStatusCode, ElmahHostModel>> Create(ElmahHostModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<HttpStatusCode, ElmahHostModel>> Update(ElmahHostModel input)
        {
            return await _thisRepository.Update(input);
        }
    }
}


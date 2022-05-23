using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahErrorService
        : IElmahErrorService
    {
        private readonly ILogger<ElmahErrorService> _logger;
        private readonly IElmahErrorRepository _thisRepository;

        public ElmahErrorService(IElmahErrorRepository thisRepository, ILogger<ElmahErrorService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahErrorModel>> Delete(ElmahErrorIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<HttpStatusCode, ElmahErrorModel>> Get(ElmahErrorIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<HttpStatusCode, ElmahErrorModel>> Create(ElmahErrorModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<HttpStatusCode, ElmahErrorModel>> Update(ElmahErrorModel input)
        {
            return await _thisRepository.Update(input);
        }
    }
}


using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahStatusCodeService
        : IElmahStatusCodeService
    {
        private readonly ILogger<ElmahStatusCodeService> _logger;
        private readonly IElmahStatusCodeRepository _thisRepository;

        public ElmahStatusCodeService(IElmahStatusCodeRepository thisRepository, ILogger<ElmahStatusCodeService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahStatusCodeModel>> Delete(ElmahStatusCodeIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<HttpStatusCode, ElmahStatusCodeModel>> Get(ElmahStatusCodeIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<HttpStatusCode, ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<HttpStatusCode, ElmahStatusCodeModel>> Update(ElmahStatusCodeModel input)
        {
            return await _thisRepository.Update(input);
        }
    }
}


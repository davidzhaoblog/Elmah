using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahUserService
        : IElmahUserService
    {
        private readonly ILogger<ElmahUserService> _logger;
        private readonly IElmahUserRepository _thisRepository;

        public ElmahUserService(IElmahUserRepository thisRepository, ILogger<ElmahUserService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response<HttpStatusCode, ElmahUserModel>> Delete(ElmahUserIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<HttpStatusCode, ElmahUserModel>> Get(ElmahUserIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<HttpStatusCode, ElmahUserModel>> Create(ElmahUserModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<HttpStatusCode, ElmahUserModel>> Update(ElmahUserModel input)
        {
            return await _thisRepository.Update(input);
        }
    }
}


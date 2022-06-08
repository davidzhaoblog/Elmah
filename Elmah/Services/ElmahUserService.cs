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

        public async Task<Response> Delete(ElmahUserIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<ElmahUserModel>> Get(ElmahUserIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahUserModel>> Create(ElmahUserModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<ElmahUserModel>> Update(ElmahUserModel input)
        {
            return await _thisRepository.Update(input);
        }

        public async Task<PagedResponse<ElmahUserModel[]>> Search(
            ElmahUserAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }

    }
}


using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Collections.Concurrent;

namespace Elmah.Services
{
    public class ElmahUserService
        : IElmahUserService
    {
        private readonly IElmahUserRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahUserService> _logger;

        public ElmahUserService(
            IElmahUserRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahUserService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ElmahUserDataModel[]>> Search(
            ElmahUserAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ElmahUserDataModel>> Update(ElmahUserIdentifier id, ElmahUserDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahUserDataModel>> Get(ElmahUserIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahUserDataModel>> Create(ElmahUserDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahUserDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahUserDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ElmahUserIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


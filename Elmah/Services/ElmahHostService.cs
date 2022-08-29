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
    public class ElmahHostService
        : IElmahHostService
    {
        private readonly IElmahHostRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahHostService> _logger;

        public ElmahHostService(
            IElmahHostRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahHostService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ElmahHostDataModel[]>> Search(
            ElmahHostAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ElmahHostDataModel>> Update(ElmahHostIdentifier id, ElmahHostDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahHostDataModel>> Get(ElmahHostIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahHostDataModel>> Create(ElmahHostDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahHostDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahHostDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ElmahHostIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


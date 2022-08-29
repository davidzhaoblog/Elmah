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
    public class ElmahSourceService
        : IElmahSourceService
    {
        private readonly IElmahSourceRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahSourceService> _logger;

        public ElmahSourceService(
            IElmahSourceRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahSourceService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ElmahSourceDataModel[]>> Search(
            ElmahSourceAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ElmahSourceDataModel>> Update(ElmahSourceIdentifier id, ElmahSourceDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahSourceDataModel>> Get(ElmahSourceIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahSourceDataModel>> Create(ElmahSourceDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahSourceDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahSourceDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ElmahSourceIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


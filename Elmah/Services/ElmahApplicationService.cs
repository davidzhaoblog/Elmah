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
    public class ElmahApplicationService
        : IElmahApplicationService
    {
        private readonly IElmahApplicationRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahApplicationService> _logger;

        public ElmahApplicationService(
            IElmahApplicationRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahApplicationService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ElmahApplicationDataModel[]>> Search(
            ElmahApplicationAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ElmahApplicationDataModel>> Update(ElmahApplicationIdentifier id, ElmahApplicationDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahApplicationDataModel>> Get(ElmahApplicationIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahApplicationDataModel>> Create(ElmahApplicationDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahApplicationDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahApplicationDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ElmahApplicationIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


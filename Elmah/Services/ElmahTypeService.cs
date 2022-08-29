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
    public class ElmahTypeService
        : IElmahTypeService
    {
        private readonly IElmahTypeRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahTypeService> _logger;

        public ElmahTypeService(
            IElmahTypeRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahTypeService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<ListResponse<ElmahTypeDataModel[]>> Search(
            ElmahTypeAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ElmahTypeDataModel>> Update(ElmahTypeIdentifier id, ElmahTypeDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahTypeDataModel>> Get(ElmahTypeIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahTypeDataModel>> Create(ElmahTypeDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahTypeDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahTypeDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ElmahTypeIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


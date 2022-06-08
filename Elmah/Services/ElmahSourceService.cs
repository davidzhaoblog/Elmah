using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahSourceService
        : IElmahSourceService
    {
        private readonly ILogger<ElmahSourceService> _logger;
        private readonly IElmahSourceRepository _thisRepository;

        public ElmahSourceService(IElmahSourceRepository thisRepository, ILogger<ElmahSourceService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response> Delete(ElmahSourceIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<ElmahSourceModel>> Get(ElmahSourceIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahSourceModel>> Create(ElmahSourceModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<ElmahSourceModel>> Update(ElmahSourceModel input)
        {
            return await _thisRepository.Update(input);
        }

        public async Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }

    }
}


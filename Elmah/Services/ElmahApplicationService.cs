using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahApplicationService
        : IElmahApplicationService
    {
        private readonly ILogger<ElmahApplicationService> _logger;
        private readonly IElmahApplicationRepository _thisRepository;

        public ElmahApplicationService(IElmahApplicationRepository thisRepository, ILogger<ElmahApplicationService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response<ElmahApplicationModel>> Delete(ElmahApplicationIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<ElmahApplicationModel>> Get(ElmahApplicationIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahApplicationModel>> Create(ElmahApplicationModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<ElmahApplicationModel>> Update(ElmahApplicationModel input)
        {
            return await _thisRepository.Update(input);
        }

        public async Task<PagedResponse<ElmahApplicationModel[]>> Search(
            ElmahApplicationAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


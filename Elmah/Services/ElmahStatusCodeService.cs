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

        public async Task<Response> Delete(ElmahStatusCodeIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<ElmahStatusCodeModel>> Update(ElmahStatusCodeModel input)
        {
            return await _thisRepository.Update(input);
        }

        public async Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }

    }
}


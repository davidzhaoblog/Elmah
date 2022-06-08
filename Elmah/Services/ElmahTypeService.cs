using Elmah.RepositoryContracts;
using Elmah.ServiceContracts;
using Elmah.Models;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Elmah.Services
{
    public class ElmahTypeService
        : IElmahTypeService
    {
        private readonly ILogger<ElmahTypeService> _logger;
        private readonly IElmahTypeRepository _thisRepository;

        public ElmahTypeService(IElmahTypeRepository thisRepository, ILogger<ElmahTypeService> logger)
        {
            _thisRepository = thisRepository;
            _logger = logger;
        }

        public async Task<Response> Delete(ElmahTypeIdModel id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<Response<ElmahTypeModel>> Get(ElmahTypeIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahTypeModel>> Create(ElmahTypeModel input)
        {
            return await _thisRepository.Create(input);
        }

        public async Task<Response<ElmahTypeModel>> Update(ElmahTypeModel input)
        {
            return await _thisRepository.Update(input);
        }

        public async Task<PagedResponse<ElmahTypeModel[]>> Search(
            ElmahTypeAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }

    }
}


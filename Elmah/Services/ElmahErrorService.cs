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
    public class ElmahErrorService
        : IElmahErrorService
    {
        private readonly IElmahErrorRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahErrorService> _logger;

        public ElmahErrorService(
            IElmahErrorRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahErrorService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<ElmahErrorModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<Response<ElmahErrorModel.DefaultView>> Update(ElmahErrorIdentifier id, ElmahErrorModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahErrorModel.DefaultView>> Create(ElmahErrorModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahErrorModel.DefaultView GetDefault()
        {
            // TODO: please set default value here
            return new ElmahErrorModel.DefaultView();
        }

        public async Task<Response> Delete(ElmahErrorIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

    }
}


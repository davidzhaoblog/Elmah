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

        public async Task<ListResponse<ElmahErrorDataModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ListResponse<ElmahErrorDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> data)
        {
            return await _thisRepository.BulkUpdate(data);
        }

        public async Task<Response<ElmahErrorDataModel.DefaultView>> Get(ElmahErrorIdentifier id)
        {
            return await _thisRepository.Get(id);
        }
    }
}


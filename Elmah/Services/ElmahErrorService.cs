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

        public async Task<ElmahErrorCompositeModel> GetCompositeModel(ElmahErrorIdentifier id, ElmahErrorCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahErrorCompositeModel();
                failedResponse.Responses.Add(ElmahErrorCompositeModel.__DataOptions__.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahErrorCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahErrorCompositeModel.__DataOptions__, Response>();
            responses.TryAdd(ElmahErrorCompositeModel.__DataOptions__.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            successResponse.Responses = new Dictionary<ElmahErrorCompositeModel.__DataOptions__, Response>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ElmahErrorIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<PagedResponse<ElmahErrorModel.DefaultView[]>> BulkUpdate(BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorModel.DefaultView> data)
        {
            return await _thisRepository.BulkUpdate(data);
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
            return new ElmahErrorModel.DefaultView { Status______ = Framework.Models.ItemStatus.New };
        }

        public async Task<Response> Delete(ElmahErrorIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahErrorAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


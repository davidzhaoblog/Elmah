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

        public async Task<PagedResponse<ElmahApplicationDataModel[]>> Search(
            ElmahApplicationAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ElmahApplicationCompositeModel> GetCompositeModel(
            ElmahApplicationIdentifier id,
            Dictionary<ElmahApplicationCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahApplicationCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahApplicationCompositeModel();
                failedResponse.Responses.Add(ElmahApplicationCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahApplicationCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahApplicationCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ElmahApplicationCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahApplicationCompositeModel.__DataOptions__.ElmahErrors_Via_Application))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery
                        {
                            Application = id.Application,
                            PageIndex = 1,
                            PageSize = listItemRequest[ElmahApplicationCompositeModel.__DataOptions__.ElmahErrors_Via_Application].PageSize,
                            OrderBys= listItemRequest[ElmahApplicationCompositeModel.__DataOptions__.ElmahErrors_Via_Application].OrderBys,
                            PaginationOption = listItemRequest[ElmahApplicationCompositeModel.__DataOptions__.ElmahErrors_Via_Application].PaginationOption,
                        };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahApplicationCompositeModel.__DataOptions__.ElmahErrors_Via_Application, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ElmahErrors_Via_Application = response.ResponseBody;
                        }
                    }
                }));
            }

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            successResponse.Responses = new Dictionary<ElmahApplicationCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ElmahApplicationIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
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

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


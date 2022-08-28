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
    public class ElmahHostService
        : IElmahHostService
    {
        private readonly IElmahHostRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahHostService> _logger;

        public ElmahHostService(
            IElmahHostRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahHostService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<ElmahHostDataModel[]>> Search(
            ElmahHostAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ElmahHostCompositeModel> GetCompositeModel(
            ElmahHostIdentifier id,
            Dictionary<ElmahHostCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahHostCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahHostCompositeModel();
                failedResponse.Responses.Add(ElmahHostCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahHostCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahHostCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ElmahHostCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahHostCompositeModel.__DataOptions__.ElmahErrors_Via_Host))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery
                        {
                            Host = id.Host,
                            PageIndex = 1,
                            PageSize = listItemRequest[ElmahHostCompositeModel.__DataOptions__.ElmahErrors_Via_Host].PageSize,
                            OrderBys= listItemRequest[ElmahHostCompositeModel.__DataOptions__.ElmahErrors_Via_Host].OrderBys,
                            PaginationOption = listItemRequest[ElmahHostCompositeModel.__DataOptions__.ElmahErrors_Via_Host].PaginationOption,
                        };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahHostCompositeModel.__DataOptions__.ElmahErrors_Via_Host, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ElmahErrors_Via_Host = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<ElmahHostCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ElmahHostIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<ElmahHostDataModel>> Update(ElmahHostIdentifier id, ElmahHostDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahHostDataModel>> Get(ElmahHostIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahHostDataModel>> Create(ElmahHostDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahHostDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahHostDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ElmahHostIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


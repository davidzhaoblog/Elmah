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
    public class ElmahStatusCodeService
        : IElmahStatusCodeService
    {
        private readonly IElmahStatusCodeRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahStatusCodeService> _logger;

        public ElmahStatusCodeService(
            IElmahStatusCodeRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahStatusCodeService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<ElmahStatusCodeDataModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ElmahStatusCodeCompositeModel> GetCompositeModel(
            ElmahStatusCodeIdentifier id,
            Dictionary<ElmahStatusCodeCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahStatusCodeCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahStatusCodeCompositeModel();
                failedResponse.Responses.Add(ElmahStatusCodeCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahStatusCodeCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahStatusCodeCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ElmahStatusCodeCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahStatusCodeCompositeModel.__DataOptions__.ElmahErrors_Via_StatusCode))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery
                        {
                            StatusCode = id.StatusCode,
                            PageIndex = 1,
                            PageSize = listItemRequest[ElmahStatusCodeCompositeModel.__DataOptions__.ElmahErrors_Via_StatusCode].PageSize,
                            OrderBys= listItemRequest[ElmahStatusCodeCompositeModel.__DataOptions__.ElmahErrors_Via_StatusCode].OrderBys,
                            PaginationOption = listItemRequest[ElmahStatusCodeCompositeModel.__DataOptions__.ElmahErrors_Via_StatusCode].PaginationOption,
                        };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahStatusCodeCompositeModel.__DataOptions__.ElmahErrors_Via_StatusCode, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ElmahErrors_Via_StatusCode = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<ElmahStatusCodeCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ElmahStatusCodeIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDModel<ElmahStatusCodeIdentifier, ElmahStatusCodeDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahStatusCodeIdentifier, ElmahStatusCodeDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<ElmahStatusCodeDataModel>> Update(ElmahStatusCodeIdentifier id, ElmahStatusCodeDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahStatusCodeDataModel>> Get(ElmahStatusCodeIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahStatusCodeDataModel>> Create(ElmahStatusCodeDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahStatusCodeDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahStatusCodeDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ElmahStatusCodeIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


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
    public class ElmahUserService
        : IElmahUserService
    {
        private readonly IElmahUserRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahUserService> _logger;

        public ElmahUserService(
            IElmahUserRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahUserService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<ElmahUserDataModel[]>> Search(
            ElmahUserAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ElmahUserCompositeModel> GetCompositeModel(
            ElmahUserIdentifier id,
            Dictionary<ElmahUserCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahUserCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahUserCompositeModel();
                failedResponse.Responses.Add(ElmahUserCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahUserCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahUserCompositeModel.__DataOptions__, Response<PaginationResponse>>();
            responses.TryAdd(ElmahUserCompositeModel.__DataOptions__.__Master__, new Response<PaginationResponse> { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahUserCompositeModel.__DataOptions__.ElmahErrors_Via_User))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery
                        {
                            User = id.User,
                            PageIndex = 1,
                            PageSize = listItemRequest[ElmahUserCompositeModel.__DataOptions__.ElmahErrors_Via_User].PageSize,
                            OrderBys= listItemRequest[ElmahUserCompositeModel.__DataOptions__.ElmahErrors_Via_User].OrderBys,
                            PaginationOption = listItemRequest[ElmahUserCompositeModel.__DataOptions__.ElmahErrors_Via_User].PaginationOption,
                        };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahUserCompositeModel.__DataOptions__.ElmahErrors_Via_User, new Response<PaginationResponse> { Status = response.Status, StatusMessage = response.StatusMessage, ResponseBody = response.Pagination });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ElmahErrors_Via_User = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<ElmahUserCompositeModel.__DataOptions__, Response<PaginationResponse>>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ElmahUserIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserDataModel> input)
        {
            return await _thisRepository.MultiItemsCUD(input);
        }

        public async Task<Response<ElmahUserDataModel>> Update(ElmahUserIdentifier id, ElmahUserDataModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahUserDataModel>> Get(ElmahUserIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahUserDataModel>> Create(ElmahUserDataModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahUserDataModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahUserDataModel { ItemUIStatus______ = ItemUIStatus.New };
        }

        public async Task<Response> Delete(ElmahUserIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


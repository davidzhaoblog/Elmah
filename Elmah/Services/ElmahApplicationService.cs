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

        public async Task<Response> Delete(ElmahApplicationIdModel id)
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

        public async Task<ElmahApplicationCompositeModel> GetCompositeModel(ElmahApplicationIdModel id, ElmahApplicationCompositeDataOptions[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahApplicationCompositeModel();
                failedResponse.Responses.Add(ElmahApplicationCompositeDataOptions.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahApplicationCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahApplicationCompositeDataOptions, Response>();
            responses.TryAdd(ElmahApplicationCompositeDataOptions.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahApplicationCompositeDataOptions.ElmahErrors))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery { Application = id.Application, PageIndex = 1, PageSize = 5, OrderBys="TimeUtc~DESC" };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahApplicationCompositeDataOptions.ElmahErrors, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ElmahErrors = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<ElmahApplicationCompositeDataOptions, Response>(responses);
            return successResponse;
        }

    }
}


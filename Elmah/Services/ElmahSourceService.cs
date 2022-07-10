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
    public class ElmahSourceService
        : IElmahSourceService
    {
        private readonly IElmahSourceRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahSourceService> _logger;

        public ElmahSourceService(
            IElmahSourceRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahSourceService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ElmahSourceCompositeModel> GetCompositeModel(ElmahSourceIdentifier id, ElmahSourceCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahSourceCompositeModel();
                failedResponse.Responses.Add(ElmahSourceCompositeModel.__DataOptions__.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahSourceCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahSourceCompositeModel.__DataOptions__, Response>();
            responses.TryAdd(ElmahSourceCompositeModel.__DataOptions__.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahSourceCompositeModel.__DataOptions__.ElmahErrors))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery { Source = id.Source, PageIndex = 1, PageSize = 5, OrderBys="TimeUtc~DESC" };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahSourceCompositeModel.__DataOptions__.ElmahErrors, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
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
            successResponse.Responses = new Dictionary<ElmahSourceCompositeModel.__DataOptions__, Response>(responses);
            return successResponse;
        }

        public async Task<Response<ElmahSourceModel>> Update(ElmahSourceIdentifier id, ElmahSourceModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahSourceModel>> Get(ElmahSourceIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahSourceModel>> Create(ElmahSourceModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahSourceModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahSourceModel();
        }

        public async Task<Response> Delete(ElmahSourceIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }

    }
}


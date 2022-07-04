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

        public async Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ElmahStatusCodeCompositeModel> GetCompositeModel(ElmahStatusCodeIdModel id, ElmahStatusCodeCompositeDataOptions[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahStatusCodeCompositeModel();
                failedResponse.Responses.Add(ElmahStatusCodeCompositeDataOptions.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahStatusCodeCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahStatusCodeCompositeDataOptions, Response>();
            responses.TryAdd(ElmahStatusCodeCompositeDataOptions.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahStatusCodeCompositeDataOptions.ElmahErrors))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery { StatusCode = id.StatusCode, PageIndex = 1, PageSize = 5, OrderBys="TimeUtc~DESC" };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahStatusCodeCompositeDataOptions.ElmahErrors, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
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
            successResponse.Responses = new Dictionary<ElmahStatusCodeCompositeDataOptions, Response>(responses);
            return successResponse;
        }

        public async Task<Response<ElmahStatusCodeModel>> Update(ElmahStatusCodeModel input)
        {
            return await _thisRepository.Update(input);
        }

        public async Task<Response<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdModel id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahStatusCodeModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahStatusCodeModel();
        }

        public async Task<Response> Delete(ElmahStatusCodeIdModel id)
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


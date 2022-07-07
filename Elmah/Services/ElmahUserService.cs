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

        public async Task<PagedResponse<ElmahUserModel[]>> Search(
            ElmahUserAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ElmahUserCompositeModel> GetCompositeModel(ElmahUserIdentifier id, ElmahUserCompositeDataOptions[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahUserCompositeModel();
                failedResponse.Responses.Add(ElmahUserCompositeDataOptions.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahUserCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahUserCompositeDataOptions, Response>();
            responses.TryAdd(ElmahUserCompositeDataOptions.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahUserCompositeDataOptions.ElmahErrors))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery { User = id.User, PageIndex = 1, PageSize = 5, OrderBys="TimeUtc~DESC" };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahUserCompositeDataOptions.ElmahErrors, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
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
            successResponse.Responses = new Dictionary<ElmahUserCompositeDataOptions, Response>(responses);
            return successResponse;
        }

        public async Task<Response<ElmahUserModel>> Update(ElmahUserIdentifier id, ElmahUserModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahUserModel>> Get(ElmahUserIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahUserModel>> Create(ElmahUserModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahUserModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahUserModel();
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


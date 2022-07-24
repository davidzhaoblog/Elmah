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
    public class ElmahTypeService
        : IElmahTypeService
    {
        private readonly IElmahTypeRepository _thisRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahTypeService> _logger;

        public ElmahTypeService(
            IElmahTypeRepository thisRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahTypeService> logger)
        {
            _thisRepository = thisRepository;
            _serviceScopeFactor = serviceScopeFactor;
            _logger = logger;
        }

        public async Task<PagedResponse<ElmahTypeModel[]>> Search(
            ElmahTypeAdvancedQuery query)
        {
            return await _thisRepository.Search(query);
        }

        public async Task<ElmahTypeCompositeModel> GetCompositeModel(ElmahTypeIdentifier id, ElmahTypeCompositeModel.__DataOptions__[]? dataOptions = null)
        {
            var masterResponse = await this._thisRepository.Get(id);
            if (masterResponse.Status != HttpStatusCode.OK || masterResponse.ResponseBody == null)
            {
                var failedResponse = new ElmahTypeCompositeModel();
                failedResponse.Responses.Add(ElmahTypeCompositeModel.__DataOptions__.__Master__, new Response { Status = masterResponse.Status, StatusMessage = masterResponse.StatusMessage });
                return failedResponse;
            }

            var successResponse = new ElmahTypeCompositeModel { __Master__ = masterResponse.ResponseBody };
            var responses = new ConcurrentDictionary<ElmahTypeCompositeModel.__DataOptions__, Response>();
            responses.TryAdd(ElmahTypeCompositeModel.__DataOptions__.__Master__, new Response { Status = HttpStatusCode.OK });

            var tasks = new List<Task>();

            // 4. ListTable = 4,

            if (dataOptions == null || dataOptions.Contains(ElmahTypeCompositeModel.__DataOptions__.ElmahErrors_Via_Type))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactor.CreateScope())
                    {
                        var _elmahErrorRepository = scope.ServiceProvider.GetRequiredService<IElmahErrorRepository>();
                        var query = new ElmahErrorAdvancedQuery { Type = id.Type, PageIndex = 1, PageSize = 5, OrderBys="TimeUtc~DESC" };
                        var response = await _elmahErrorRepository.Search(query);
                        responses.TryAdd(ElmahTypeCompositeModel.__DataOptions__.ElmahErrors_Via_Type, new Response { Status = response.Status, StatusMessage = response.StatusMessage });
                        if (response.Status == HttpStatusCode.OK)
                        {
                            successResponse.ElmahErrors_Via_Type = response.ResponseBody;
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
            successResponse.Responses = new Dictionary<ElmahTypeCompositeModel.__DataOptions__, Response>(responses);
            return successResponse;
        }

        public async Task<Response> BulkDelete(List<ElmahTypeIdentifier> ids)
        {
            return await _thisRepository.BulkDelete(ids);
        }

        public async Task<Response<ElmahTypeModel>> Update(ElmahTypeIdentifier id, ElmahTypeModel input)
        {
            return await _thisRepository.Update(id, input);
        }

        public async Task<Response<ElmahTypeModel>> Get(ElmahTypeIdentifier id)
        {
            return await _thisRepository.Get(id);
        }

        public async Task<Response<ElmahTypeModel>> Create(ElmahTypeModel input)
        {
            return await _thisRepository.Create(input);
        }

        public ElmahTypeModel GetDefault()
        {
            // TODO: please set default value here
            return new ElmahTypeModel();
        }

        public async Task<Response> Delete(ElmahTypeIdentifier id)
        {
            return await _thisRepository.Delete(id);
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query)
        {
            return await _thisRepository.GetCodeList(query);
        }
    }
}


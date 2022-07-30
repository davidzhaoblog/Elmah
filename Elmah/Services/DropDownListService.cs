using Elmah.Models;
using Elmah.RepositoryContracts;
using Framework.Models;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Linq;

namespace Elmah.Services
{
    public class DropDownListService : Elmah.ServiceContracts.IDropDownListService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ElmahErrorService> _logger;

        public DropDownListService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ElmahErrorService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }

        /// <summary>
        /// This method will be used to get top level dropdownlists from database for Search and Editing, to minimize roundtrip.
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, List<Framework.Models.NameValuePair>>> GetTopLevelDropDownListsFromDatabase(Elmah.Models.Definitions.TopLevelDropDownLists[] options)
        {
            var dropDownLists = new ConcurrentDictionary<string, List<Framework.Models.NameValuePair>>();

            var tasks = new List<Task>();

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahApplication))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _elmahApplicationRepository = scope.ServiceProvider.GetRequiredService<IElmahApplicationRepository>();

                        var oneList = await _elmahApplicationRepository.GetCodeList(new ElmahApplicationAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahApplication.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahHost))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _elmahHostRepository = scope.ServiceProvider.GetRequiredService<IElmahHostRepository>();

                        var oneList = await _elmahHostRepository.GetCodeList(new ElmahHostAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahHost.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahSource))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _elmahSourceRepository = scope.ServiceProvider.GetRequiredService<IElmahSourceRepository>();

                        var oneList = await _elmahSourceRepository.GetCodeList(new ElmahSourceAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahSource.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahStatusCode))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _elmahStatusCodeRepository = scope.ServiceProvider.GetRequiredService<IElmahStatusCodeRepository>();
                        var oneList = await _elmahStatusCodeRepository.GetCodeList(new ElmahStatusCodeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahStatusCode.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahType))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _elmahTypeRepository = scope.ServiceProvider.GetRequiredService<IElmahTypeRepository>();
                        var oneList = await _elmahTypeRepository.GetCodeList(new ElmahTypeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahType.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahUser))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _elmahUserRepository = scope.ServiceProvider.GetRequiredService<IElmahUserRepository>();
                        var oneList = await _elmahUserRepository.GetCodeList(new ElmahUserAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahUser.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
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
            return new Dictionary<string, List<NameValuePair>>(dropDownLists);
        }
    }
}


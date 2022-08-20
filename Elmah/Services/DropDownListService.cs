using Elmah.Models.Definitions;
using Elmah.ServiceContracts;
using Elmah.Models;
using Elmah.RepositoryContracts;
using Framework.Models;

using System.Collections.Concurrent;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Elmah.Services
{
    public class DropDownListService : IDropDownListService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<DropDownListService> _logger;

        public DropDownListService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<DropDownListService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<Dictionary<string, List<NameValuePair>>> GetElmahErrorTopLevelDropDownListsFromDatabase()
        {
            var  _topLevelDropDownLists =
                new Elmah.Models.Definitions.TopLevelDropDownLists[] {
                    TopLevelDropDownLists.ElmahApplication,
                    TopLevelDropDownLists.ElmahHost,
                    TopLevelDropDownLists.ElmahSource,
                    TopLevelDropDownLists.ElmahStatusCode,
                    TopLevelDropDownLists.ElmahType,
                    TopLevelDropDownLists.ElmahUser,
                };
            return await GetTopLevelDropDownListsFromDatabase(_topLevelDropDownLists);
        }

        /// <summary>
        /// This method will be used to get top level dropdownlists from database for Search and Editing, to minimize roundtrip.
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, List<NameValuePair>>> GetTopLevelDropDownListsFromDatabase(TopLevelDropDownLists[] options)
        {
            var dropDownLists = new ConcurrentDictionary<string, List<NameValuePair>>();

            var tasks = new List<Task>();

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ElmahApplication))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var elmahApplicationRepository = scope.ServiceProvider.GetRequiredService<IElmahApplicationRepository>();

                        var oneList = await elmahApplicationRepository.GetCodeList(new ElmahApplicationAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ElmahApplication.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ElmahHost))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var elmahHostRepository = scope.ServiceProvider.GetRequiredService<IElmahHostRepository>();

                        var oneList = await elmahHostRepository.GetCodeList(new ElmahHostAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ElmahHost.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ElmahSource))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var elmahSourceRepository = scope.ServiceProvider.GetRequiredService<IElmahSourceRepository>();

                        var oneList = await elmahSourceRepository.GetCodeList(new ElmahSourceAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ElmahSource.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ElmahStatusCode))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var elmahStatusCodeRepository = scope.ServiceProvider.GetRequiredService<IElmahStatusCodeRepository>();

                        var oneList = await elmahStatusCodeRepository.GetCodeList(new ElmahStatusCodeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ElmahStatusCode.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ElmahType))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var elmahTypeRepository = scope.ServiceProvider.GetRequiredService<IElmahTypeRepository>();

                        var oneList = await elmahTypeRepository.GetCodeList(new ElmahTypeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ElmahType.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ElmahUser))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var elmahUserRepository = scope.ServiceProvider.GetRequiredService<IElmahUserRepository>();

                        var oneList = await elmahUserRepository.GetCodeList(new ElmahUserAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ElmahUser.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
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


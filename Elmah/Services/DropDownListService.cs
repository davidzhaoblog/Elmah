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
        private readonly IElmahApplicationRepository _elmahApplicationRepository;
        private readonly IElmahHostRepository _elmahHostRepository;
        private readonly IElmahSourceRepository _elmahSourceRepository;
        private readonly IElmahStatusCodeRepository _elmahStatusCodeRepository;
        private readonly IElmahTypeRepository _elmahTypeRepository;
        private readonly IElmahUserRepository _elmahUserRepository;
        private readonly IServiceScopeFactory _serviceScopeFactor;
        private readonly ILogger<ElmahErrorService> _logger;

        public DropDownListService(
            IElmahApplicationRepository elmahApplicationRepository,
            IElmahHostRepository elmahHostRepository,
            IElmahSourceRepository elmahSourceRepository,
            IElmahStatusCodeRepository elmahStatusCodeRepository,
            IElmahTypeRepository elmahTypeRepository,
            IElmahUserRepository elmahUserRepository,
            IServiceScopeFactory serviceScopeFactor,
            ILogger<ElmahErrorService> logger)
        {
            _elmahApplicationRepository = elmahApplicationRepository;
            _elmahHostRepository = elmahHostRepository;
            _elmahSourceRepository = elmahSourceRepository;
            _elmahStatusCodeRepository = elmahStatusCodeRepository;
            _elmahTypeRepository = elmahTypeRepository;
            _elmahUserRepository = elmahUserRepository;
            _serviceScopeFactor = serviceScopeFactor;
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
                    var oneList = await _elmahApplicationRepository.GetCodeList(new ElmahApplicationAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                    if (oneList.Status == HttpStatusCode.OK)
                    {
                        dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahApplication.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahHost))
            {
                tasks.Add(Task.Run(async () =>
                {
                    var oneList = await _elmahHostRepository.GetCodeList(new ElmahHostAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                    if (oneList.Status == HttpStatusCode.OK)
                    {
                        dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahHost.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahSource))
            {
                tasks.Add(Task.Run(async () =>
                {
                    var oneList = await _elmahSourceRepository.GetCodeList(new ElmahSourceAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                    if (oneList.Status == HttpStatusCode.OK)
                    {
                        dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahSource.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahStatusCode))
            {
                tasks.Add(Task.Run(async () =>
                {
                    var oneList = await _elmahStatusCodeRepository.GetCodeList(new ElmahStatusCodeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                    if (oneList.Status == HttpStatusCode.OK)
                    {
                        dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahStatusCode.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahType))
            {
                tasks.Add(Task.Run(async () =>
                {
                    var oneList = await _elmahStatusCodeRepository.GetCodeList(new ElmahStatusCodeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                    if (oneList.Status == HttpStatusCode.OK)
                    {
                        dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahType.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                    }
                }));
            }

            if (options == null || options.Any(t => t == Elmah.Models.Definitions.TopLevelDropDownLists.ElmahUser))
            {
                tasks.Add(Task.Run(async () =>
                {
                    var oneList = await _elmahUserRepository.GetCodeList(new ElmahUserAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                    if (oneList.Status == HttpStatusCode.OK)
                    {
                        dropDownLists.TryAdd(Elmah.Models.Definitions.TopLevelDropDownLists.ElmahUser.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
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


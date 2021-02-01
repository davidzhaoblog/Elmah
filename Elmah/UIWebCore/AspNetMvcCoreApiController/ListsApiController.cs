using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Elmah.AspNetMvcCoreApiController
{
    [Route("api/[controller]/[action]")]
    public partial class ListsApiController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public ListsApiController(IServiceProvider serviceProvider, ILogger<ListsApiController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }

        #region GetAllCachedList

        // [Authorize]
        [HttpGet, ActionName("GetAllCachedLists")]
        public async Task<Dictionary<Elmah.EntityContracts.Enums.CacheLists, Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection>> GetAllCachedLists()
        {
            var retval = new ConcurrentDictionary<Elmah.EntityContracts.Enums.CacheLists, Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection>();

            var tasks = new List<Task>();

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                    var result = new Dictionary<Elmah.EntityContracts.Enums.CacheLists, Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection>();

                    foreach (var kv in retval)
                    {
                        result.Add(kv.Key, kv.Value);
                    }

                    return result;
                }
                catch {  }
            }
            return new Dictionary<Elmah.EntityContracts.Enums.CacheLists, Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection>();
        }

        #endregion GetAllCachedList

        #region 1. ELMAH_ErrorList

        // [Authorize]
        [HttpGet, ActionName("ELMAH_ErrorList")]
        public async Task<List<Framework.Models.NameValuePair>> ELMAH_ErrorList(
            string application = default(string)
            , string host = default(string)
            , string source = default(string)
            , int? statusCode = default(int?)
            , string type = default(string)
            , string user = default(string)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string message = default(string);
             string allXml = default(string);
             System.DateTime? timeUtcRangeLow = default(System.DateTime?); System.DateTime? timeUtcRangeHigh = default(System.DateTime?);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IELMAH_ErrorService>();

                return (await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(application), application
                , !string.IsNullOrEmpty(host), host
                , !string.IsNullOrEmpty(source), source
                , statusCode.HasValue, statusCode
                , !string.IsNullOrEmpty(type), type
                , !string.IsNullOrEmpty(user), user
                , !string.IsNullOrEmpty(message), message
                , !string.IsNullOrEmpty(allXml), allXml
                , timeUtcRangeLow.HasValue || timeUtcRangeHigh.HasValue, timeUtcRangeLow.HasValue, timeUtcRangeLow, timeUtcRangeHigh.HasValue, timeUtcRangeHigh
                , currentIndex
                , pageSize
                , queryOrderByExpression)).Message;
            }
        }

        // [Authorize]
        [HttpGet, ActionName("ELMAH_ErrorListMessage")]
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> ELMAH_ErrorListMessage(
            string application = default(string)
            , string host = default(string)
            , string source = default(string)
            , int? statusCode = default(int?)
            , string type = default(string)
            , string user = default(string)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string message = default(string);
             string allXml = default(string);
             System.DateTime? timeUtcRangeLow = default(System.DateTime?); System.DateTime? timeUtcRangeHigh = default(System.DateTime?);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IELMAH_ErrorService>();
                return await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(application), application
                , !string.IsNullOrEmpty(host), host
                , !string.IsNullOrEmpty(source), source
                , statusCode.HasValue, statusCode
                , !string.IsNullOrEmpty(type), type
                , !string.IsNullOrEmpty(user), user
                , !string.IsNullOrEmpty(message), message
                , !string.IsNullOrEmpty(allXml), allXml
                , timeUtcRangeLow.HasValue || timeUtcRangeHigh.HasValue, timeUtcRangeLow.HasValue, timeUtcRangeLow, timeUtcRangeHigh.HasValue, timeUtcRangeHigh
                , currentIndex
                , pageSize
                , queryOrderByExpression);

            }
        }

        #endregion 1. ELMAH_ErrorList

        #region 2. ElmahApplicationList

        // [Authorize]
        [HttpGet, ActionName("ElmahApplicationList")]
        public async Task<List<Framework.Models.NameValuePair>> ElmahApplicationList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string application = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahApplicationService>();

                return (await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(application), application
                , currentIndex
                , pageSize
                , queryOrderByExpression)).Message;
            }
        }

        // [Authorize]
        [HttpGet, ActionName("ElmahApplicationListMessage")]
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> ElmahApplicationListMessage(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string application = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahApplicationService>();
                return await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(application), application
                , currentIndex
                , pageSize
                , queryOrderByExpression);

            }
        }

        #endregion 2. ElmahApplicationList

        #region 3. ElmahHostList

        // [Authorize]
        [HttpGet, ActionName("ElmahHostList")]
        public async Task<List<Framework.Models.NameValuePair>> ElmahHostList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string host = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahHostService>();

                return (await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(host), host
                , currentIndex
                , pageSize
                , queryOrderByExpression)).Message;
            }
        }

        // [Authorize]
        [HttpGet, ActionName("ElmahHostListMessage")]
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> ElmahHostListMessage(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string host = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahHostService>();
                return await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(host), host
                , currentIndex
                , pageSize
                , queryOrderByExpression);

            }
        }

        #endregion 3. ElmahHostList

        #region 4. ElmahSourceList

        // [Authorize]
        [HttpGet, ActionName("ElmahSourceList")]
        public async Task<List<Framework.Models.NameValuePair>> ElmahSourceList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string source = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahSourceService>();

                return (await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(source), source
                , currentIndex
                , pageSize
                , queryOrderByExpression)).Message;
            }
        }

        // [Authorize]
        [HttpGet, ActionName("ElmahSourceListMessage")]
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> ElmahSourceListMessage(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string source = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahSourceService>();
                return await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(source), source
                , currentIndex
                , pageSize
                , queryOrderByExpression);

            }
        }

        #endregion 4. ElmahSourceList

        #region 5. ElmahStatusCodeList

        // [Authorize]
        [HttpGet, ActionName("ElmahStatusCodeList")]
        public async Task<List<Framework.Models.NameValuePair>> ElmahStatusCodeList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string name = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahStatusCodeService>();

                return (await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(name), name
                , currentIndex
                , pageSize
                , queryOrderByExpression)).Message;
            }
        }

        // [Authorize]
        [HttpGet, ActionName("ElmahStatusCodeListMessage")]
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> ElmahStatusCodeListMessage(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string name = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahStatusCodeService>();
                return await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(name), name
                , currentIndex
                , pageSize
                , queryOrderByExpression);

            }
        }

        #endregion 5. ElmahStatusCodeList

        #region 6. ElmahTypeList

        // [Authorize]
        [HttpGet, ActionName("ElmahTypeList")]
        public async Task<List<Framework.Models.NameValuePair>> ElmahTypeList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string type = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahTypeService>();

                return (await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(type), type
                , currentIndex
                , pageSize
                , queryOrderByExpression)).Message;
            }
        }

        // [Authorize]
        [HttpGet, ActionName("ElmahTypeListMessage")]
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> ElmahTypeListMessage(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string type = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahTypeService>();
                return await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(type), type
                , currentIndex
                , pageSize
                , queryOrderByExpression);

            }
        }

        #endregion 6. ElmahTypeList

        #region 7. ElmahUserList

        // [Authorize]
        [HttpGet, ActionName("ElmahUserList")]
        public async Task<List<Framework.Models.NameValuePair>> ElmahUserList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string user = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahUserService>();

                return (await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(user), user
                , currentIndex
                , pageSize
                , queryOrderByExpression)).Message;
            }
        }

        // [Authorize]
        [HttpGet, ActionName("ElmahUserListMessage")]
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> ElmahUserListMessage(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)
        {
            string user = default(string);
            using (var scope = this._serviceProvider.CreateScope())
            {
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahUserService>();
                return await thisService.GetMessageOfNameValuePairByCommon(
                !string.IsNullOrEmpty(user), user
                , currentIndex
                , pageSize
                , queryOrderByExpression);

            }
        }

        #endregion 7. ElmahUserList

    }
}


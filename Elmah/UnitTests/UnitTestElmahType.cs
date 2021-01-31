using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;
using Elmah.MvcCore;
using Elmah.AspNetMvcCoreApiController;
using Elmah.AspNetMvcCoreViewModel;
using Elmah.CoreCommonBLL;
using Elmah.EntityFrameworkContext;
using Elmah.EntityFrameworkDAL;
using Elmah.ViewModelData;

namespace Elmah.UnitTests
{
    public class UnitTestElmahType: Elmah.UnitTests._UnitTestBase
    {

        public UnitTestElmahType()
            : base("/ElmahTypeApi")
        {
        }

        [Fact]
        public async Task TestMethod_GetIndexVM()
        {
            const string ActionName = "GetIndexVM";
            string url = GetHttpRequestUrl(ActionName);

            var vm = new Elmah.ViewModelData.ElmahType.IndexVM();

            var response = await Post<Elmah.ViewModelData.ElmahType.IndexVM>(url, vm);
        }

        /*
        [Fact]
        public async Task TestMethod_Template_ApiController()
        {
            const string ActionName = "GetIndexVM";
            string url = GetHttpRequestUrl(ActionName);

            var vm = new NTierOnTime.ViewModelData.ActivitySummary.IndexVM();

            var response = await Post<NTierOnTime.ViewModelData.ActivitySummary.IndexVM>(url, vm);
        }
        */

        /*
        [Fact]
        public async Task TestMethod_Get_Job_IndexVM()
        {
        }
        */
    }
}


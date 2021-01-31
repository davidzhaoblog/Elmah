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
    // TODO: should do more when generated code.
    public class _UnitTestBase
    {
        internal readonly HttpClient Client;

        internal readonly string RootPath = "/api";
        internal readonly string ControllerName;

        //TODO: How to get token?
        internal readonly string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE0OGYyOGJmLTEzYmUtNGJiZC04YmEwLWM4ODExNTBjMjA3MyIsIm5iZiI6MTU3MzU2NjI1OSwiZXhwIjoxNTc0MTcxMDU5LCJpYXQiOjE1NzM1NjYyNTl9.9iXksaS2u9dpLOFVX08COgTQag0pYB4DvS863spsd0s";

        public _UnitTestBase(string controllerName)
        {
            this.ControllerName = controllerName;
            var appFactory = new WebApplicationFactory<Elmah.MvcCore.Startup>();
            Client = appFactory.CreateClient();
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }

        public string GetHttpRequestUrl(string actionName, Dictionary<string, string> parameters)
        {
            List<string> parametersInList = new List<string>();
            foreach (var kvPair in parameters)
            {
                if (!string.IsNullOrEmpty(kvPair.Value))
                    parametersInList.Add(string.Format("{0}={1}", kvPair.Key, kvPair.Value));
            }
            string parametersInString = String.Join("&", parametersInList);

            return GetHttpRequestUrl(RootPath, ControllerName, actionName, parametersInString);
        }

        public const string ListsApiControllerName = "ListsApi";
        public string GetListsApiHttpRequestUrl(string actionName, Dictionary<string, string> parameters)
        {
            List<string> parametersInList = new List<string>();
            foreach (var kvPair in parameters)
            {
                if (!string.IsNullOrEmpty(kvPair.Value))
                    parametersInList.Add(string.Format("{0}={1}", kvPair.Key, kvPair.Value));
            }
            string parametersInString = String.Join("&", parametersInList);

            return GetHttpRequestUrl(RootPath, ListsApiControllerName, actionName, parametersInString);
        }

        //public string GetHttpRequestUrl(string actionName, string parameters)
        //{
        //    return GetHttpRequestUrl(RootPath, ControllerName, actionName, parameters);
        //}

        public string GetHttpRequestUrl(string actionName)
        {
            return GetHttpRequestUrl(RootPath, ControllerName, actionName, null);
        }

        public static string GetHttpRequestUrl(string rootPath, string controllerName, string actionName, string parameters)
        {
            StringBuilder retval = new StringBuilder(rootPath.TrimEnd('/'));
            if (!string.IsNullOrWhiteSpace(controllerName))
            {
                retval.Append('/');
                retval.Append(controllerName.TrimStart('/').TrimEnd('/'));
            }
            if (!string.IsNullOrWhiteSpace(actionName))
            {
                retval.Append('/');
                retval.Append(actionName.TrimStart('/').TrimEnd('/'));
            }
            if (!string.IsNullOrWhiteSpace(parameters))
            {
                retval.Append('?');
                retval.Append(parameters.TrimStart('?').TrimEnd('/'));
            }
            return retval.ToString();
        }

        public async Task<TResponse> Get<TResponse>(string url)
        {
            var response = await Client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResponse>(content);
                return result;
            }
            else
            {
                return default(TResponse);
            }
        }

        public async Task<TViewModel> GetItemViewModel<TViewModel>(string url)
            where TViewModel : class, Framework.ViewModels.IViewModelItemBase, new()
        {
            var response = await Client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TViewModel>(content);
                return result;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = new TViewModel();
                result.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                result.StatusMessageOfResult = content;
                return result;
            }
        }

        /*
        public async Task<TViewModel> GetEntityRelated<TViewModel>(string url)
            where TViewModel : class, ViewModelEntityRelatedBase, new()
        {
            var response = await Client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TViewModel>(content);
                return result;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = new TViewModel();
                result.StatusOfMasterEntity = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                result.StatusMessageOfMasterEntity = content;
                return result;
            }
        }
        */

        public async Task<TViewModel> Post<TViewModel>(string url, TViewModel vm)
            where TViewModel : class, Framework.ViewModels.IViewModelBase, new()
        {
            string requestJSON = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

            try
            {
                var response = await Client.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<TViewModel>(content);
                    return result;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    vm.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    vm.StatusMessageOfResult = content;
                    return vm;
                }
            }
            catch (Exception ex)
            {
                vm.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                vm.StatusMessageOfResult = ex.Message;
                return vm;
            }
        }

        public async Task<TResponse> PostCommon<TRequest, TResponse>(string url, TRequest request)
        {
            string requestJSON = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<TResponse>(content);
                    return result;
                }
                catch
                {
                    return default(TResponse);
                }
            }
            else
            {
                return default(TResponse);
            }
        }
        /*
        public async Task<TViewModel> PostIViewModelEntityRelatedBase<TViewModel>(string url, TViewModel vm)
            where TViewModel : class, ViewModelEntityRelatedBase, new()
        {
            string requestJSON = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

            try
            {
                var response = await Client.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<TViewModel>(content);
                    return result;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    vm.StatusOfMasterEntity = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    vm.StatusMessageOfMasterEntity = content;
                    return vm;
                }
            }
            catch (Exception ex)
            {
                vm.StatusOfMasterEntity = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                vm.StatusMessageOfMasterEntity = ex.Message;
                return vm;
            }
        }
        */
        public async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request)
            where TResponse : class, Framework.IBusinessLogicLayerResponseMessageBase, new()
        {
            string requestJSON = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResponse>(content);
                return result;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var retval = new TResponse();
                retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                retval.ServerErrorMessage = content;
                return retval;
            }
        }

        public async Task<TResponse> Put<TRequest, TResponse>(string url, TRequest request)
            where TResponse : class, Framework.IBusinessLogicLayerResponseMessageBase, new()
        {
            string requestJSON = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResponse>(content);
                return result;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var retval = new TResponse();
                retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                retval.ServerErrorMessage = content;
                return retval;
            }
        }

        public async Task<TResponse> Delete<TResponse>(string url)
            where TResponse : class, Framework.IBusinessLogicLayerResponseMessageBase, new()
        {
            var response = await Client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResponse>(content);
                return result;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var retval = new TResponse();
                retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                retval.ServerErrorMessage = content;
                return retval;
            }
        }
    }
}


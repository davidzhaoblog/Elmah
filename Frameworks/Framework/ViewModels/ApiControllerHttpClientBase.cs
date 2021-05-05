using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ViewModels
{
    public abstract class ApiControllerHttpClientBase
    {
        public const string WebApiRootUrlAppSettingName = "WebApiRootUrl";
        /// <summary>
        /// Gets or sets the root path.
        /// </summary>
        /// <value>
        /// The root path.
        /// </value>
        ///
        string RootPath { get; set; }

        public abstract string ControllerName { get; }

        protected static HttpClient Client;

        public ApiControllerHttpClientBase(string rootPath, bool useToken = false, string token = null)
        {
            this.RootPath = rootPath;
            Client = new HttpClient(new System.Net.Http.HttpClientHandler());
            if(useToken)
            {
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }

        public string GetHttpRequestUrl(string actionName, Dictionary<string, string> parameters)
        {
            List<string> parametersInList = new List<string>();
            foreach (var kvPair in parameters)
            {
                if (!string.IsNullOrEmpty(kvPair.Key) && !string.IsNullOrEmpty(kvPair.Value))
                {
                    // with [query string name] is an array, otherwise is a single value
                    if (kvPair.Key.StartsWith("[") && kvPair.Key.EndsWith("]"))
                        parametersInList.Add(kvPair.Value); // only value is here. format parametername[index=0,1,...], parameter name is optional if only one array in this web api method, see https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1#collections
                    else if (!kvPair.Key.StartsWith("[") && !kvPair.Key.EndsWith("]")) // single value, should check it is a valid url query string parameter name
                        parametersInList.Add(string.Format("{0}={1}", kvPair.Key, kvPair.Value));
                }
            }
            string parametersInString = String.Join("&", parametersInList);

            return GetHttpRequestUrl(RootPath, ControllerName, actionName, parametersInString);
        }

        public const string ListsApiControllerName = "ListsApi";
        public string GetListsApiHttpRequestUrl(string actionName, Dictionary<string, string> parameters)
        {
            if(parameters == null)
                return GetHttpRequestUrl(RootPath, ListsApiControllerName, actionName, null);

            List<string> parametersInList = new List<string>();
            foreach (var kvPair in parameters)
            {
                if(!string.IsNullOrEmpty(kvPair.Value))
                    parametersInList.Add(string.Format("{0}={1}", kvPair.Key, kvPair.Value));
            }
            string parametersInString = String.Join("&", parametersInList);

            return GetHttpRequestUrl(RootPath, ListsApiControllerName, actionName, parametersInString);
        }

        //public string GetHttpRequestUrl(string actionName, string parameters)
        //{
        //    return GetHttpRequestUrl(RootPath, ControllerName, actionName, parameters);
        //}

        public static string GetArrayParamterString<T>(string name, bool addNameToParameters, List<T> array)
        {
            if (array == null && array.Count == 0)
                return string.Empty;

            if(addNameToParameters)
                return string.Join("&", array.Select(t => $"{name}[{array.IndexOf(t)}]={t}"));
            else
                return string.Join("&", array.Select(t => $"[{array.IndexOf(t)}]={t}"));
        }

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

        public async Task<TViewModel> GetEntityRelated<TViewModel>(string url)
            where TViewModel : class, Framework.ViewModels.IViewModelEntityRelatedBase, new()
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

        public async Task<TViewModel> Post<TViewModel>(string url, TViewModel vm)
            where TViewModel : class, Framework.ViewModels.IViewModelBase, new()
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            string requestJSON = JsonConvert.SerializeObject(vm, Formatting.Indented, jsonSerializerSettings);
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
            catch(Exception ex)
            {
                vm.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                vm.StatusMessageOfResult = ex.Message;
                return vm;
            }
        }

        public async Task<TResponse> PostCommon<TRequest, TResponse>(string url, TRequest request)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            string requestJSON = JsonConvert.SerializeObject(request, Formatting.Indented, jsonSerializerSettings);
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

        public async Task<TViewModel> PostIViewModelEntityRelatedBase<TViewModel>(string url, TViewModel vm)
            where TViewModel : class, Framework.ViewModels.IViewModelEntityRelatedBase, new()
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            string requestJSON = JsonConvert.SerializeObject(vm, Formatting.Indented, jsonSerializerSettings);
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

        public async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request)
            where TResponse : class, Framework.IBusinessLogicLayerResponseMessageBase, new()
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            string requestJSON = JsonConvert.SerializeObject(request, Formatting.Indented, jsonSerializerSettings);
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

        public async Task<TResponse> PutCommon<TRequest, TResponse>(string url, TRequest request)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            string requestJSON = JsonConvert.SerializeObject(request, Formatting.Indented, jsonSerializerSettings);
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(url, httpContent);

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

        public async Task<TResponse> Put<TRequest, TResponse>(string url, TRequest request)
            where TResponse : class, Framework.IBusinessLogicLayerResponseMessageBase, new()
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            string requestJSON = JsonConvert.SerializeObject(request, Formatting.Indented, jsonSerializerSettings);
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

        public async Task<Framework.WebApi.Response> DeleteCommon(string url)
        {
            var response = await Client.DeleteAsync(url);
            return new Framework.WebApi.Response { Status = response.IsSuccessStatusCode ? Framework.Services.BusinessLogicLayerResponseStatus.MessageOK : Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected };
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


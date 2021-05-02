using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Elmah.PetStore.WebApiClient
{
    /// <summary>

    /// </summary>
    public partial class PetApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        // Set to string.Empty
        public override string ControllerName => string.Empty;

        public PetApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        public async Task<Framework.WebApi.Response> DeletePetAsync(string api_key, long petId)
        {
            string url = GetHttpRequestUrl($"/pet/{petId}?api_key={api_key}");
            try
            {
                var response = await DeleteCommon(url);
                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Get.1 FindPetsByStatus /pet/findByStatus

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]>> FindPetsByStatusAsync(string status)
        {
            string url = GetHttpRequestUrl($"/pet/findByStatus?status={status}");
            try
            {
                var response = await Get<Elmah.PetStore.Models.Pet[]>(url);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Get.2 FindPetsByTags /pet/findByTags

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]>> FindPetsByTagsAsync(string[] tags)
        {
            string url = GetHttpRequestUrl($"/pet/findByTags?tags={tags}");
            try
            {
                var response = await Get<Elmah.PetStore.Models.Pet[]>(url);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Get.3 GetPetById /pet/{petId}

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.Pet>> GetPetByIdAsync(long petId)
        {
            string url = GetHttpRequestUrl($"/pet/{petId}");
            try
            {
                var response = await Get<Elmah.PetStore.Models.Pet>(url);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Post.1 AddPet /pet

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.Pet>> AddPetAsync(Elmah.PetStore.Models.Pet item)
        {
            string url = GetHttpRequestUrl($"/pet");
            try
            {
                var response = await PostCommon<Elmah.PetStore.Models.Pet, Elmah.PetStore.Models.Pet>(url, item);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Post.2 UpdatePetWithForm /pet/{petId}

        public async Task<Framework.WebApi.Response> UpdatePetWithFormAsync(long petId, string name, string status)
        {
            string url = GetHttpRequestUrl($"/pet/{petId}?name={name}&status={status}");
            try
            {
                await PostCommon<string>(url, null);

                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Post.3 UploadFile /pet/{petId}/uploadImage

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.ApiResponse>> UploadFileAsync(long petId, string additionalMetadata)
        {
            string url = GetHttpRequestUrl($"/pet/{petId}/uploadImage?additionalMetadata={additionalMetadata}");
            try
            {
                var response = await PostCommon<string, Elmah.PetStore.Models.ApiResponse>(url, null);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.ApiResponse> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.ApiResponse> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.ApiResponse> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Put.1 UpdatePet /pet

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.Pet>> UpdatePetAsync(Elmah.PetStore.Models.Pet item)
        {
            string url = GetHttpRequestUrl($"/pet");
            try
            {
                var response = await PutCommon<Elmah.PetStore.Models.Pet, Elmah.PetStore.Models.Pet>(url, item);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.Pet> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

    }
}


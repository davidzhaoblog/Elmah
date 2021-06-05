import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { FindPetsByStatusParameters, FindPetsByTagsParameters, GetPetByIdParameters, UpdatePetWithFormParameters, UploadFileParameters, DeletePetParameters } from './PetParameters';
import { ApiResponse } from 'src/features/PetStore/ApiResponse';
import { Pet } from 'src/features/PetStore/Pet';

export class PetApi extends ApiBase
{
  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

    // // this middleware is been called right before the http request is made.
    // this.interceptors.request.use((param: AxiosRequestConfig) => ({
    //   ...param,
    // }));
  
    // // this middleware is been called right before the response is get it by the method that triggers the request
    // this.interceptors.response.use((param: AxiosResponse) => ({
    //   ...param
    // }));
    
    // console.log(conf);
  }


  // Get.1 FindPetsByStatus -- /pet/findByStatus
  public FindPetsByStatus = (params: FindPetsByStatusParameters): Promise<Pet[]> => {
    const url = '/pet/findByStatus' + "?" + this.ConvertCriteriaToQueryString({ status:params.status });
    return this.Get<FindPetsByStatusParameters, Pet[]>(url, params);
  }


  // Get.2 FindPetsByTags -- /pet/findByTags
  public FindPetsByTags = (params: FindPetsByTagsParameters): Promise<Pet[]> => {
    const url = '/pet/findByTags' + "?" + this.ConvertCriteriaToQueryString({ tags:params.tags });
    return this.Get<FindPetsByTagsParameters, Pet[]>(url, params);
  }


  // Get.3 GetPetById -- /pet/{petId}
  public GetPetById = (params: GetPetByIdParameters): Promise<Pet> => {
    const url = `/pet/${params.petId}`;
    return this.Get<GetPetByIdParameters, Pet>(url, params);
  }




  // Post.1 AddPet -- /pet
  public AddPet = (requestBody: Pet): Promise<Pet> => {
    const url = '/pet';
    return this.post<Pet, Pet, AxiosResponse<Pet>>(url, requestBody)
      .then(this.success);
  }


  // Post.2 UpdatePetWithForm -- /pet/{petId}
  public UpdatePetWithForm = (params: UpdatePetWithFormParameters): Promise<string> => {
    const url = `/pet/${params.petId}` + "?" + this.ConvertCriteriaToQueryString({ name:params.name, status:params.status });
    return this.post<string, , AxiosResponse<string>>(url, )
      .then(this.success_NoResponseBody);
  }


  // Post.3 UploadFile -- /pet/{petId}/uploadImage
  public UploadFile = (params: UploadFileParameters): Promise<ApiResponse> => {
    const url = `/pet/${params.petId}/uploadImage` + "?" + this.ConvertCriteriaToQueryString({ additionalMetadata:params.additionalMetadata });
    return this.post<ApiResponse, string, AxiosResponse<ApiResponse>>(url, null)
      .then(this.success);
  }


  // Put.1 UpdatePet -- /pet
  public UpdatePet = (requestBody: Pet): Promise<Pet> => {
    const url = '/pet';
    return this.put<Pet, Pet, AxiosResponse<Pet>>(url, requestBody)
      .then(this.success);
  }




  // Delete.1 DeletePet -- /pet/{petId}
  public DeletePet = (params: DeletePetParameters): Promise<string> => {
    const url = `/pet/${params.petId}` + "?" + this.ConvertCriteriaToQueryString({ api_key:params.api_key });
    return this.delete<string, AxiosResponse<string>>(url)
      .then(this.success_NoResponseBody);
  }


}
export const petApi = new PetApi(apiConfig);


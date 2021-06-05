import { AxiosRequestConfig } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { FindPetsByStatusCriteria, FindPetsByTagsCriteria, GetPetByIdCriteria } from './PetCriteria';
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
  public FindPetsByStatus = (criteria: FindPetsByStatusCriteria): Promise<Pet[]> => {
    const url = '/pet/findByStatus' + "?" + this.ConvertCriteriaToQueryString({ status:criteria.status });
    return this.Get<FindPetsByStatusCriteria, Pet[]>(url, criteria);
  }


  // Get.2 FindPetsByTags -- /pet/findByTags
  public FindPetsByTags = (criteria: FindPetsByTagsCriteria): Promise<Pet[]> => {
    const url = '/pet/findByTags' + "?" + this.ConvertCriteriaToQueryString({ tags:criteria.tags });
    return this.Get<FindPetsByTagsCriteria, Pet[]>(url, criteria);
  }


  // Get.3 GetPetById -- /pet/{petId}
  public GetPetById = (criteria: GetPetByIdCriteria): Promise<Pet> => {
    const url = `/pet/${criteria.petId}`;
    return this.Get<GetPetByIdCriteria, Pet>(url, criteria);
  }




  // Post.1 AddPet -- /pet
  public AddPet = (requestBody: Pet): Promise<Pet> => {
    const url = '/pet';
    return this.post<Pet, Pet, AxiosResponse<Pet>>(url, requestBody)
      .then(this.success);
  }


  // Post.2 UpdatePetWithForm -- /pet/{petId}
  public UpdatePetWithForm = (criteria: UpdatePetWithFormCriteria): Promise<string> => {
    const url = `/pet/${criteria.petId}` + "?" + this.ConvertCriteriaToQueryString({ name:criteria.name, status:criteria.status });
    return this.post<string, , AxiosResponse<string>>(url, )
      .then(this.success_NoResponseBody);
  }


  // Post.3 UploadFile -- /pet/{petId}/uploadImage
  public UploadFile = (criteria: UploadFileCriteria): Promise<ApiResponse> => {
    const url = `/pet/${criteria.petId}/uploadImage` + "?" + this.ConvertCriteriaToQueryString({ additionalMetadata:criteria.additionalMetadata });
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
  public DeletePet = (criteria: DeletePetCriteria): Promise<string> => {
    const url = `/pet/${criteria.petId}` + "?" + this.ConvertCriteriaToQueryString({ api_key:criteria.api_key });
    return this.delete<string, AxiosResponse<string>>(url)
      .then(this.success_NoResponseBody);
  }


}
export const petApi = new PetApi(apiConfig);


import { AxiosRequestConfig, AxiosResponse } from 'axios';
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
    const url = '/pet/findByStatus';
    return this.Get<FindPetsByStatusCriteria, AxiosResponse<Pet[]>>(url, criteria)
      .then(this.success);
  }


  // Get.2 FindPetsByTags -- /pet/findByTags
  public FindPetsByTags = (criteria: FindPetsByTagsCriteria): Promise<Pet[]> => {
    const url = '/pet/findByTags';
    return this.Get<FindPetsByTagsCriteria, AxiosResponse<Pet[]>>(url, criteria)
      .then(this.success);
  }


  // Get.3 GetPetById -- /pet/{petId}
  public GetPetById = (criteria: GetPetByIdCriteria): Promise<Pet> => {
    const url = `/pet/${criteria.petId}`;
    return this.Get<GetPetByIdCriteria, AxiosResponse<Pet>>(url, criteria)
      .then(this.success);
  }


}
export const petApi = new PetApi(apiConfig);


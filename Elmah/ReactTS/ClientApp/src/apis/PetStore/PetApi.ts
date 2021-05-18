import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { FindPetsByStatusCriteria, FindPetsByTagsCriteria, GetPetByIdCriteria } from './PetCriteria';
import { Pet, PetIdentifier } from 'src/features/PetStore/Pet';

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


  public FindPetsByStatus = (criteria: FindPetsByStatusCriteria): Promise<Pet[]> => {
    const url = '/pet/findByStatus';
    return this.Get<Pet[], AxiosResponse<Pet[]>>(url, criteria)
      .then(this.success);
  }


  public FindPetsByTags = (criteria: FindPetsByTagsCriteria): Promise<Pet[]> => {
    const url = '/pet/findByTags';
    return this.Get<Pet[], AxiosResponse<Pet[]>>(url, criteria)
      .then(this.success);
  }


  public GetPetById = (criteria: GetPetByIdCriteria): Promise<Pet> => {
    const url = `/pet/${criteria.petId}`;
    return this.Get<Pet, AxiosResponse<Pet>>(url, criteria)
      .then(this.success);
  }


}
export const petApi = new PetApi(apiConfig);


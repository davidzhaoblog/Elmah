import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { FindPetsByStatusCriteria, FindPetsByTagsCriteria, GetPetByIdCriteria } from './PetCriteria';
import { Pet, PetIdentifier } from 'src/features/Elmah/Pet';

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
    const url = '/pet/{petId}';
    return this.Get<Pet, AxiosResponse<Pet>>(url, criteria)
      .then(this.success);
  }


}
export const petApi = new PetApi(apiConfig);


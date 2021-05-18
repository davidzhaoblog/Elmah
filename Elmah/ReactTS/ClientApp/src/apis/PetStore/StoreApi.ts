import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { GetInventoryCriteria, GetOrderByIdCriteria } from './StoreCriteria';
import { Store, StoreIdentifier } from 'src/features/PetStore/Store';

export class StoreApi extends ApiBase
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


  public GetOrderById = (criteria: GetOrderByIdCriteria): Promise<Order> => {
    const url = `/store/order/${criteria.orderId}`;
    return this.Get<Order, AxiosResponse<Order>>(url, criteria)
      .then(this.success);
  }


}
export const storeApi = new StoreApi(apiConfig);


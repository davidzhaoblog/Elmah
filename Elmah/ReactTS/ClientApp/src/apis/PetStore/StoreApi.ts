import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { GetInventoryCriteria, GetOrderByIdCriteria } from './StoreCriteria';
import { Store, StoreIdentifier } from 'src/features/Elmah/Store';

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
    const url = '/store/order/{orderId}';
    return this.Get<Order, AxiosResponse<Order>>(url, criteria)
      .then(this.success);
  }


}
export const storeApi = new StoreApi(apiConfig);


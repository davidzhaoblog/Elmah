import { AxiosRequestConfig } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { GetOrderByIdCriteria } from './StoreCriteria';
import { Order } from 'src/features/PetStore/Order';

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


  // Get.1 GetInventory -- /store/inventory
  public GetInventory = (): Promise<any> => {
    const url = '/store/inventory';
    return this.Get<string, any>(url, null);
  }


  // Get.2 GetOrderById -- /store/order/{orderId}
  public GetOrderById = (criteria: GetOrderByIdCriteria): Promise<Order> => {
    const url = `/store/order/${criteria.orderId}`;
    return this.Get<GetOrderByIdCriteria, Order>(url, criteria);
  }


}
export const storeApi = new StoreApi(apiConfig);


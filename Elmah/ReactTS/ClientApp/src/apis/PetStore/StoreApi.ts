import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { GetOrderByIdParameters, DeleteOrderParameters } from './StoreParameters';
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
  public GetOrderById = (params: GetOrderByIdParameters): Promise<Order> => {
    const url = `/store/order/${params.orderId}`;
    return this.Get<GetOrderByIdParameters, Order>(url, params);
  }




  // Post.1 PlaceOrder -- /store/order
  public PlaceOrder = (requestBody: Order): Promise<Order> => {
    const url = '/store/order';
    return this.post<Order, Order, AxiosResponse<Order>>(url, requestBody)
      .then(this.success);
  }




  // Delete.1 DeleteOrder -- /store/order/{orderId}
  public DeleteOrder = (params: DeleteOrderParameters): Promise<string> => {
    const url = `/store/order/${params.orderId}`;
    return this.delete<string, AxiosResponse<string>>(url)
      .then(this.success_NoResponseBody);
  }


}
export const storeApi = new StoreApi(apiConfig);


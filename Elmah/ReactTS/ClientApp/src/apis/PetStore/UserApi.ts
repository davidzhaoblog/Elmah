import { AxiosRequestConfig } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { LoginUserCriteria, GetUserByNameCriteria } from './UserCriteria';
import { User } from 'src/features/PetStore/User';

export class UserApi extends ApiBase
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


  // Get.1 LoginUser -- /user/login
  public LoginUser = (criteria: LoginUserCriteria): Promise<string> => {
    const url = '/user/login';
    return this.Get<LoginUserCriteria, string>(url, criteria);
  }


  // // Get.2 LogoutUser -- /user/logout
  // public LogoutUser = (): Promise<string> => {
  //   const url = '/user/logout';
  //   return this.Get<string, >(url, null);
  // }


  // Get.3 GetUserByName -- /user/{username}
  public GetUserByName = (criteria: GetUserByNameCriteria): Promise<User> => {
    const url = `/user/${criteria.username}`;
    return this.Get<GetUserByNameCriteria, User>(url, criteria);
  }


}
export const userApi = new UserApi(apiConfig);


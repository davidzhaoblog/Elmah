import { AxiosRequestConfig, AxiosResponse } from 'axios';
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


  public LoginUser = (criteria: LoginUserCriteria): Promise<string> => {
    const url = '/user/login';
    return this.Get<LoginUserCriteria, AxiosResponse<string>>(url, criteria)
      .then(this.success);
  }


  public LogoutUser = (): Promise<string> => {
    const url = '/user/logout';
    return this.Get<string, AxiosResponse>(url, null)
      .then(this.success_NoResponseBody);
  }


  public GetUserByName = (criteria: GetUserByNameCriteria): Promise<User> => {
    const url = `/user/${criteria.username}`;
    return this.Get<GetUserByNameCriteria, AxiosResponse<User>>(url, criteria)
      .then(this.success);
  }


}
export const userApi = new UserApi(apiConfig);


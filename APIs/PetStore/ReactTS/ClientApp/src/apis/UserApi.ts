import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { LoginUserCriteria,LogoutUserCriteria,GetUserByNameCriteria } from './UserCriteria';
import { User, UserIdentifier } from 'src/features/Elmah/User';

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
    return this.Get<string, AxiosResponse<string>>(url, criteria)
      .then(this.success);
  }


  public GetUserByName = (criteria: GetUserByNameCriteria): Promise<User> => {
    const url = '/user/{username}';
    return this.Get<User, AxiosResponse<User>>(url, criteria)
      .then(this.success);
  }


}
export const userApi = new UserApi(apiConfig);


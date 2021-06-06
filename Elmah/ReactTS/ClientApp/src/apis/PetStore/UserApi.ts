import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { GetUserByNameParameters, LoginUserParameters, UpdateUserParameters, DeleteUserParameters } from './UserParameters';
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
  public LoginUser = (params: LoginUserParameters): Promise<string> => {
    const url = '/user/login' + "?" + this.ConvertCriteriaToQueryString({ username:params.username, password:params.password });
    return this.Get<LoginUserParameters, string>(url, params);
  }


  // Get.2 LogoutUser -- /user/logout
  public LogoutUser = (): Promise<string> => {
    const url = '/user/logout';
    return this.Get<string, string>(url, null);
  }


  // Get.3 GetUserByName -- /user/{username}
  public GetUserByName = (params: GetUserByNameParameters): Promise<User> => {
    const url = `/user/${params.username}`;
    return this.Get<GetUserByNameParameters, User>(url, params);
  }




  // Post.1 CreateUser -- /user
  public CreateUser = (requestBody: User): Promise<User> => {
    const url = '/user';
    return this.post<User, User, AxiosResponse<User>>(url, requestBody)
      .then(this.success);
  }


  // Post.2 CreateUsersWithListInput -- /user/createWithList
  public CreateUsersWithListInput = (requestBody: User[]): Promise<User> => {
    const url = '/user/createWithList';
    return this.post<User, User[], AxiosResponse<User>>(url, requestBody)
      .then(this.success);
  }


  // Put.1 UpdateUser -- /user/{username}
  public UpdateUser = (params: UpdateUserParameters, requestBody: User): Promise<string> => {
    const url = `/user/${params.username}`;
    return this.put<string, User, AxiosResponse<string>>(url, requestBody)
      .then(this.success_NoResponseBody);
  }




  // Delete.1 DeleteUser -- /user/{username}
  public DeleteUser = (params: DeleteUserParameters): Promise<string> => {
    const url = `/user/${params.username}`;
    return this.delete<string, AxiosResponse<string>>(url)
      .then(this.success_NoResponseBody);
  }


}
export const userApi = new UserApi(apiConfig);


import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { LoginViewModel, AuthenticationResponse } from 'src/models/AccountModels';
import { apiClientConfig } from 'src/framework/apiClientConfig';
import { ApiClientBase } from 'src/framework/ApiClientBase';


export class AuthenticationApiClient extends ApiClientBase
{
  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

    // this middleware is been called right before the http request is made.
    this.interceptors.request.use((param: AxiosRequestConfig) => ({
      ...param,
    }));
  
    // this middleware is been called right before the response is get it by the method that triggers the request
    this.interceptors.response.use((param: AxiosResponse) => ({
      ...param
    }));

    this.login = this.login.bind(this);
  }

  public login = (credentials: LoginViewModel): Promise<AuthenticationResponse> =>
  {
    return this.post<AuthenticationResponse,LoginViewModel, AxiosResponse<AuthenticationResponse>>("api/AuthenticationApi/Login", credentials)
      .then(res => {
        this.setToken(res.data.token);
        return this.success(res);
      });
  }
}

export const authenticationApiClient = new AuthenticationApiClient(apiClientConfig);
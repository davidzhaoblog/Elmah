import { AxiosRequestConfig } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { NameStringValue } from 'src/framework/Models/NameValuePair';
import { IListQueryString } from 'src/framework/Services/IListQueryString';

export class ListsApi extends ApiBase
{
  protected url_ElmahHostList: string;

  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

	  this.url_ElmahHostList = "api/ListsApi/ElmahHostList";
	
    // // this middleware is been called right before the http request is made.
    // this.interceptors.request.use((param: AxiosRequestConfig) => ({
    //   ...param,
    // }));
  
    // // this middleware is been called right before the response is get it by the method that triggers the request
    // this.interceptors.response.use((param: AxiosResponse) => ({
    //   ...param
    // }));
    
    console.log(conf);
  }

  public GetElmahHostList = (queryString: IListQueryString): Promise<NameStringValue[]> => {
    return this.Get<IListQueryString, NameStringValue[]>(this.url_ElmahHostList, queryString);
  }
}

export const listsApi = new ListsApi(apiConfig);
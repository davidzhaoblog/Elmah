import { AxiosRequestConfig } from 'axios';
import { ApiBase } from 'src/framework/ApiBase';
import { apiConfig } from 'src/framework/apiConfig';
import { NameStringValue } from 'src/framework/Models/NameValuePair';
import { IListQueryString } from 'src/framework/Services/IListQueryString';

export class ListsApi extends ApiBase
{
  protected url_ELMAH_ErrorList: string;
  protected url_ElmahApplicationList: string;
  protected url_ElmahHostList: string;
  protected url_ElmahSourceList: string;
  protected url_ElmahStatusCodeList: string;
  protected url_ElmahTypeList: string;
  protected url_ElmahUserList: string;

  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

	  this.url_ELMAH_ErrorList = "api/ListsApi/ELMAH_ErrorList";
	  this.url_ElmahApplicationList = "api/ListsApi/ElmahApplicationList";
	  this.url_ElmahHostList = "api/ListsApi/ElmahHostList";
	  this.url_ElmahSourceList = "api/ListsApi/ElmahSourceList";
	  this.url_ElmahStatusCodeList = "api/ListsApi/ElmahStatusCodeList";
	  this.url_ElmahTypeList = "api/ListsApi/ElmahTypeList";
	  this.url_ElmahUserList = "api/ListsApi/ElmahUserList";
	
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


  // 1. ELMAH_Error
  public GetELMAH_ErrorList = (queryString: IListQueryString): Promise<NameStringValue[]> => {
    return this.Get<IListQueryString, NameStringValue[]>(this.url_ELMAH_ErrorList, queryString);
  }


  // 2. ElmahApplication
  public GetElmahApplicationList = (queryString: IListQueryString): Promise<NameStringValue[]> => {
    return this.Get<IListQueryString, NameStringValue[]>(this.url_ElmahApplicationList, queryString);
  }


  // 3. ElmahHost
  public GetElmahHostList = (queryString: IListQueryString): Promise<NameStringValue[]> => {
    return this.Get<IListQueryString, NameStringValue[]>(this.url_ElmahHostList, queryString);
  }


  // 4. ElmahSource
  public GetElmahSourceList = (queryString: IListQueryString): Promise<NameStringValue[]> => {
    return this.Get<IListQueryString, NameStringValue[]>(this.url_ElmahSourceList, queryString);
  }


  // 5. ElmahStatusCode
  public GetElmahStatusCodeList = (queryString: IListQueryString): Promise<NameStringValue[]> => {
    return this.Get<IListQueryString, NameStringValue[]>(this.url_ElmahStatusCodeList, queryString);
  }


  // 6. ElmahType
  public GetElmahTypeList = (queryString: IListQueryString): Promise<NameStringValue[]> => {
    return this.Get<IListQueryString, NameStringValue[]>(this.url_ElmahTypeList, queryString);
  }


  // 7. ElmahUser
  public GetElmahUserList = (queryString: IListQueryString): Promise<NameStringValue[]> => {
    return this.Get<IListQueryString, NameStringValue[]>(this.url_ElmahUserList, queryString);
  }


}

export const listsApi = new ListsApi(apiConfig);


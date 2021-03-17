import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { ElmahHost, ElmahHostCommonCriteria, ElmahHostIdentifier } from 'src/features/ElmahHost/Types';

export class ElmahHostApi extends ApiBaseCRUD<
  ElmahHost, IResponse<ElmahHost>, ElmahHostIdentifier, IListRequest<ElmahHostCommonCriteria>, IListResponse<ElmahHost[]>>
{
  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

	this.url_Upsert = "api/ElmahHostApi/UpsertEntity";
    this.url_Delete = "api/ElmahHostApi/DeleteEntity";
    this.url_GetByIdentifier = "api/ElmahHostApi/GetMessageOfEntityByIdentifier";
    this.url_GetIndexVM = "api/ElmahHostApi/GetIndexVM";
	
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

  // protected  ConvertGetByIdentifierCriteriaToQueryString = (params: ElmahHostIdentifier): string => {
  //   return null;
  // } 
}
export const elmahHostApi = new ElmahHostApi(apiConfig);


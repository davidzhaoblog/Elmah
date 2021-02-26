import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { ElmahStatusCode, ElmahStatusCodeCommonCriteria, ElmahStatusCodeIdentifier } from 'src/features/ElmahStatusCode/types';

export class ElmahStatusCodeApi extends ApiBaseCRUD<
  ElmahStatusCode, IResponse<ElmahStatusCode>, ElmahStatusCodeIdentifier, IListRequest<ElmahStatusCodeCommonCriteria>, IListResponse<ElmahStatusCode[]>>
{
  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

	this.url_Upsert = "api/ElmahStatusCodeApi/UpsertEntity";
    this.url_Delete = "api/ElmahStatusCodeApi/DeleteEntity";
    this.url_GetByIdentifier = "api/ElmahStatusCodeApi/GetMessageOfDefaultByIdentifier";
    this.url_GetIndexVM = "api/ElmahStatusCodeApi/GetIndexVM";
	
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

  // protected  ConvertGetByIdentifierCriteriaToQueryString = (params: ElmahStatusCodeIdentifier): string => {
  //   return null;
  // } 
}
export const elmahStatusCodeApi = new ElmahStatusCodeApi(apiConfig);

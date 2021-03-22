import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { ElmahSource, ElmahSourceCommonCriteria, ElmahSourceIdentifier } from 'src/features/ElmahSource/Types';

export class ElmahSourceApi extends ApiBaseCRUD<
  ElmahSource, IResponse<ElmahSource>, ElmahSourceIdentifier, IListRequest<ElmahSourceCommonCriteria>, IListResponse<ElmahSource[]>>
{
  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

	this.url_Upsert = "api/ElmahSourceApi/UpsertEntity";
    this.url_Delete = "api/ElmahSourceApi/DeleteEntity";
    this.url_GetByIdentifier = "api/ElmahSourceApi/GetMessageOfEntityByIdentifier";
    this.url_GetIndexVM = "api/ElmahSourceApi/GetIndexVM";
	
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

  // protected  ConvertGetByIdentifierCriteriaToQueryString = (params: ElmahSourceIdentifier): string => {
  //   return null;
  // } 
}
export const elmahSourceApi = new ElmahSourceApi(apiConfig);


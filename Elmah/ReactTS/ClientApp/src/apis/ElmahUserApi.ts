import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { ElmahUser, ElmahUserCommonCriteria, ElmahUserIdentifier } from 'src/features/ElmahUser/types';

export class ElmahUserApi extends ApiBaseCRUD<
  ElmahUser, IResponse<ElmahUser>, ElmahUserIdentifier, IListRequest<ElmahUserCommonCriteria>, IListResponse<ElmahUser[]>>
{
  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

	this.url_Upsert = "api/ElmahUserApi/UpsertEntity";
    this.url_Delete = "api/ElmahUserApi/DeleteEntity";
    this.url_GetByIdentifier = "api/ElmahUserApi/GetMessageOfDefaultByIdentifier";
    this.url_GetIndexVM = "api/ElmahUserApi/GetIndexVM";
	
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

  // protected  ConvertGetByIdentifierCriteriaToQueryString = (params: ElmahUserIdentifier): string => {
  //   return null;
  // } 
}
export const elmahUserApi = new ElmahUserApi(apiConfig);


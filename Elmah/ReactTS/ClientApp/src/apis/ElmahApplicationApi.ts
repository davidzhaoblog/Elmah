import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { ElmahApplication, ElmahApplicationCommonCriteria, ElmahApplicationIdentifier } from 'src/features/ElmahApplication/types';

export class ElmahApplicationApi extends ApiBaseCRUD<
  ElmahApplication, IResponse<ElmahApplication>, ElmahApplicationIdentifier, IListRequest<ElmahApplicationCommonCriteria>, IListResponse<ElmahApplication[]>>
{
  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

	this.url_Upsert = "api/ElmahApplicationApi/UpsertEntity";
    this.url_Delete = "api/ElmahApplicationApi/DeleteEntity";
    this.url_GetByIdentifier = "api/ElmahApplicationApi/GetMessageOfDefaultByIdentifier";
    this.url_GetIndexVM = "api/ElmahApplicationApi/GetIndexVM";
	
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

  // protected  ConvertGetByIdentifierCriteriaToQueryString = (params: ElmahApplicationIdentifier): string => {
  //   return null;
  // } 
}
export const elmahApplicationApi = new ElmahApplicationApi(apiConfig);


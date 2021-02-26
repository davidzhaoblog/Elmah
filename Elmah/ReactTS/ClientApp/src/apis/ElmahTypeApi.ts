import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { ElmahType, ElmahTypeCommonCriteria, ElmahTypeIdentifier } from 'src/features/ElmahType/types';

export class ElmahTypeApi extends ApiBaseCRUD<
  ElmahType, IResponse<ElmahType>, ElmahTypeIdentifier, IListRequest<ElmahTypeCommonCriteria>, IListResponse<ElmahType[]>>
{
  public constructor(conf?: AxiosRequestConfig) {
    super(conf);

	this.url_Upsert = "api/ElmahTypeApi/UpsertEntity";
    this.url_Delete = "api/ElmahTypeApi/DeleteEntity";
    this.url_GetByIdentifier = "api/ElmahTypeApi/GetMessageOfDefaultByIdentifier";
    this.url_GetIndexVM = "api/ElmahTypeApi/GetIndexVM";
	
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

  // protected  ConvertGetByIdentifierCriteriaToQueryString = (params: ElmahTypeIdentifier): string => {
  //   return null;
  // } 
}
export const elmahTypeApi = new ElmahTypeApi(apiConfig);


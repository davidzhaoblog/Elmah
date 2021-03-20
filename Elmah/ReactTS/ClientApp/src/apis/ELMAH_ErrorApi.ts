import { useSelector } from 'react-redux';

import { AxiosRequestConfig } from 'axios';
import { ApiBaseCRUD } from 'src/framework/ApiBaseCRUD';
import { apiConfig } from 'src/framework/apiConfig';
import { IResponse } from 'src/framework/Services/IResponse';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { IListResponse } from 'src/framework/Services/IListResponse';
import { ELMAH_Error, ELMAH_ErrorCommonCriteria, ELMAH_ErrorIdentifier } from 'src/features/ELMAH_Error/Types';
import { selectToken } from 'src/features/Authentication/authenticationSlice';

export class ELMAH_ErrorApi extends ApiBaseCRUD<
  ELMAH_Error, IResponse<ELMAH_Error>, ELMAH_ErrorIdentifier, IListRequest<ELMAH_ErrorCommonCriteria>, IListResponse<ELMAH_Error[]>>
{
  public constructor(conf?: AxiosRequestConfig, token: string) {
    super(conf, token);

	  this.url_Upsert = "api/ELMAH_ErrorApi/UpsertEntity";
    this.url_Delete = "api/ELMAH_ErrorApi/DeleteEntity";
    this.url_GetByIdentifier = "api/ELMAH_ErrorApi/GetMessageOfDefaultByIdentifier";
    this.url_GetIndexVM = "api/ELMAH_ErrorApi/GetIndexVM";
	
    // // this middleware is been called right before the http request is made.
    // this.interceptors.request.use((param: AxiosRequestConfig) => ({
    //   ...param,
    // }));
  
    // // this middleware is been called right before the response is get it by the method that triggers the request
    // this.interceptors.response.use((param: AxiosResponse) => ({
    //   ...param
    // }));
    
    //console.log(conf);
  }

  // protected  ConvertGetByIdentifierCriteriaToQueryString = (params: ELMAH_ErrorIdentifier): string => {
  //   return null;
  // } 
}

const token = useSelector(selectToken)
export const eLMAH_ErrorApi = new ELMAH_ErrorApi(apiConfig, token);


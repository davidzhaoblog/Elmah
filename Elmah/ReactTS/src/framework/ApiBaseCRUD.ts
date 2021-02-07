// the following code from:
// https://medium.com/@enetoOlveda/how-to-use-axios-typescript-like-a-pro-7c882f71e34a


import { AxiosResponse, AxiosRequestConfig } from 'axios';
import { ApiBase } from './ApiBase';

export class ApiBaseCRUD<TItem, TResponse, TIdentifierCriteria, TIndexVMRequest, TIndexVMResponse> extends ApiBase {

    protected url_Upsert: string;
    protected url_GetByIdentifier: string;
    protected url_GetIndexVM: string;

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
    
        this.Upsert = this.Upsert.bind(this);
        this.GetByIdentifier = this.GetByIdentifier.bind(this);
        this.GetIndexVM = this.GetIndexVM.bind(this);
    }

    public Upsert = (item: TItem): Promise<TResponse> => {
        return this.post<TResponse,TItem, AxiosResponse<TResponse>>(this.url_Upsert, item)
            .then(this.success);
    }
    
    public GetByIdentifier = (params: TIdentifierCriteria): Promise<TResponse> => {
      const url = this.url_GetByIdentifier + "?" + this.ConvertIdentifierCriteriaToQueryString(params);
      return this.get<TResponse, AxiosResponse<TResponse>>(url)
          .then(this.success);
    }
    
    public GetIndexVM = (params: TIndexVMRequest): Promise<TIndexVMResponse> => {
      return this.post<TIndexVMResponse, TIndexVMRequest, AxiosResponse<TIndexVMResponse>>(this.url_GetIndexVM, params)
          .then(this.success);
    }

    protected ConvertIdentifierCriteriaToQueryString = (params: TIdentifierCriteria): string => {
      // https://morioh.com/p/480aef8e92cd
      // Exclude empty or null or undefined properties or fields.
      // ES 6
      return Object.keys(params).filter(key=>params[key]).map(key => key + '=' + params[key]).join('&');

      // // ES 5
      // return Object.keys(params).filter(function(key){ return params[key]; }).map(function(key) {
      //   return key + '=' + params[key]
      // }).join('&');
    }
}

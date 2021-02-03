import { NameValuePair } from './Models/NameValuePair';
import { QueryPagingSetting } from './Queries/QueryPagingSetting';
import { BusinessLogicLayerResponseStatus } from './Services/BusinessLogicLayerResponseStatus';

export interface IListRequest<TSearchCriteria> {
    criteria: TSearchCriteria;
    // orderByList: Array<NameValuePair<numbe>>;
    queryPagingSetting: QueryPagingSetting;
}

export interface IListResponse<TSearchResult> {
    criteria: any;
    result: TSearchResult;
    orderByList: Array<NameValuePair<number>>;
    statusOfResult: BusinessLogicLayerResponseStatus;
    queryPagingSetting: QueryPagingSetting;
    
    error: any;
    tableHeaders: string[];
}

import { QueryOrderBySetting } from './Queries/QueryOrderBySetting';
import { QueryPagingSetting } from './Queries/QueryPagingSetting';
import { ResponseStatus } from './Services/ResponseStatus';

export interface IListRequest<TSearchCriteria> {
    criteria: TSearchCriteria;
    orderBy: QueryOrderBySetting;
    queryPagingSetting: QueryPagingSetting;
}

export interface IListResponse<TSearchResult> {
    criteria: any;
    result: TSearchResult;
    orderBy: QueryOrderBySetting;
    statusOfResult: ResponseStatus;
    queryPagingSetting: QueryPagingSetting;
    
    error: any;
    tableHeaders: string[];
}

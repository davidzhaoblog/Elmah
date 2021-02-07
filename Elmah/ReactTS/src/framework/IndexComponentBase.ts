import { QueryOrderBySetting } from './Queries/QueryOrderBySetting';
import { QueryPagingSetting } from './Queries/QueryPagingSetting';
import { BusinessLogicLayerResponseStatus } from './Services/BusinessLogicLayerResponseStatus';

export interface IListRequest<TSearchCriteria> {
    criteria: TSearchCriteria;
    orderBy: QueryOrderBySetting;
    queryPagingSetting: QueryPagingSetting;
}

export interface IListResponse<TSearchResult> {
    criteria: any;
    result: TSearchResult;
    orderBy: QueryOrderBySetting;
    statusOfResult: BusinessLogicLayerResponseStatus;
    queryPagingSetting: QueryPagingSetting;
    
    error: any;
    tableHeaders: string[];
}

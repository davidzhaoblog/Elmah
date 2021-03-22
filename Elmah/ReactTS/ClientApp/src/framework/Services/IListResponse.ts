import { QueryOrderBySetting } from 'src/framework/Queries/QueryOrderBySetting';
import { QueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';
import { ResponseStatus } from 'src/framework/Services/ResponseStatus';

export interface IListResponse<TSearchResult> {
    criteria: any;
    result: TSearchResult;
    orderBy: QueryOrderBySetting;
    statusOfResult: ResponseStatus;
    queryPagingSetting: QueryPagingSetting;
    
    error: any;
    tableHeaders: string[];
}

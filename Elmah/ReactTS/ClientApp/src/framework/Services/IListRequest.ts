import { QueryOrderBySetting } from 'src/framework/Queries/QueryOrderBySetting';
import { QueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';

export interface IListRequest<TSearchCriteria> {
    criteria: TSearchCriteria;
    orderBy: QueryOrderBySetting;
    queryPagingSetting: QueryPagingSetting;
}

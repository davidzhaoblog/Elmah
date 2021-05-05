import { QueryOrderBySetting } from 'src/framework/Queries/QueryOrderBySetting';
import { QueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';

export interface IListRequest<TSearchCriteria> {
    criteria: TSearchCriteria;
    orderBy: QueryOrderBySetting;
    queryPagingSetting: QueryPagingSetting;
}

export interface IIndexVM {
    criteria: any;
    orderBy: QueryOrderBySetting;
    queryPagingSetting: QueryPagingSetting;
}

export interface IIndexVMRequest {
    criteria: any;
    queryOrderBySettingCollection: QueryOrderBySetting[];
    queryPagingSetting: QueryPagingSetting;
}

import { NameValuePair } from "./Models/NameValuePair";
import { createQueryPagingSetting, QueryPagingSetting } from "./Queries/QueryPagingSetting";
import { BusinessLogicLayerResponseStatus } from "./Services/BusinessLogicLayerResponseStatus";

export interface IListState {
    criteria: any;
    // result: any[];
    orderByList: Array<NameValuePair<number>>;
    statusOfResult: BusinessLogicLayerResponseStatus;
    queryPagingSetting: QueryPagingSetting;

    error: any;
    tableHeaders: string[];
}

export const createListState = (criteria: any, orderByList: Array<NameValuePair<number>>): IListState => {
    return {
        criteria,
        // result: [],
        orderByList,
        statusOfResult: BusinessLogicLayerResponseStatus.NoAction,
        queryPagingSetting: createQueryPagingSetting(10, 1),
        error: null,
        tableHeaders: []
    };
}
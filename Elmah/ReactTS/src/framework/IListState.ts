import { NameNumberValue } from "./Models/NameValuePair";
import { QueryPagingSetting } from "./Queries/QueryPagingSetting";
import { ResponseStatus } from "./Services/ResponseStatus";

export interface IListState {
    criteria: any;
    // result: any[];
    orderByList: Array<NameNumberValue>;
    statusOfResult: ResponseStatus;
    queryPagingSetting: QueryPagingSetting;

    error: any;
    tableHeaders: string[];
}

// export const createListState = (criteria: any, orderByList: Array<NameValuePair<number>>): IListState => {
//     return {
//         criteria,
//         // result: [],
//         orderByList,
//         statusOfResult: BusinessLogicLayerResponseStatus.NoAction,
//         queryPagingSetting: createQueryPagingSetting(10, 1),
//         error: null,
//         tableHeaders: []
//     };
// }
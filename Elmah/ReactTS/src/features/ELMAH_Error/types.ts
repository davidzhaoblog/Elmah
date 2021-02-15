import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ELMAH_Error {
    id: string,
    completed: boolean
    text: string, 
}

export function createELMAH_ErrorDefault(): ELMAH_Error {
    return {
        id: 0,
        text: '',
        completed: false,
    } as unknown as ELMAH_Error;
}

export const orderBys : QueryOrderBySetting[] = [
    { propertyName: 'text', direction: QueryOrderDirections.Ascending, displayName: 'Text Ascending' },
    { propertyName: 'text', direction: QueryOrderDirections.Descending, displayName: 'Text Descending' },
];
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface Todo {
    id: string,
    completed: boolean
    text: string, 
}

export const orderBys : QueryOrderBySetting[] = [
    { propertyName: 'text', direction: QueryOrderDirections.Ascending, displayName: 'Text Ascending' },
    { propertyName: 'text', direction: QueryOrderDirections.Descending, displayName: 'Text Descending' },
];
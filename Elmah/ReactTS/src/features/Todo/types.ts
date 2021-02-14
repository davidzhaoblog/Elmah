import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";
import Todo from "./ListPage";

export interface Todo {
    id: string,
    completed: boolean
    text: string, 
}

export function createTodoDefault(): Todo {
    return {
        id: 0,
        text: '',
        completed: false,
    } as unknown as Todo;
}

export const orderBys : QueryOrderBySetting[] = [
    { propertyName: 'text', direction: QueryOrderDirections.Ascending, displayName: 'Text Ascending' },
    { propertyName: 'text', direction: QueryOrderDirections.Descending, displayName: 'Text Descending' },
];
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";


export interface Tag {
    id: number,
    name: string
}

export function createTagDefault(): Tag {
    return {
		id: 0,
		name: null
	}
}

// Identifier
export interface TagIdentifier {
    id: number
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'id', direction: QueryOrderDirections.Ascending, displayName: 'Id', expression: 'Id~ASC' },
	{ propertyName: 'id', direction: QueryOrderDirections.Descending, displayName: 'Id', expression: 'Id~DESC' },
	{ propertyName: 'name', direction: QueryOrderDirections.Ascending, displayName: 'Name', expression: 'Name~ASC' },
	{ propertyName: 'name', direction: QueryOrderDirections.Descending, displayName: 'Name', expression: 'Name~DESC' }
];





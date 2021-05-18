import { Range } from 'src/framework/Models/Range'
import { convertToDateTimeRange, PreDefinedDateTimeRanges } from 'src/framework/Queries/PreDefinedDateTimeRanges';
import { convertQueryUnitEquals, convertQueryUnitContains, convertQueryUnitRange } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";


export interface Category {
    id: number,
    name: string
}

export function createCategoryDefault(): Category {
    return {
		id: 0,
		name: null
	}
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'id', direction: QueryOrderDirections.Ascending, displayName: 'Id', expression: 'Id~ASC' },
	{ propertyName: 'id', direction: QueryOrderDirections.Descending, displayName: 'Id', expression: 'Id~DESC' },
	{ propertyName: 'name', direction: QueryOrderDirections.Ascending, displayName: 'Name', expression: 'Name~ASC' },
	{ propertyName: 'name', direction: QueryOrderDirections.Descending, displayName: 'Name', expression: 'Name~DESC' }
];

// Identifier
export interface CategoryIdentifier {
    id: number
}

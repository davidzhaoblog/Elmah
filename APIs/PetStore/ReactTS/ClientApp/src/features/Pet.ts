import { Range } from 'src/framework/Models/Range'
import { convertToDateTimeRange, PreDefinedDateTimeRanges } from 'src/framework/Queries/PreDefinedDateTimeRanges';
import { convertQueryUnitEquals, convertQueryUnitContains, convertQueryUnitRange } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";
import { Category } from "./Category";,
import { Tag } from "./Tag";

export interface Pet {
    id: number,
    name: string,
    category: Category,
    photoUrls: string[],
    tags: Tag[],
    status: string
}

export function createPetDefault(): Pet {
    return {
		id: 0,
		name: null,
		category: null,
		photoUrls: [],
		tags: [],
		status: null
	}
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'id', direction: QueryOrderDirections.Ascending, displayName: 'Id', expression: 'Id~ASC' },
	{ propertyName: 'id', direction: QueryOrderDirections.Descending, displayName: 'Id', expression: 'Id~DESC' },
	{ propertyName: 'name', direction: QueryOrderDirections.Ascending, displayName: 'Name', expression: 'Name~ASC' },
	{ propertyName: 'name', direction: QueryOrderDirections.Descending, displayName: 'Name', expression: 'Name~DESC' },
	{ propertyName: 'status', direction: QueryOrderDirections.Ascending, displayName: 'Status', expression: 'Status~ASC' },
	{ propertyName: 'status', direction: QueryOrderDirections.Descending, displayName: 'Status', expression: 'Status~DESC' }
];

// Identifier
export interface PetIdentifier {
    id: number
}


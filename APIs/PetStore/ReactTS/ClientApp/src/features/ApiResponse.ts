import { Range } from 'src/framework/Models/Range'
import { convertToDateTimeRange, PreDefinedDateTimeRanges } from 'src/framework/Queries/PreDefinedDateTimeRanges';
import { convertQueryUnitEquals, convertQueryUnitContains, convertQueryUnitRange } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";


export interface ApiResponse {
    code: number,
    type: string,
    message: string
}

export function createApiResponseDefault(): ApiResponse {
    return {
		code: 0,
		type: null,
		message: null
	}
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'code', direction: QueryOrderDirections.Ascending, displayName: 'Code', expression: 'Code~ASC' },
	{ propertyName: 'code', direction: QueryOrderDirections.Descending, displayName: 'Code', expression: 'Code~DESC' },
	{ propertyName: 'type', direction: QueryOrderDirections.Ascending, displayName: 'Type', expression: 'Type~ASC' },
	{ propertyName: 'type', direction: QueryOrderDirections.Descending, displayName: 'Type', expression: 'Type~DESC' },
	{ propertyName: 'message', direction: QueryOrderDirections.Ascending, displayName: 'Message', expression: 'Message~ASC' },
	{ propertyName: 'message', direction: QueryOrderDirections.Descending, displayName: 'Message', expression: 'Message~DESC' }
];

// Identifier
export interface ApiResponseIdentifier {
    code: number
}


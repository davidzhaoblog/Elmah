import { convertQueryUnitContains } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ElmahStatusCode {
    statusCode: number,
    name: string,
}

export function createElmahStatusCodeDefault(): ElmahStatusCode {
    return {
    statusCode: 0,
    name: null,
    } as unknown as ElmahStatusCode;
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'name', direction: QueryOrderDirections.Ascending, displayName: 'Name', expression: 'Name~ASC' },
	{ propertyName: 'name', direction: QueryOrderDirections.Ascending, displayName: 'Name', expression: 'Name~DESC' },
];

// Identifier
export interface ElmahStatusCodeIdentifier {
	statusCode: number;
}

// CommonCriteria
export interface ElmahStatusCodeCommonCriteria {
	name: string;
	canQueryWhenNoQuery: boolean;
}

export const defaultElmahStatusCodeCommonCriteria = (): ElmahStatusCodeCommonCriteria => {
	return {
		name: null,
		canQueryWhenNoQuery: true
	};
}

export const convertElmahStatusCodeCommonCriteria = (criteria: ElmahStatusCodeCommonCriteria): any => {
	return {
		common: {
			name: convertQueryUnitContains(criteria?.name),
        },
		canQueryWhenNoQuery: true
	};
}


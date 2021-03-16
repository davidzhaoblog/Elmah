import { convertQueryUnitContains } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ElmahType {
    type: string,
}

export function createElmahTypeDefault(): ElmahType {
    return {
    type: null,
    } as unknown as ElmahType;
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'type', direction: QueryOrderDirections.Ascending, displayName: 'Type', expression: 'Type~ASC' },
	{ propertyName: 'type', direction: QueryOrderDirections.Ascending, displayName: 'Type', expression: 'Type~DESC' },
];

// Identifier
export interface ElmahTypeIdentifier {
	type: string;
}

// CommonCriteria
export interface ElmahTypeCommonCriteria {
	type: string;
	canQueryWhenNoQuery: boolean;
}

export const defaultElmahTypeCommonCriteria = (): ElmahTypeCommonCriteria => {
	return {
		type: null,
		canQueryWhenNoQuery: true
	};
}

export const convertElmahTypeCommonCriteria = (criteria: ElmahTypeCommonCriteria): any => {
	return {
		common: {
			type: convertQueryUnitContains(criteria?.type),
        },
		canQueryWhenNoQuery: true
	};
}


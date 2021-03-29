import { convertQueryUnitContains } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ElmahApplication {
    application: string,
}

export function createElmahApplicationDefault(): ElmahApplication {
    return {
    application: null,
    } as unknown as ElmahApplication;
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'application', direction: QueryOrderDirections.Ascending, displayName: 'Application', expression: 'Application~ASC' },
	{ propertyName: 'application', direction: QueryOrderDirections.Descending, displayName: 'Application', expression: 'Application~DESC' },
];

// Identifier
export interface ElmahApplicationIdentifier {
	application: string;
}

// CommonCriteria
export interface ElmahApplicationCommonCriteria {
	application: string;
	stringContains_AllColumns: string;
	canQueryWhenNoQuery: boolean;
}

export const defaultElmahApplicationCommonCriteria = (): ElmahApplicationCommonCriteria => {
	return {
		application: null,
		stringContains_AllColumns: null,
		canQueryWhenNoQuery: true
	};
}

export const convertElmahApplicationCommonCriteria = (criteria: ElmahApplicationCommonCriteria): any => {
	return {
		common: {
			application: convertQueryUnitContains(criteria?.application),
        },
		canQueryWhenNoQuery: true
	};
}


import { convertQueryUnitContains } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ElmahHost {
    host: string,
}

export function createElmahHostDefault(): ElmahHost {
    return {
    host: null,
    } as unknown as ElmahHost;
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'host', direction: QueryOrderDirections.Ascending, displayName: 'Host', expression: 'Host~ASC' },
	{ propertyName: 'host', direction: QueryOrderDirections.Descending, displayName: 'Host', expression: 'Host~DESC' },
];

// Identifier
export interface ElmahHostIdentifier {
	host: string;
}

// CommonCriteria
export interface ElmahHostCommonCriteria {
	host: string;
	stringContains_AllColumns: string;
	canQueryWhenNoQuery: boolean;
}

export const defaultElmahHostCommonCriteria = (): ElmahHostCommonCriteria => {
	return {
		host: null,
		stringContains_AllColumns: null,
		canQueryWhenNoQuery: true
	};
}

export const convertElmahHostCommonCriteria = (criteria: ElmahHostCommonCriteria): any => {
	return {
		common: {
			host: convertQueryUnitContains(criteria?.host),
        },
		canQueryWhenNoQuery: true
	};
}


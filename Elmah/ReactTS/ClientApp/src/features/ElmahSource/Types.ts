import { convertQueryUnitContains } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ElmahSource {
    source: string,
}

export function createElmahSourceDefault(): ElmahSource {
    return {
    source: null,
    } as unknown as ElmahSource;
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'source', direction: QueryOrderDirections.Ascending, displayName: 'Source Ascending' },
	{ propertyName: 'source', direction: QueryOrderDirections.Ascending, displayName: 'Source Descending' }
];

// Identifier
export interface ElmahSourceIdentifier {
	source: string;
}

// CommonCriteria
export interface ElmahSourceCommonCriteria {
	source: string;
	canQueryWhenNoQuery: boolean;
}

export const defaultElmahSourceCommonCriteria = (): ElmahSourceCommonCriteria => {
	return {
		source: null,
		canQueryWhenNoQuery: true
	};
}

export const convertElmahSourceCommonCriteria = (criteria: ElmahSourceCommonCriteria): any => {
	return {
		common: {
			source: convertQueryUnitContains(criteria?.source),
        },
		canQueryWhenNoQuery: true
	};
}


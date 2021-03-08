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
	{ propertyName: 'host', direction: QueryOrderDirections.Ascending, displayName: 'Host Ascending' },
	{ propertyName: 'host', direction: QueryOrderDirections.Ascending, displayName: 'Host Descending' },
];

// Identifier
export interface ElmahHostIdentifier {
	host: string;
}

// CommonCriteria
export interface ElmahHostCommonCriteria {
	host: string;
	canQueryWhenNoQuery: boolean;
}

export const defaultElmahHostCommonCriteria = (): ElmahHostCommonCriteria => {
	return {
		host: null,
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

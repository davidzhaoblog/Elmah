import { convertQueryUnitContains } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ElmahUser {
    user: string,
}

export function createElmahUserDefault(): ElmahUser {
    return {
    user: null,
    } as unknown as ElmahUser;
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'user', direction: QueryOrderDirections.Ascending, displayName: 'User', expression: 'User~ASC' },
	{ propertyName: 'user', direction: QueryOrderDirections.Ascending, displayName: 'User', expression: 'User~DESC' },
];

// Identifier
export interface ElmahUserIdentifier {
	user: string;
}

// CommonCriteria
export interface ElmahUserCommonCriteria {
	user: string;
	canQueryWhenNoQuery: boolean;
}

export const defaultElmahUserCommonCriteria = (): ElmahUserCommonCriteria => {
	return {
		user: null,
		canQueryWhenNoQuery: true
	};
}

export const convertElmahUserCommonCriteria = (criteria: ElmahUserCommonCriteria): any => {
	return {
		common: {
			user: convertQueryUnitContains(criteria?.user),
        },
		canQueryWhenNoQuery: true
	};
}


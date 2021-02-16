import { Range } from "src/framework/Models/Range";
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ELMAH_Error {
    errorId: string,
    application: string, 
    host: string, 
    type: string, 
    source: string, 
    message: string, 
    user: string, 
    statusCode: number, 
    timeUtc: Date, 
    sequence: number, 
    allXml: string, 
}

export function createELMAH_ErrorDefault(): ELMAH_Error {
    return {
        errorId: 0,
        application: '', 
        host: '',
        type: '',
        source: '',
        message: '',
        user: '',
        statusCode: '',
        timeUtc: new Date(), 
        sequence: 0, 
        allXml: '',
    } as unknown as ELMAH_Error;
}

export const orderBys : QueryOrderBySetting[] = [
    { propertyName: 'type', direction: QueryOrderDirections.Ascending, displayName: 'Type Ascending' },
    { propertyName: 'type', direction: QueryOrderDirections.Descending, displayName: 'Type Descending' },
];

export interface ELMAH_ErrorIdentifier {
    errorId: string,
}

// Criteria.1 EntityStatusCodeCommonCriteria
export interface ELMAH_ErrorCommonCriteria {
	common: {
		name: string;
		modifiedDateRange: Range<string>;
		stringContains_AllColumns: string;
	}
	canQueryWhenNoQuery: boolean;
}

export const createELMAH_ErrorCommonCriteria = (): ELMAH_ErrorCommonCriteria => {
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
	return {
		common: {
			name: '',
			modifiedDateRange: {lower: yesterday.toString(), upper: (new Date()).toString()},
			stringContains_AllColumns: ''
		},
		canQueryWhenNoQuery: true
	};
}

export const convertELMAH_ErrorCommonCriteria = (criteria: ELMAH_ErrorCommonCriteria): any => {
	return {
		common: {
			name: {
                valueToBeContained: criteria?.common?.name,
				isToCompare: !criteria?.common?.name
            },
			modifiedDateRange: {
                isToCompareLowerBound: !criteria?.common?.modifiedDateRange?.lower,
                isToIncludeLowerBound: true,
                lowerBound: criteria?.common?.modifiedDateRange?.lower,
                isToCompareUpperBound: !criteria?.common?.modifiedDateRange.upper,
                isToIncludeUpperBound: false,
                upperBound: criteria?.common?.modifiedDateRange?.upper
            },
			stringContains_AllColumns: {
                valueToBeContained: criteria?.common?.stringContains_AllColumns,
				isToCompare: !criteria?.common?.stringContains_AllColumns
            },
		},
		canQueryWhenNoQuery: true
	};
}
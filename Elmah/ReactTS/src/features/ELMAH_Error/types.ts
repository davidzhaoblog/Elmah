import { defaultDateRange } from "src/framework/Models/defaultRanges";
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
    testCheckBox: boolean,
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
        testCheckBox: false,
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
		name: string;
		modifiedDateRange: Range<string>;
		stringContains_AllColumns: string;
	canQueryWhenNoQuery: boolean;
}

export const createELMAH_ErrorCommonCriteria = (): ELMAH_ErrorCommonCriteria => {

	return {
			name: '',
			modifiedDateRange: defaultDateRange(),
			stringContains_AllColumns: '',
		canQueryWhenNoQuery: true
	};
}

export const convertELMAH_ErrorCommonCriteria = (criteria: ELMAH_ErrorCommonCriteria): any => {
	return {
		common: {
			name: {
                valueToBeContained: criteria?.name,
				isToCompare: !criteria?.name
            },
			modifiedDateRange: {
                isToCompareLowerBound: !criteria?.modifiedDateRange?.lower,
                isToIncludeLowerBound: true,
                lowerBound: criteria?.modifiedDateRange?.lower,
                isToCompareUpperBound: !criteria?.modifiedDateRange.upper,
                isToIncludeUpperBound: false,
                upperBound: criteria?.modifiedDateRange?.upper
            },
			stringContains_AllColumns: {
                valueToBeContained: criteria?.stringContains_AllColumns,
				isToCompare: !criteria?.stringContains_AllColumns
            },
		},
		canQueryWhenNoQuery: true
	};
}
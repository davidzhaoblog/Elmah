import { Range } from 'src/framework/Models/Range'
import { defaultDateRange } from 'src/framework/Models/defaultRanges'
import { convertQueryUnitEquals, convertQueryUnitContains, convertQueryUnitRange } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export interface ELMAH_Error {
    elmahApplication_Name: string,
    errorId: any,
    elmahHost_Name: string,
    elmahSource_Name: string,
    elmahStatusCode_Name: string,
    elmahType_Name: string,
    elmahUser_Name: string,
    application: string,
    host: string,
    type: string,
    source: string,
    message: string,
    user: string,
    statusCode: number,
    timeUtc: string,
    sequence: number,
    allXml: string,
}

export function createELMAH_ErrorDefault(): ELMAH_Error {
    return {
    elmahApplication_Name: null,
    errorId: null,
    elmahHost_Name: null,
    elmahSource_Name: null,
    elmahStatusCode_Name: null,
    elmahType_Name: null,
    elmahUser_Name: null,
    application: null,
    host: null,
    type: null,
    source: null,
    message: null,
    user: null,
    statusCode: 0,
    timeUtc: null,
    sequence: 0,
    allXml: null,
    } as unknown as ELMAH_Error;
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'elmahApplication_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahApplication_Name Ascending' },
	{ propertyName: 'elmahApplication_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahApplication_Name Descending' }
	{ propertyName: 'elmahHost_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahHost_Name Ascending' },
	{ propertyName: 'elmahHost_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahHost_Name Descending' }
	{ propertyName: 'elmahSource_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahSource_Name Ascending' },
	{ propertyName: 'elmahSource_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahSource_Name Descending' }
	{ propertyName: 'elmahStatusCode_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahStatusCode_Name Ascending' },
	{ propertyName: 'elmahStatusCode_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahStatusCode_Name Descending' }
	{ propertyName: 'elmahType_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahType_Name Ascending' },
	{ propertyName: 'elmahType_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahType_Name Descending' }
	{ propertyName: 'elmahUser_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahUser_Name Ascending' },
	{ propertyName: 'elmahUser_Name', direction: QueryOrderDirections.Ascending, displayName: 'ElmahUser_Name Descending' }
	{ propertyName: 'application', direction: QueryOrderDirections.Ascending, displayName: 'Application Ascending' },
	{ propertyName: 'application', direction: QueryOrderDirections.Ascending, displayName: 'Application Descending' }
	{ propertyName: 'host', direction: QueryOrderDirections.Ascending, displayName: 'Host Ascending' },
	{ propertyName: 'host', direction: QueryOrderDirections.Ascending, displayName: 'Host Descending' }
	{ propertyName: 'type', direction: QueryOrderDirections.Ascending, displayName: 'Type Ascending' },
	{ propertyName: 'type', direction: QueryOrderDirections.Ascending, displayName: 'Type Descending' }
	{ propertyName: 'source', direction: QueryOrderDirections.Ascending, displayName: 'Source Ascending' },
	{ propertyName: 'source', direction: QueryOrderDirections.Ascending, displayName: 'Source Descending' }
	{ propertyName: 'message', direction: QueryOrderDirections.Ascending, displayName: 'Message Ascending' },
	{ propertyName: 'message', direction: QueryOrderDirections.Ascending, displayName: 'Message Descending' }
	{ propertyName: 'user', direction: QueryOrderDirections.Ascending, displayName: 'User Ascending' },
	{ propertyName: 'user', direction: QueryOrderDirections.Ascending, displayName: 'User Descending' }
	{ propertyName: 'timeUtc', direction: QueryOrderDirections.Ascending, displayName: 'TimeUtc Ascending' },
	{ propertyName: 'timeUtc', direction: QueryOrderDirections.Ascending, displayName: 'TimeUtc Descending' }
	{ propertyName: 'allXml', direction: QueryOrderDirections.Ascending, displayName: 'AllXml Ascending' },
	{ propertyName: 'allXml', direction: QueryOrderDirections.Ascending, displayName: 'AllXml Descending' }
];

// Identifier
export interface ELMAH_ErrorIdentifier {
	errorId: any;
}

// CommonCriteria
export interface ELMAH_ErrorCommonCriteria {
	application: string;
	host: string;
	source: string;
	statusCode: number;
	type: string;
	user: string;
	message: string;
	allXml: string;
	timeUtcRange: Range<string>;
	canQueryWhenNoQuery: boolean;
}

export const defaultELMAH_ErrorCommonCriteria = (): ELMAH_ErrorCommonCriteria => {
	return {
		application: null,
		host: null,
		source: null,
		statusCode: null,
		type: null,
		user: null,
		message: null,
		allXml: null,
		timeUtcRange: defaultDateRange(),
		canQueryWhenNoQuery: true
	};
}

export const convertELMAH_ErrorCommonCriteria = (criteria: ELMAH_ErrorCommonCriteria): any => {
	return {
		common: {
			application: convertQueryUnitEquals(criteria?.application),
			host: convertQueryUnitEquals(criteria?.host),
			source: convertQueryUnitEquals(criteria?.source),
			statusCode: convertQueryUnitEquals(criteria?.statusCode),
			type: convertQueryUnitEquals(criteria?.type),
			user: convertQueryUnitEquals(criteria?.user),
			message: convertQueryUnitContains(criteria?.message),
			allXml: convertQueryUnitContains(criteria?.allXml),
			timeUtcRange: convertQueryUnitRange(criteria?.timeUtcRange),
        },
		canQueryWhenNoQuery: true
	};
}


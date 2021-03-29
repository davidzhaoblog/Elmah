import { Range } from 'src/framework/Models/Range'
import { convertToDateTimeRange, PreDefinedDateTimeRanges } from 'src/framework/Queries/PreDefinedDateTimeRanges';
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
	{ propertyName: 'application', direction: QueryOrderDirections.Ascending, displayName: 'Application', expression: 'Application~ASC' },
	{ propertyName: 'application', direction: QueryOrderDirections.Descending, displayName: 'Application', expression: 'Application~DESC' },
	{ propertyName: 'host', direction: QueryOrderDirections.Ascending, displayName: 'Host', expression: 'Host~ASC' },
	{ propertyName: 'host', direction: QueryOrderDirections.Descending, displayName: 'Host', expression: 'Host~DESC' },
	{ propertyName: 'type', direction: QueryOrderDirections.Ascending, displayName: 'Type', expression: 'Type~ASC' },
	{ propertyName: 'type', direction: QueryOrderDirections.Descending, displayName: 'Type', expression: 'Type~DESC' },
	{ propertyName: 'source', direction: QueryOrderDirections.Ascending, displayName: 'Source', expression: 'Source~ASC' },
	{ propertyName: 'source', direction: QueryOrderDirections.Descending, displayName: 'Source', expression: 'Source~DESC' },
	{ propertyName: 'message', direction: QueryOrderDirections.Ascending, displayName: 'Message', expression: 'Message~ASC' },
	{ propertyName: 'message', direction: QueryOrderDirections.Descending, displayName: 'Message', expression: 'Message~DESC' },
	{ propertyName: 'user', direction: QueryOrderDirections.Ascending, displayName: 'User', expression: 'User~ASC' },
	{ propertyName: 'user', direction: QueryOrderDirections.Descending, displayName: 'User', expression: 'User~DESC' },
	{ propertyName: 'timeUtc', direction: QueryOrderDirections.Ascending, displayName: 'TimeUtc', expression: 'TimeUtc~ASC' },
	{ propertyName: 'timeUtc', direction: QueryOrderDirections.Descending, displayName: 'TimeUtc', expression: 'TimeUtc~DESC' },
	{ propertyName: 'allXml', direction: QueryOrderDirections.Ascending, displayName: 'AllXml', expression: 'AllXml~ASC' },
	{ propertyName: 'allXml', direction: QueryOrderDirections.Descending, displayName: 'AllXml', expression: 'AllXml~DESC' },
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
	timeUtcRangePredefined: PreDefinedDateTimeRanges;
	timeUtcRange: Range<string>;
	stringContains_AllColumns: string;
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
		timeUtcRangePredefined: PreDefinedDateTimeRanges.Unknown,
		timeUtcRange: convertToDateTimeRange(PreDefinedDateTimeRanges.Unknown),
		stringContains_AllColumns: null,
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


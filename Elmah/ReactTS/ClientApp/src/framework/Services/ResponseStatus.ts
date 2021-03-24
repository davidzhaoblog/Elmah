export enum ResponseStatus
{
	NoAction = 'NoAction',
	UIProcessReady = 'UIProcessReady',
	RequestError = 'RequestError',
	MessageOK = 'MessageOK',
	NoValueFromDataSource = 'NoValueFromDataSource',
	MessageErrorDetected = 'MessageErrorDetected',
	YouHaveNoPermissionToView = 'YouHaveNoPermissionToView',
	YouHaveNoPermissionToInsert = 'YouHaveNoPermissionToInsert',
	YouHaveNoPermissionToUpdate = 'YouHaveNoPermissionToUpdate',
	YouHaveNoPermissionToDelete = 'YouHaveNoPermissionToDelete',
	NeedAtLeastOneSearchCondition = 'NeedAtLeastOneSearchCondition',
}

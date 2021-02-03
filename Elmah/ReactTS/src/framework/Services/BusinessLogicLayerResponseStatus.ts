export enum BusinessLogicLayerResponseStatus
{
	NoAction,
	UIProcessReady,
	RequestError,
	MessageOK,
	NoValueFromDataSource,
	MessageErrorDetected,
	YouHaveNoPermissionToView,
	YouHaveNoPermissionToInsert,
	YouHaveNoPermissionToUpdate,
	YouHaveNoPermissionToDelete,
	NeedAtLeastOneSearchCondition,
}

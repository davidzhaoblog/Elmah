import { Range } from 'src/framework/Models/Range'
import { convertToDateTimeRange, PreDefinedDateTimeRanges } from 'src/framework/Queries/PreDefinedDateTimeRanges';
import { convertQueryUnitEquals, convertQueryUnitContains, convertQueryUnitRange } from 'src/framework/Queries/convertQueryUnits'
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";


export interface Order {
    id: number,
    petId: number,
    quantity: number,
    shipDate: Date,
    status: string,
    complete: boolean
}

export function createOrderDefault(): Order {
    return {
		id: 0,
		petId: 0,
		quantity: 0,
		shipDate: null,
		status: null,
		complete: false
	}
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'id', direction: QueryOrderDirections.Ascending, displayName: 'Id', expression: 'Id~ASC' },
	{ propertyName: 'id', direction: QueryOrderDirections.Descending, displayName: 'Id', expression: 'Id~DESC' },
	{ propertyName: 'petId', direction: QueryOrderDirections.Ascending, displayName: 'PetId', expression: 'PetId~ASC' },
	{ propertyName: 'petId', direction: QueryOrderDirections.Descending, displayName: 'PetId', expression: 'PetId~DESC' },
	{ propertyName: 'quantity', direction: QueryOrderDirections.Ascending, displayName: 'Quantity', expression: 'Quantity~ASC' },
	{ propertyName: 'quantity', direction: QueryOrderDirections.Descending, displayName: 'Quantity', expression: 'Quantity~DESC' },
	{ propertyName: 'shipDate', direction: QueryOrderDirections.Ascending, displayName: 'ShipDate', expression: 'ShipDate~ASC' },
	{ propertyName: 'shipDate', direction: QueryOrderDirections.Descending, displayName: 'ShipDate', expression: 'ShipDate~DESC' },
	{ propertyName: 'status', direction: QueryOrderDirections.Ascending, displayName: 'Status', expression: 'Status~ASC' },
	{ propertyName: 'status', direction: QueryOrderDirections.Descending, displayName: 'Status', expression: 'Status~DESC' },
	{ propertyName: 'complete', direction: QueryOrderDirections.Ascending, displayName: 'Complete', expression: 'Complete~ASC' },
	{ propertyName: 'complete', direction: QueryOrderDirections.Descending, displayName: 'Complete', expression: 'Complete~DESC' }
];

// Identifier
export interface OrderIdentifier {
    id: number
}


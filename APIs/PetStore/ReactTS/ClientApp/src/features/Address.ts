import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";


export interface Address {
    street: string,
    city: string,
    state: string,
    zip: string
}

export function createAddressDefault(): Address {
    return {
		street: null,
		city: null,
		state: null,
		zip: null
	}
}

// Identifier
export interface AddressIdentifier {
    street: string
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'street', direction: QueryOrderDirections.Ascending, displayName: 'Street', expression: 'Street~ASC' },
	{ propertyName: 'street', direction: QueryOrderDirections.Descending, displayName: 'Street', expression: 'Street~DESC' },
	{ propertyName: 'city', direction: QueryOrderDirections.Ascending, displayName: 'City', expression: 'City~ASC' },
	{ propertyName: 'city', direction: QueryOrderDirections.Descending, displayName: 'City', expression: 'City~DESC' },
	{ propertyName: 'state', direction: QueryOrderDirections.Ascending, displayName: 'State', expression: 'State~ASC' },
	{ propertyName: 'state', direction: QueryOrderDirections.Descending, displayName: 'State', expression: 'State~DESC' },
	{ propertyName: 'zip', direction: QueryOrderDirections.Ascending, displayName: 'Zip', expression: 'Zip~ASC' },
	{ propertyName: 'zip', direction: QueryOrderDirections.Descending, displayName: 'Zip', expression: 'Zip~DESC' }
];





import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";
import { Address } from "./Address";

export interface Customer {
    id: number,
    username: string,
    address: Address[]
}

export function createCustomerDefault(): Customer {
    return {
		id: 0,
		username: null,
		address: []
	}
}

// Identifier
export interface CustomerIdentifier {
    id: number
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'id', direction: QueryOrderDirections.Ascending, displayName: 'Id', expression: 'Id~ASC' },
	{ propertyName: 'id', direction: QueryOrderDirections.Descending, displayName: 'Id', expression: 'Id~DESC' },
	{ propertyName: 'username', direction: QueryOrderDirections.Ascending, displayName: 'Username', expression: 'Username~ASC' },
	{ propertyName: 'username', direction: QueryOrderDirections.Descending, displayName: 'Username', expression: 'Username~DESC' }
];





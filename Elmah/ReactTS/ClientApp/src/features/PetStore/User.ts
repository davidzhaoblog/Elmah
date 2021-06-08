import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";


export interface User {
    id: number,
    username: string,
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    phone: string,
    userStatus: number
}

export function createUserDefault(): User {
    return {
		id: 0,
		username: null,
		firstName: null,
		lastName: null,
		email: null,
		password: null,
		phone: null,
		userStatus: 0
	}
}

// Identifier
export interface UserIdentifier {
    id: number
}

export const orderBys : QueryOrderBySetting[] = [
	{ propertyName: 'id', direction: QueryOrderDirections.Ascending, displayName: 'Id', expression: 'Id~ASC' },
	{ propertyName: 'id', direction: QueryOrderDirections.Descending, displayName: 'Id', expression: 'Id~DESC' },
	{ propertyName: 'username', direction: QueryOrderDirections.Ascending, displayName: 'Username', expression: 'Username~ASC' },
	{ propertyName: 'username', direction: QueryOrderDirections.Descending, displayName: 'Username', expression: 'Username~DESC' },
	{ propertyName: 'firstName', direction: QueryOrderDirections.Ascending, displayName: 'FirstName', expression: 'FirstName~ASC' },
	{ propertyName: 'firstName', direction: QueryOrderDirections.Descending, displayName: 'FirstName', expression: 'FirstName~DESC' },
	{ propertyName: 'lastName', direction: QueryOrderDirections.Ascending, displayName: 'LastName', expression: 'LastName~ASC' },
	{ propertyName: 'lastName', direction: QueryOrderDirections.Descending, displayName: 'LastName', expression: 'LastName~DESC' },
	{ propertyName: 'email', direction: QueryOrderDirections.Ascending, displayName: 'Email', expression: 'Email~ASC' },
	{ propertyName: 'email', direction: QueryOrderDirections.Descending, displayName: 'Email', expression: 'Email~DESC' },
	{ propertyName: 'password', direction: QueryOrderDirections.Ascending, displayName: 'Password', expression: 'Password~ASC' },
	{ propertyName: 'password', direction: QueryOrderDirections.Descending, displayName: 'Password', expression: 'Password~DESC' },
	{ propertyName: 'phone', direction: QueryOrderDirections.Ascending, displayName: 'Phone', expression: 'Phone~ASC' },
	{ propertyName: 'phone', direction: QueryOrderDirections.Descending, displayName: 'Phone', expression: 'Phone~DESC' },
	{ propertyName: 'userStatus', direction: QueryOrderDirections.Ascending, displayName: 'UserStatus', expression: 'UserStatus~ASC' },
	{ propertyName: 'userStatus', direction: QueryOrderDirections.Descending, displayName: 'UserStatus', expression: 'UserStatus~DESC' }
];





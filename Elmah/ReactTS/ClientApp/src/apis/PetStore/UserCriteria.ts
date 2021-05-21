export interface LoginUserCriteria {
	username: string;
	password: string;
}
export const defaultLoginUserCriteria = (): LoginUserCriteria => {
	username: null,
	password: null
}


export interface GetUserByNameCriteria {
	username: string;
}
export const defaultGetUserByNameCriteria = (): GetUserByNameCriteria => {
	username: null
}





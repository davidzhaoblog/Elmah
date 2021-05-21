export interface LoginUserCriteria {
	username: string;
	password: string;
}
export const defaultLoginUserCriteria = (): LoginUserCriteria => {
	return {
		username: null,
		password: null
	};
}


export interface GetUserByNameCriteria {
	username: string;
}
export const defaultGetUserByNameCriteria = (): GetUserByNameCriteria => {
	return {
		username: null
	};
}





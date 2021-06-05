// Get.1 GetUserByName -- /user/{username}
export interface GetUserByNameParameters {
	username: string;
}
export const defaultGetUserByNameParameters = (): GetUserByNameParameters => {
	return {
		username: null
	};
}


// Get.2 LoginUser -- /user/login
export interface LoginUserParameters {
	username: string;
	password: string;
}
export const defaultLoginUserParameters = (): LoginUserParameters => {
	return {
		username: null,
		password: null
	};
}


// Put.1 UpdateUser -- /user/{username}
export interface UpdateUserParameters {
	username: string;
}
export const defaultUpdateUserParameters = (): UpdateUserParameters => {
	return {
		username: null
	};
}


// Delete.1 DeleteUser -- /user/{username}
export interface DeleteUserParameters {
	username: string;
}
export const defaultDeleteUserParameters = (): DeleteUserParameters => {
	return {
		username: null
	};
}





// Get.1 FindPetsByStatus -- /pet/findByStatus
export interface FindPetsByStatusParameters {
	status: string;
}
export const defaultFindPetsByStatusParameters = (): FindPetsByStatusParameters => {
	return {
		status: "available"
	};
}


// Get.2 FindPetsByTags -- /pet/findByTags
export interface FindPetsByTagsParameters {
	tags: string[];
}
export const defaultFindPetsByTagsParameters = (): FindPetsByTagsParameters => {
	return {
		tags: []
	};
}


// Get.3 GetPetById -- /pet/{petId}
export interface GetPetByIdParameters {
	petId: number;
}
export const defaultGetPetByIdParameters = (): GetPetByIdParameters => {
	return {
		petId: 0
	};
}


// Post.1 UpdatePetWithForm -- /pet/{petId}
export interface UpdatePetWithFormParameters {
	petId: number;
	name: string;
	status: string;
}
export const defaultUpdatePetWithFormParameters = (): UpdatePetWithFormParameters => {
	return {
		petId: 0,
		name: null,
		status: null
	};
}


// Post.2 UploadFile -- /pet/{petId}/uploadImage
export interface UploadFileParameters {
	petId: number;
	additionalMetadata: string;
}
export const defaultUploadFileParameters = (): UploadFileParameters => {
	return {
		petId: 0,
		additionalMetadata: null
	};
}


// Delete.1 DeletePet -- /pet/{petId}
export interface DeletePetParameters {
	api_key: string;
	petId: number;
}
export const defaultDeletePetParameters = (): DeletePetParameters => {
	return {
		api_key: null,
		petId: 0
	};
}





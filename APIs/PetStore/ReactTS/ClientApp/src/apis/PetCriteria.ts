export interface FindPetsByStatusCriteria {
	status: string;
}
export const defaultFindPetsByStatusCriteria = (): FindPetsByStatusCriteria => {
	return {
		status: null
	};
}


export interface FindPetsByTagsCriteria {
	tags: string[];
}
export const defaultFindPetsByTagsCriteria = (): FindPetsByTagsCriteria => {
	return {
		tags: []
	};
}


export interface GetPetByIdCriteria {
	petId: number;
}
export const defaultGetPetByIdCriteria = (): GetPetByIdCriteria => {
	return {
		petId: 0
	};
}





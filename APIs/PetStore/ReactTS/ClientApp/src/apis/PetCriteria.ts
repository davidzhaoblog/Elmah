export interface FindPetsByStatusCriteria {
	status: string;
}
export const defaultFindPetsByStatusCriteria = (): FindPetsByStatusCriteria => {
	status: null
}


export interface FindPetsByTagsCriteria {
	tags: string[];
}
export const defaultFindPetsByTagsCriteria = (): FindPetsByTagsCriteria => {
	tags: []
}


export interface GetPetByIdCriteria {
	petId: number;
}
export const defaultGetPetByIdCriteria = (): GetPetByIdCriteria => {
	petId: 0
}





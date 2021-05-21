export interface GetOrderByIdCriteria {
	orderId: number;
}
export const defaultGetOrderByIdCriteria = (): GetOrderByIdCriteria => {
	return {
		orderId: 0
	};
}





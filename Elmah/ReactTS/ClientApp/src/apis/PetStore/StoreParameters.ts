// Get.1 GetOrderById -- /store/order/{orderId}
export interface GetOrderByIdParameters {
	orderId: number;
}
export const defaultGetOrderByIdParameters = (): GetOrderByIdParameters => {
	return {
		orderId: 0
	};
}


// Delete.1 DeleteOrder -- /store/order/{orderId}
export interface DeleteOrderParameters {
	orderId: number;
}
export const defaultDeleteOrderParameters = (): DeleteOrderParameters => {
	return {
		orderId: 0
	};
}





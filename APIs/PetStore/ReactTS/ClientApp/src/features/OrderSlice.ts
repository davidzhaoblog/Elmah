import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import { closeSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';

import { createQueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';


import { storeApi } from 'src/apis/PetStore/StoreApi';



import { 
	GetOrderByIdParameters, defaultGetOrderByIdParameters,
	DeleteOrderParameters, defaultDeleteOrderParameters 
} from 'src/apis/PetStore/StoreParameters';


import { orderBys, Order } from './Order';

// 1. createEntityAdapter
const entityAdapter = createEntityAdapter<Order>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (item: Order) => item.id,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
  })

// 2. Async Thunks

// 2.Get.1. GetInventory - /store/inventory
export const getInventory = createAsyncThunk(
    'Order.GetInventory',
    async (, {dispatch}) => {
        const response = await storeApi.GetInventory(null).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Get.2. GetOrderById - /store/order/{orderId}
export const getOrderById = createAsyncThunk(
    'Order.GetOrderById',
    async (criteria: GetOrderByIdParameters, {dispatch}) => {
        const response = await storeApi.GetOrderById(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Post.1. PlaceOrder - /store/order
export const placeOrder = createAsyncThunk(
    'Order.PlaceOrder',
    async (requestBody: Order, {dispatch}) => {
        const response = await storeApi.PlaceOrder(null).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Delete.1. DeleteOrder - /store/order/{orderId}
export const deleteOrder = createAsyncThunk(
    'Order.DeleteOrder',
    async (criteria: DeleteOrderParameters, {dispatch}) => {
        const response = await storeApi.DeleteOrder(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)



// 3. slice
const orderSlice = createSlice({
    name: 'orders',
    initialState: entityAdapter.getInitialState({
        orderBy: orderBys.find(x=>x.expression),
		queryPagingSetting: createQueryPagingSetting(10, 1),

        getOrderByIdParameters: defaultGetOrderByIdParameters(),
        deleteOrderParameters: defaultDeleteOrderParameters()


    }), // createEntityAdapter Usage #1
    reducers: {
    },
    // 3.2. extraReducers
    extraReducers: builder => {

		// 3.2.Get.1. GetInventory - /store/inventory
        builder.addCase(getInventory.pending, (state) => {
            // console.log("getInventory.pending");
        });
        builder.addCase(getInventory.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("getInventory.fulfilled");
        });
        builder.addCase(getInventory.rejected, (state, action) => {
            // console.log("getInventory.rejected");
        });


		// 3.2.Get.2. GetOrderById - /store/order/{orderId}
        builder.addCase(getOrderById.pending, (state) => {
            // console.log("getOrderById.pending");
        });
        builder.addCase(getOrderById.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("getOrderById.fulfilled");
        });
        builder.addCase(getOrderById.rejected, (state, action) => {
            // console.log("getOrderById.rejected");
        });


		// 3.2.Post.1. PlaceOrder - /store/order
        builder.addCase(placeOrder.pending, (state) => {
            // console.log("placeOrder.pending");
        });
        builder.addCase(placeOrder.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("placeOrder.fulfilled");
        });
        builder.addCase(placeOrder.rejected, (state, action) => {
            // console.log("placeOrder.rejected");
        });




		// 3.2.Delete.1. DeleteOrder - /store/order/{orderId}
        builder.addCase(deleteOrder.pending, (state) => {
            // console.log("deleteOrder.pending");
        });
        builder.addCase(deleteOrder.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			// TODO: please write your logic to delete one or many entries in entityAdapter
			// entityAdapter.removeOne(state, payload);
            // entityAdapter.removeMany(state, payload);
            // console.log("deleteOrder.fulfilled");
        });
        builder.addCase(deleteOrder.rejected, (state, action) => {
            // console.log("deleteOrder.rejected");
        });



    }
});

 // createEntityAdapter Usage #4, used in ToDoList.tsx
export const orderSelectors = entityAdapter.getSelectors<RootState>(
    state => state.order
  )
export default orderSlice.reducer;


import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import { closeSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';

import { createQueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';


import { storeApi } from 'src/apis/PetStore/StoreApi';



import { GetOrderByIdCriteria, defaultGetOrderByIdCriteria } from 'src/apis/PetStore/StoreCriteria';


import { orderBys, Order } from './Order';

// 1. createEntityAdapter
const entityAdapter = createEntityAdapter<Order>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (item: Order) => item.id,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
  })

// 2. actions can dispatch


// 2.Get.1. GetInventory - /store/inventory
export const getInventory = createAsyncThunk(
    'Order.getInventory',
    async (criteria: any, {dispatch}) => {
        const response = await storeApi.GetInventory().catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Get.2. GetOrderById - /store/order/{orderId}
export const getOrderById = createAsyncThunk(
    'Order.getOrderById',
    async (criteria: GetOrderByIdCriteria, {dispatch}) => {
        const response = await storeApi.GetOrderById(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)



// 3. slice
const orderSlice = createSlice({
    name: 'orders',
    initialState: entityAdapter.getInitialState({
        orderBys: orderBys.find(x=>x.expression),
		queryPagingSetting: createQueryPagingSetting(10, 1),

        getOrderByIdCriteria: defaultGetOrderByIdCriteria()


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


		// 3.2.Get.1. GetOrderById - /store/order/{orderId}
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



    }
});

 // createEntityAdapter Usage #4, used in ToDoList.tsx
export const orderSelectors = entityAdapter.getSelectors<RootState>(
    state => state.order
  )
export default orderSlice.reducer;


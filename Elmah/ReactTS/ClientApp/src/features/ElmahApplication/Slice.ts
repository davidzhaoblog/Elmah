import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import { createQueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';
import { IListRequest } from 'src/framework/Services/IListRequest';
import { closeSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { elmahApplicationApi } from 'src/apis/ElmahApplicationApi';
import { orderBys, ElmahApplication, ElmahApplicationCommonCriteria, createElmahApplicationCommonCriteria, convertElmahApplicationCommonCriteria, ElmahApplicationIdentifier } from './types';

// 1. createEntityAdapter
const entityAdapter = createEntityAdapter<ElmahApplication>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (item: ElmahApplication) => item.errorId,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
  })

// 2. actions can dispatch
// 2.upsert upsert action can dispatch
export const upsert = createAsyncThunk(
    'ElmahApplication.upsert',
    async (payload: ElmahApplication) => {
        const response = await elmahApplicationApi.Upsert(payload);
        return response;
    }
)
// 2.delete delete action can dispatch
export const del = createAsyncThunk(
    'ElmahApplication.del',
    async (payload: ElmahApplication) => {
        const response = await elmahApplicationApi.Delete(payload);
        return response;
    }
)
// 2.getByIdentifier getByIdentifier action can dispatch
export const getByIdentifier = createAsyncThunk(
    'ElmahApplication.getByIdentifier',
    async (payload: ElmahApplicationIdentifier) => {
        const response = await elmahApplicationApi.GetByIdentifier(payload);
        return response;
    }
)
// 2.getIndexVM getIndexVM action can dispatch
export const getIndexVM = createAsyncThunk(
    'ElmahApplication.getIndexVM',
    async (payload: IListRequest<ElmahApplicationCommonCriteria>, {dispatch}) => {
        
        const response = await elmahApplicationApi.GetIndexVM({criteria: convertElmahApplicationCommonCriteria(payload.criteria), ...payload });
        dispatch(closeSpinner());
        return response;
    }
)

// 3. slice
const elmahApplicationSlice = createSlice({
    name: 'elmahApplications',
    initialState: entityAdapter.getInitialState({
        criteria: createElmahApplicationCommonCriteria(),
        orderBy: orderBys.find(x=>x.displayName),
        queryPagingSetting: createQueryPagingSetting(10, 1)
    }), // createEntityAdapter Usage #1
    reducers: {
    },
    // 3.2. extraReducers
    extraReducers: builder => {
        // 3.2.upsert. upsert
        builder.addCase(upsert.pending, (state) => {
            // console.log("upsert.pending");
        });
        builder.addCase(upsert.fulfilled, (state, { payload }) => {
            var { businessLogicLayerResponseStatus, message } = payload;
            if(businessLogicLayerResponseStatus && businessLogicLayerResponseStatus === 'MessageOK')
            {
                entityAdapter.upsertOne(state, message[0]);
            }
            // console.log("upsert.fulfilled");
        });
        builder.addCase(upsert.rejected, (state, action) => {
            // console.log("upsert.rejected");
        });
        // 3.2.delete. delete
        builder.addCase(del.pending, (state) => {
            // console.log("delete.pending");
        });
        builder.addCase(del.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
            var { businessLogicLayerResponseStatus, message } = payload;
            if(businessLogicLayerResponseStatus && businessLogicLayerResponseStatus === 'MessageOK')
            {
                entityAdapter.removeOne(state, message[0].errorId);
            }
            // console.log("delete.fulfilled");
        });
        builder.addCase(del.rejected, (state, action) => {
            // console.log("delete.rejected");
        });
        // 3.2.getByIdentifier. getByIdentifier
        builder.addCase(getByIdentifier.pending, (state) => {
            // console.log("getByIdentifier.pending");
        });
        builder.addCase(getByIdentifier.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
            var { businessLogicLayerResponseStatus, message } = payload;
            if(businessLogicLayerResponseStatus && businessLogicLayerResponseStatus === 'MessageOK')
            {
                entityAdapter.removeOne(state, message[0].errorId);
            }
            // console.log("getByIdentifier.fulfilled");
        });
        builder.addCase(getByIdentifier.rejected, (state, action) => {
            // console.log("getByIdentifier.rejected");
        });
        // 3.2.getIndexVM. getIndexVM
        builder.addCase(getIndexVM.pending, (state) => {
            // console.log("getIndexVM.pending");
        });
        builder.addCase(getIndexVM.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
            var { statusOfResult, result } = payload;
            if(statusOfResult && statusOfResult === 'MessageOK')
            {
                entityAdapter.removeAll(state);
                entityAdapter.upsertMany(state, result);
                state.queryPagingSetting = payload.queryPagingSetting;
                state.orderBy = payload.orderBy;
                // console.log("getIndexVM.fulfilled");
            }
        });
        builder.addCase(getIndexVM.rejected, (state, action) => {
            // console.log("getIndexVM.rejected");
        });
    }
});

 // createEntityAdapter Usage #4, used in ToDoList.tsx
export const elmahApplicationSelectors = entityAdapter.getSelectors<RootState>(
    state => state.elmahApplications
  )
export default elmahApplicationSlice.reducer;


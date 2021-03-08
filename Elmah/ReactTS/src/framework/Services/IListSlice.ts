import { AsyncThunk, createEntityAdapter, createSlice, EntityAdapter, Slice } from "@reduxjs/toolkit";
import { NameStringValue } from "../Models/NameValuePair";

export const createListSlice = (key: string, initState: any, getList: AsyncThunk<NameStringValue[], void, {}>): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    // 1. createEntityAdapter
    const entityAdapter = createEntityAdapter<NameStringValue>({
        // Assume IDs are stored in a field other than `book.id`
        selectId: (item) => item.value,
        // Keep the "all IDs" array sorted based on book titles
        // sortComparer: (a, b) => a.text.localeCompare(b.text), 
    })


    // 1. initialState 
    const _initialState = entityAdapter.getInitialState(initState);

    // 3. slice
    const slice = createSlice({
        name: key,
        initialState: _initialState, // createEntityAdapter Usage #1
        reducers: {
        },
        // 3.2. extraReducers
        extraReducers: builder => {
            // 3.2.getList. getList
            builder.addCase(getList.pending, (state) => {
                // console.log("key + getList.pending");
            });
            builder.addCase(getList.fulfilled, (state, { payload }) => {
                entityAdapter.removeAll(state);
                entityAdapter.upsertMany(state, payload);
                // console.log("key + getList.fulfilled");
                // console.log(payload);
            });
            builder.addCase(getList.rejected, (state, action) => {
                // console.log(key + ".getList.rejected");
            });
        }
    });

    return { entityAdapter, slice };
}

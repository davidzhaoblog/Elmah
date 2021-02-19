import { createAsyncThunk, EntityAdapter, Slice } from '@reduxjs/toolkit';
import { listsApi } from 'src/apis/ListsApi';
import { NameStringValue } from 'src/framework/Models/NameValuePair';
import { createListSlice, } from 'src/framework/Services/IListSlice';
import { RootState } from 'src/store/CombinedReducers';

// 1. keys
const Key_ElmahHost = 'elmahhost';

// 2. AsyncThunk
// 2.1. ElmahHost List
export const getElmahHostList = createAsyncThunk(
    Key_ElmahHost + '.getList',
    async () => {
        const response = await listsApi.GetElmahHostList(null);
        return response;
    }
)

// 3. create...Slice
// 3.1. create...ElmahHostList...Slice
const createElmahHostListSlice = (masterkey: string, initialState: any): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    return createListSlice(masterkey + Key_ElmahHost, null, getElmahHostList);
}

const elmahHostListSlice = createElmahHostListSlice(null, null);
export const elmahHostList = elmahHostListSlice.slice.reducer;

export const elmahHostListSelector = elmahHostListSlice.entityAdapter.getSelectors<RootState>(
    state => state.elmahHostList
)
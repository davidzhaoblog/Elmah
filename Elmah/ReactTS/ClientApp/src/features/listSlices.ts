import { createAsyncThunk, EntityAdapter, Slice } from '@reduxjs/toolkit';
import { listsApi } from 'src/apis/ListsApi';
import { NameStringValue } from 'src/framework/Models/NameValuePair';
import { createListSlice } from 'src/framework/Services/IListSlice';
import { RootState } from 'src/store/CombinedReducers';

// 1. keys
// 1. 1. ELMAH_Error
const Key_ELMAH_Error = 'elmah_error';
// 1. 2. ElmahApplication
const Key_ElmahApplication = 'elmahapplication';
// 1. 3. ElmahHost
const Key_ElmahHost = 'elmahhost';
// 1. 4. ElmahSource
const Key_ElmahSource = 'elmahsource';
// 1. 5. ElmahStatusCode
const Key_ElmahStatusCode = 'elmahstatuscode';
// 1. 6. ElmahType
const Key_ElmahType = 'elmahtype';
// 1. 7. ElmahUser
const Key_ElmahUser = 'elmahuser';


// 2. AsyncThunk

// 2.1. ELMAH_Error List
export const getELMAH_ErrorList = createAsyncThunk(
    Key_ELMAH_Error + '.getList',
    async () => {
		// TODO: please modify here if you have more parameters, for autocomplete or cascading dropdowns.
        const response = await listsApi.GetELMAH_ErrorList(null);
        return response;
    }
)


// 2.2. ElmahApplication List
export const getElmahApplicationList = createAsyncThunk(
    Key_ElmahApplication + '.getList',
    async () => {
		// TODO: please modify here if you have more parameters, for autocomplete or cascading dropdowns.
        const response = await listsApi.GetElmahApplicationList(null);
        return response;
    }
)


// 2.3. ElmahHost List
export const getElmahHostList = createAsyncThunk(
    Key_ElmahHost + '.getList',
    async () => {
		// TODO: please modify here if you have more parameters, for autocomplete or cascading dropdowns.
        const response = await listsApi.GetElmahHostList(null);
        return response;
    }
)


// 2.4. ElmahSource List
export const getElmahSourceList = createAsyncThunk(
    Key_ElmahSource + '.getList',
    async () => {
		// TODO: please modify here if you have more parameters, for autocomplete or cascading dropdowns.
        const response = await listsApi.GetElmahSourceList(null);
        return response;
    }
)


// 2.5. ElmahStatusCode List
export const getElmahStatusCodeList = createAsyncThunk(
    Key_ElmahStatusCode + '.getList',
    async () => {
		// TODO: please modify here if you have more parameters, for autocomplete or cascading dropdowns.
        const response = await listsApi.GetElmahStatusCodeList(null);
        return response;
    }
)


// 2.6. ElmahType List
export const getElmahTypeList = createAsyncThunk(
    Key_ElmahType + '.getList',
    async () => {
		// TODO: please modify here if you have more parameters, for autocomplete or cascading dropdowns.
        const response = await listsApi.GetElmahTypeList(null);
        return response;
    }
)


// 2.7. ElmahUser List
export const getElmahUserList = createAsyncThunk(
    Key_ElmahUser + '.getList',
    async () => {
		// TODO: please modify here if you have more parameters, for autocomplete or cascading dropdowns.
        const response = await listsApi.GetElmahUserList(null);
        return response;
    }
)



// 3. create...Slice

// 3.1.1. create...ELMAH_ErrorList...Slice
const createELMAH_ErrorListSlice = (masterkey: string, initialState: any): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    return createListSlice(masterkey + Key_ELMAH_Error, null, getELMAH_ErrorList);
}

const eLMAH_ErrorListSlice = createELMAH_ErrorListSlice(null, null);
export const eLMAH_ErrorList = eLMAH_ErrorListSlice.slice.reducer;

// 3.1.2. ELMAH_ErrorListSelector
export const eLMAH_ErrorListSelector = eLMAH_ErrorListSlice.entityAdapter.getSelectors<RootState>(
    state => state.eLMAH_ErrorList
)


// 3.2.1. create...ElmahApplicationList...Slice
const createElmahApplicationListSlice = (masterkey: string, initialState: any): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    return createListSlice(masterkey + Key_ElmahApplication, null, getElmahApplicationList);
}

const elmahApplicationListSlice = createElmahApplicationListSlice(null, null);
export const elmahApplicationList = elmahApplicationListSlice.slice.reducer;

// 3.2.2. ElmahApplicationListSelector
export const elmahApplicationListSelector = elmahApplicationListSlice.entityAdapter.getSelectors<RootState>(
    state => state.elmahApplicationList
)


// 3.3.1. create...ElmahHostList...Slice
const createElmahHostListSlice = (masterkey: string, initialState: any): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    return createListSlice(masterkey + Key_ElmahHost, null, getElmahHostList);
}

const elmahHostListSlice = createElmahHostListSlice(null, null);
export const elmahHostList = elmahHostListSlice.slice.reducer;

// 3.3.2. ElmahHostListSelector
export const elmahHostListSelector = elmahHostListSlice.entityAdapter.getSelectors<RootState>(
    state => state.elmahHostList
)


// 3.4.1. create...ElmahSourceList...Slice
const createElmahSourceListSlice = (masterkey: string, initialState: any): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    return createListSlice(masterkey + Key_ElmahSource, null, getElmahSourceList);
}

const elmahSourceListSlice = createElmahSourceListSlice(null, null);
export const elmahSourceList = elmahSourceListSlice.slice.reducer;

// 3.4.2. ElmahSourceListSelector
export const elmahSourceListSelector = elmahSourceListSlice.entityAdapter.getSelectors<RootState>(
    state => state.elmahSourceList
)


// 3.5.1. create...ElmahStatusCodeList...Slice
const createElmahStatusCodeListSlice = (masterkey: string, initialState: any): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    return createListSlice(masterkey + Key_ElmahStatusCode, null, getElmahStatusCodeList);
}

const elmahStatusCodeListSlice = createElmahStatusCodeListSlice(null, null);
export const elmahStatusCodeList = elmahStatusCodeListSlice.slice.reducer;

// 3.5.2. ElmahStatusCodeListSelector
export const elmahStatusCodeListSelector = elmahStatusCodeListSlice.entityAdapter.getSelectors<RootState>(
    state => state.elmahStatusCodeList
)


// 3.6.1. create...ElmahTypeList...Slice
const createElmahTypeListSlice = (masterkey: string, initialState: any): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    return createListSlice(masterkey + Key_ElmahType, null, getElmahTypeList);
}

const elmahTypeListSlice = createElmahTypeListSlice(null, null);
export const elmahTypeList = elmahTypeListSlice.slice.reducer;

// 3.6.2. ElmahTypeListSelector
export const elmahTypeListSelector = elmahTypeListSlice.entityAdapter.getSelectors<RootState>(
    state => state.elmahTypeList
)


// 3.7.1. create...ElmahUserList...Slice
const createElmahUserListSlice = (masterkey: string, initialState: any): { entityAdapter: EntityAdapter<NameStringValue>, slice: Slice<any, {}, string> } => {
    return createListSlice(masterkey + Key_ElmahUser, null, getElmahUserList);
}

const elmahUserListSlice = createElmahUserListSlice(null, null);
export const elmahUserList = elmahUserListSlice.slice.reducer;

// 3.7.2. ElmahUserListSelector
export const elmahUserListSelector = elmahUserListSlice.entityAdapter.getSelectors<RootState>(
    state => state.elmahUserList
)





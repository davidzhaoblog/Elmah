import { combineReducers } from "@reduxjs/toolkit";

import app from "src/layout/appSlice";
import auth from "src/features/Authentication/authenticationSlice";
import todos from 'src/features/Todo/todoSlice';
import visibilityFilter from 'src/features/visibilityFilter/visibilityFilterSlice';

import eLMAH_Error from 'src/features/ELMAH_Error/Slice';
import elmahApplication from 'src/features/ElmahApplication/Slice';
import elmahHost from 'src/features/ElmahHost/Slice';
import elmahSource from 'src/features/ElmahSource/Slice';
import elmahStatusCode from 'src/features/ElmahStatusCode/Slice';
import elmahType from 'src/features/ElmahType/Slice';
import elmahUser from 'src/features/ElmahUser/Slice';

import pet from 'src/features/PetStore/Pet/Slice';
import user from 'src/features/PetStore/User/Slice';

import { 
    eLMAH_ErrorList,
    elmahApplicationList,
    elmahHostList,
    elmahSourceList,
    elmahStatusCodeList,
    elmahTypeList,
    elmahUserList
 } from 'src/features/listSlices';

export const reducers = combineReducers({
    app: app,
    auth: auth,
	// todo is an example
    todos: todos,
    visibilityFilter: visibilityFilter,

    user: user,
    pet: pet,

    eLMAH_Error: eLMAH_Error,
    elmahApplication: elmahApplication,
    elmahHost: elmahHost,
    elmahSource: elmahSource,
    elmahStatusCode: elmahStatusCode,
    elmahType: elmahType,
    elmahUser: elmahUser,

    eLMAH_ErrorList,
    elmahApplicationList,
    elmahHostList,
    elmahSourceList,
    elmahStatusCodeList,
    elmahTypeList,
    elmahUserList,
});

export type RootState = ReturnType<typeof reducers>


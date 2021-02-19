import { combineReducers } from "@reduxjs/toolkit";

import app from "src/layout/appSlice";
import auth from "src/features/Authentication/authenticationSlice";

import todos from 'src/features/Todo/todoSlice';
import eLMAH_Errors from 'src/features/ELMAH_Error/elmah_ErrorSlice';

import visibilityFilter from 'src/features/visibilityFilter/visibilityFilterSlice';
import { elmahHostList } from 'src/features/listSlices';

export const reducers = combineReducers({
    app: app,
    auth: auth,
    
    todos: todos,

    eLMAH_Errors: eLMAH_Errors,

    elmahHostList,

    visibilityFilter: visibilityFilter,
});

export type RootState = ReturnType<typeof reducers>
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



export const reducers = combineReducers({
    app: app,
    auth: auth,
	// todo is an example
    todos: todos,
    visibilityFilter: visibilityFilter,


    eLMAH_Error: eLMAH_Error,


    elmahApplication: elmahApplication,


    elmahHost: elmahHost,


    elmahSource: elmahSource,


    elmahStatusCode: elmahStatusCode,


    elmahType: elmahType,


    elmahUser: elmahUser,


});

export type RootState = ReturnType<typeof reducers>


import { combineReducers } from "@reduxjs/toolkit";

import app from "src/layout/appSlice";
import auth from "src/features/Authentication/authenticationSlice";

import todos from 'src/features/Todo/todoSlice';

import visibilityFilter from 'src/features/visibilityFilter/visibilityFilterSlice';

export const reducers = combineReducers({
    app: app,
    auth: auth,
    
    todos: todos,

    visibilityFilter: visibilityFilter,
});

export type RootState = ReturnType<typeof reducers>
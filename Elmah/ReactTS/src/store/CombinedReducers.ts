import { combineReducers } from "@reduxjs/toolkit";

import visibilityFilter from 'src/features/visibilityFilter/visibilityFilterSlice';
import auth from "src/features/Authentication/authenticationSlice";

import app from "src/layout/appSlice";

export const reducers = combineReducers({
    app: app,
    auth: auth,

    visibilityFilter: visibilityFilter,
});

export type RootState = ReturnType<typeof reducers>
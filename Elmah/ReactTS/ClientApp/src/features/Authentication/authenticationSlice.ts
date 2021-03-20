// https://stackoverflow.com/questions/62020623/createasyncthunk-and-writing-reducer-login-with-redux-toolkit
// https://stackoverflow.com/questions/64287428/call-reducer-from-a-different-slice-redux-toolkit

import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
//import { authenticationApiClient } from "src/apiCients/AuthenticationApiClient";
import { closeSpinner } from "src/layout/appSlice";
import { LoginViewModel } from "src/models/AccountModels";
import { RootState } from "src/store/CombinedReducers";

export const login = createAsyncThunk(
    'login',
    async ({ email, password, from }: LoginViewModel, {dispatch}) => {
        // TODO: the following link can authenticate with Asp.Net Core built in Identity Framework
        // const response = await authenticationApiClient.login({ email: email, password: password, rememberMe: true, from });
        const response = {
            succeeded: true,
            isLockedOut: false,
            isNotAllowed: false,
            requiresTwoFactor: false,
            token: 'Fake Token',
            expiresIn: 7,
            refreshToken: 'Fake Rereshed Token',
            entityID: 'null',
            roles: ['admin']
        }
        localStorage.setItem('user', JSON.stringify(response));
        dispatch(closeSpinner());
        return response;
    }
)

export const logout = createAsyncThunk(
    'logout',
    async () => {
        new Promise(r => setTimeout(r, 5000));
    }
)

const authSlice = createSlice({
    name: "authSlice",
    initialState: {
        isLoggingIn: false,
        isLoggingOut: false,
        isVerifying: false,
        loginError: false,
        logoutError: false,
        isAuthenticated: false,
        token: null,
        user: {},
    },
    reducers: {
        /* any other state updates here */

    },
    extraReducers: builder => {
        builder.addCase(login.pending, (state) => {
            state.isLoggingIn = true;
            state.isAuthenticated = false;
            state.loginError = false;
            // console.log("login.pending");
        });
        builder.addCase(login.fulfilled, (state, { payload }) => {
            state.isLoggingIn = false;
            state.isAuthenticated = true;
            state.loginError = false;
            state.token = payload.token;
            // console.log("login.fulfilled");
        });
        builder.addCase(login.rejected, (state, action) => {
            state.isLoggingIn = false;
            state.isAuthenticated = false;
            state.loginError = true;
            state.token = null;
            // console.log("login.rejected");
        });
        builder.addCase(logout.pending, (state) => {
            state.isLoggingOut = true;
            // console.log("logout.pending");
        });
        builder.addCase(logout.fulfilled, (state, { payload }) => {
            state.isLoggingOut = false;
            state.isAuthenticated = false;
            state.logoutError = false;
            // console.log("logout.fulfilled");
        });
        builder.addCase(logout.rejected, (state, action) => {
            state.isLoggingOut = false;
            state.logoutError = true;
            // console.log("logout.rejected");
        });
    }
});

export default authSlice.reducer;

export const selectToken = (state: RootState) => state.auth.token;
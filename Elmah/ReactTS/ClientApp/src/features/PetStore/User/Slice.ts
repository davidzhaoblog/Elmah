import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import { closeSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';

import { userApi } from 'src/apis/PetStore/UserApi';



import { LoginUserCriteria, defaultLoginUserCriteria, GetUserByNameCriteria, defaultGetUserByNameCriteria } from 'src/apis/PetStore/UserCriteria';


import { orderBys, User } from '../User';

// 1. createEntityAdapter
const entityAdapter = createEntityAdapter<User>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (item: User) => item.id,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
  })

// 2. actions can dispatch


// 2.Get.1. LoginUser - /user/login
export const loginUser = createAsyncThunk(
    'User.loginUser',
    async (criteria: LoginUserCriteria, {dispatch}) => {
        const response = await userApi.LoginUser(criteria);
        dispatch(closeSpinner());
        return response;
    }
)


// 2.Get.2. LogoutUser - /user/logout
export const logoutUser = createAsyncThunk(
    'User.logoutUser',
    async (criteria: LogoutUserCriteria, {dispatch}) => {
        const response = await userApi.LogoutUser(criteria);
        dispatch(closeSpinner());
        return response;
    }
)


// 2.Get.3. GetUserByName - /user/{username}
export const getUserByName = createAsyncThunk(
    'User.getUserByName',
    async (criteria: GetUserByNameCriteria, {dispatch}) => {
        const response = await userApi.GetUserByName(criteria);
        dispatch(closeSpinner());
        return response;
    }
)



// 3. slice
const userSlice = createSlice({
    name: 'users',
    initialState: entityAdapter.getInitialState({
        orderBy: orderBys.find(x=>x.expression),

        loginUserCriteria: defaultLoginUserCriteria(),
        getUserByNameCriteria: defaultGetUserByNameCriteria()


    }), // createEntityAdapter Usage #1
    reducers: {
    },
    // 3.2. extraReducers
    extraReducers: builder => {

		// 3.2.Get.1. LoginUser - /user/login
        builder.addCase(loginUser.pending, (state) => {
            // console.log("loginUser.pending");
        });
        builder.addCase(loginUser.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("loginUser.fulfilled");
        });
        builder.addCase(loginUser.rejected, (state, action) => {
            // console.log("loginUser.rejected");
        });


		// 3.2.Get.1. LogoutUser - /user/logout
        builder.addCase(logoutUser.pending, (state) => {
            // console.log("logoutUser.pending");
        });
        builder.addCase(logoutUser.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("logoutUser.fulfilled");
        });
        builder.addCase(logoutUser.rejected, (state, action) => {
            // console.log("logoutUser.rejected");
        });


		// 3.2.Get.1. GetUserByName - /user/{username}
        builder.addCase(getUserByName.pending, (state) => {
            // console.log("getUserByName.pending");
        });
        builder.addCase(getUserByName.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("getUserByName.fulfilled");
        });
        builder.addCase(getUserByName.rejected, (state, action) => {
            // console.log("getUserByName.rejected");
        });



    }
});

 // createEntityAdapter Usage #4, used in ToDoList.tsx
export const userSelectors = entityAdapter.getSelectors<RootState>(
    state => state.user
  )
export default userSlice.reducer;


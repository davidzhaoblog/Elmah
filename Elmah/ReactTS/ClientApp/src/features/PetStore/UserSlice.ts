import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import { closeSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';

import { createQueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';


import { userApi } from 'src/apis/PetStore/UserApi';



import { 
	GetUserByNameParameters, defaultGetUserByNameParameters,
	LoginUserParameters, defaultLoginUserParameters,
	UpdateUserParameters, defaultUpdateUserParameters,
	DeleteUserParameters, defaultDeleteUserParameters 
} from 'src/apis/PetStore/UserParameters';


import { orderBys, User } from './User';

// 1. createEntityAdapter
const entityAdapter = createEntityAdapter<User>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (item: User) => item.id,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
  })

// 2. Async Thunks

// 2.Get.1. GetUserByName - /user/{username}
export const getUserByName = createAsyncThunk(
    'User.GetUserByName',
    async (criteria: GetUserByNameParameters, {dispatch}) => {
        const response = await userApi.GetUserByName(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Get.2. LoginUser - /user/login
export const loginUser = createAsyncThunk(
    'User.LoginUser',
    async (criteria: LoginUserParameters, {dispatch}) => {
        const response = await userApi.LoginUser(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Get.3. LogoutUser - /user/logout
export const logoutUser = createAsyncThunk(
    'User.LogoutUser',
    async (, {dispatch}) => {
        const response = await userApi.LogoutUser(null).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Post.1. CreateUser - /user
export const createUser = createAsyncThunk(
    'User.CreateUser',
    async (requestBody: User, {dispatch}) => {
        const response = await userApi.CreateUser(null).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Post.2. CreateUsersWithListInput - /user/createWithList
export const createUsersWithListInput = createAsyncThunk(
    'User.CreateUsersWithListInput',
    async (requestBody: User[], {dispatch}) => {
        const response = await userApi.CreateUsersWithListInput(null).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Put.1. UpdateUser - /user/{username}
export const updateUser = createAsyncThunk(
    'User.UpdateUser',
    async (criteria: UpdateUserParameters, requestBody: User, {dispatch}) => {
        const response = await userApi.UpdateUser(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Delete.1. DeleteUser - /user/{username}
export const deleteUser = createAsyncThunk(
    'User.DeleteUser',
    async (criteria: DeleteUserParameters, {dispatch}) => {
        const response = await userApi.DeleteUser(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)



// 3. slice
const userSlice = createSlice({
    name: 'users',
    initialState: entityAdapter.getInitialState({
        orderBy: orderBys.find(x=>x.expression),
		queryPagingSetting: createQueryPagingSetting(10, 1),

        getUserByNameParameters: defaultGetUserByNameParameters(),
        loginUserParameters: defaultLoginUserParameters(),
        updateUserParameters: defaultUpdateUserParameters(),
        deleteUserParameters: defaultDeleteUserParameters()


    }), // createEntityAdapter Usage #1
    reducers: {
    },
    // 3.2. extraReducers
    extraReducers: builder => {

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


		// 3.2.Get.2. LoginUser - /user/login
        builder.addCase(loginUser.pending, (state) => {
            // console.log("loginUser.pending");
        });
        builder.addCase(loginUser.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			// entityAdapter.upsertOne(state, payload);
            // console.log("loginUser.fulfilled");
        });
        builder.addCase(loginUser.rejected, (state, action) => {
            // console.log("loginUser.rejected");
        });


		// 3.2.Get.3. LogoutUser - /user/logout
        builder.addCase(logoutUser.pending, (state) => {
            // console.log("logoutUser.pending");
        });
        builder.addCase(logoutUser.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			// TODO: please write your logic to delete one or many entries in entityAdapter
			// entityAdapter.upsertOne(state, payload);
            // console.log("logoutUser.fulfilled");
        });
        builder.addCase(logoutUser.rejected, (state, action) => {
            // console.log("logoutUser.rejected");
        });


		// 3.2.Post.1. CreateUser - /user
        builder.addCase(createUser.pending, (state) => {
            // console.log("createUser.pending");
        });
        builder.addCase(createUser.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("createUser.fulfilled");
        });
        builder.addCase(createUser.rejected, (state, action) => {
            // console.log("createUser.rejected");
        });


		// 3.2.Post.2. CreateUsersWithListInput - /user/createWithList
        builder.addCase(createUsersWithListInput.pending, (state) => {
            // console.log("createUsersWithListInput.pending");
        });
        builder.addCase(createUsersWithListInput.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("createUsersWithListInput.fulfilled");
        });
        builder.addCase(createUsersWithListInput.rejected, (state, action) => {
            // console.log("createUsersWithListInput.rejected");
        });


		// 3.2.Put.1. UpdateUser - /user/{username}
        builder.addCase(updateUser.pending, (state) => {
            // console.log("updateUser.pending");
        });
        builder.addCase(updateUser.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			// TODO: please write your logic to delete one or many entries in entityAdapter
			// entityAdapter.upsertOne(state, payload);
            // console.log("updateUser.fulfilled");
        });
        builder.addCase(updateUser.rejected, (state, action) => {
            // console.log("updateUser.rejected");
        });




		// 3.2.Delete.1. DeleteUser - /user/{username}
        builder.addCase(deleteUser.pending, (state) => {
            // console.log("deleteUser.pending");
        });
        builder.addCase(deleteUser.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			// TODO: please write your logic to delete one or many entries in entityAdapter
			// entityAdapter.removeOne(state, payload);
            // entityAdapter.removeMany(state, payload);
            // console.log("deleteUser.fulfilled");
        });
        builder.addCase(deleteUser.rejected, (state, action) => {
            // console.log("deleteUser.rejected");
        });



    }
});

 // createEntityAdapter Usage #4, used in ToDoList.tsx
export const userSelectors = entityAdapter.getSelectors<RootState>(
    state => state.user
  )
export default userSlice.reducer;


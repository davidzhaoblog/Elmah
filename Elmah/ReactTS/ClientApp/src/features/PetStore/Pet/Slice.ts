import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import { closeSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';

import { createQueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';

import { petApi } from 'src/apis/PetStore/PetApi';



import { FindPetsByStatusCriteria, defaultFindPetsByStatusCriteria, FindPetsByTagsCriteria, defaultFindPetsByTagsCriteria, GetPetByIdCriteria, defaultGetPetByIdCriteria } from 'src/apis/PetStore/PetCriteria';


import { orderBys, Pet } from '../Pet';


// 1. createEntityAdapter
const entityAdapter = createEntityAdapter<Pet>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (item: Pet) => item.id,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
  })

// 2. actions can dispatch


// 2.Get.1. FindPetsByStatus - /pet/findByStatus
export const findPetsByStatus = createAsyncThunk(
    'findPetsByStatus',
    async (criteria: FindPetsByStatusCriteria, {dispatch}) => {
        const response = await petApi.FindPetsByStatus(criteria);
        dispatch(closeSpinner());
        return response;
    }
)


// 2.Get.2. FindPetsByTags - /pet/findByTags
export const findPetsByTags = createAsyncThunk(
    'findPetsByTags',
    async (criteria: FindPetsByTagsCriteria, {dispatch}) => {
        try
        {
        const response = await petApi.FindPetsByTags(criteria);
        dispatch(closeSpinner());
        return response;
        }
        catch
        {
            dispatch(closeSpinner());
            return null;
            }
    }
)


// 2.Get.3. GetPetById - /pet/{petId}
export const getPetById = createAsyncThunk(
    'Pet.getPetById',
    async (criteria: GetPetByIdCriteria, {dispatch}) => {
        const response = await petApi.GetPetById(criteria);
        dispatch(closeSpinner());
        return response;
    }
)



// 3. slice
const petSlice = createSlice({
    name: 'pets',
    initialState: entityAdapter.getInitialState({
        orderBy: orderBys.find(x=>x.expression),

        findPetsByStatusCriteria: defaultFindPetsByStatusCriteria(),
        findPetsByTagsCriteria: defaultFindPetsByTagsCriteria(),
        getPetByIdCriteria: defaultGetPetByIdCriteria(),

        queryPagingSetting: createQueryPagingSetting(10, 1)
    }), // createEntityAdapter Usage #1
    reducers: {
    },
    // 3.2. extraReducers
    extraReducers: builder => {

		// 3.2.Get.1. FindPetsByStatus - /pet/findByStatus
        builder.addCase(findPetsByStatus.pending, (state) => {
            // console.log("findPetsByStatus.pending");
        });
        builder.addCase(findPetsByStatus.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertMany(state, payload);
            // console.log("findPetsByStatus.fulfilled");
        });
        builder.addCase(findPetsByStatus.rejected, (state, action) => {
            // console.log("findPetsByStatus.rejected");
        });


		// 3.2.Get.1. FindPetsByTags - /pet/findByTags
        builder.addCase(findPetsByTags.pending, (state) => {
            // console.log("findPetsByTags.pending");
        });
        builder.addCase(findPetsByTags.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertMany(state, payload);
            // console.log("findPetsByTags.fulfilled");
        });
        builder.addCase(findPetsByTags.rejected, (state, action) => {
            // console.log("findPetsByTags.rejected");
        });


		// 3.2.Get.1. GetPetById - /pet/{petId}
        builder.addCase(getPetById.pending, (state) => {
            // console.log("getPetById.pending");
        });
        builder.addCase(getPetById.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("getPetById.fulfilled");
        });
        builder.addCase(getPetById.rejected, (state, action) => {
            // console.log("getPetById.rejected");
        });



    }
});

 // createEntityAdapter Usage #4, used in ToDoList.tsx
export const petSelectors = entityAdapter.getSelectors<RootState>(
    state => state.pet
  )
export default petSlice.reducer;


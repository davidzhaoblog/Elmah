import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import { closeSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';

import { createQueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';


import { petApi } from 'src/apis/PetStore/PetApi';



import { 
	FindPetsByStatusParameters, defaultFindPetsByStatusParameters,
	FindPetsByTagsParameters, defaultFindPetsByTagsParameters,
	GetPetByIdParameters, defaultGetPetByIdParameters,
	UpdatePetWithFormParameters, defaultUpdatePetWithFormParameters,
	UploadFileParameters, defaultUploadFileParameters,
	DeletePetParameters, defaultDeletePetParameters 
} from 'src/apis/PetStore/PetParameters';


import { orderBys, Pet } from './Pet';

// 1. createEntityAdapter
const entityAdapter = createEntityAdapter<Pet>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (item: Pet) => item.id,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
  })

// 2. Async Thunks

// 2.Get.1. FindPetsByStatus - /pet/findByStatus
export const findPetsByStatus = createAsyncThunk(
    'Pet.FindPetsByStatus',
    async (criteria: FindPetsByStatusParameters, {dispatch}) => {
        const response = await petApi.FindPetsByStatus(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Get.2. FindPetsByTags - /pet/findByTags
export const findPetsByTags = createAsyncThunk(
    'Pet.FindPetsByTags',
    async (criteria: FindPetsByTagsParameters, {dispatch}) => {
        const response = await petApi.FindPetsByTags(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Get.3. GetPetById - /pet/{petId}
export const getPetById = createAsyncThunk(
    'Pet.GetPetById',
    async (criteria: GetPetByIdParameters, {dispatch}) => {
        const response = await petApi.GetPetById(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Post.1. AddPet - /pet
export const addPet = createAsyncThunk(
    'Pet.AddPet',
    async (requestBody: Pet, {dispatch}) => {
        const response = await petApi.AddPet(null).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Post.2. UpdatePetWithForm - /pet/{petId}
export const updatePetWithForm = createAsyncThunk(
    'Pet.UpdatePetWithForm',
    async (criteria: UpdatePetWithFormParameters, {dispatch}) => {
        const response = await petApi.UpdatePetWithForm(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Post.3. UploadFile - /pet/{petId}/uploadImage
export const uploadFile = createAsyncThunk(
    'Pet.UploadFile',
    async (criteria: UploadFileParameters, {dispatch}) => {
        const response = await petApi.UploadFile(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Put.1. UpdatePet - /pet
export const updatePet = createAsyncThunk(
    'Pet.UpdatePet',
    async (requestBody: Pet, {dispatch}) => {
        const response = await petApi.UpdatePet(null).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)


// 2.Delete.1. DeletePet - /pet/{petId}
export const deletePet = createAsyncThunk(
    'Pet.DeletePet',
    async (criteria: DeletePetParameters, {dispatch}) => {
        const response = await petApi.DeletePet(criteria).catch(ex => {alert(ex);}).finally(()=>{dispatch(closeSpinner());});
        return response;
    }
)



// 3. slice
const petSlice = createSlice({
    name: 'pets',
    initialState: entityAdapter.getInitialState({
        orderBy: orderBys.find(x=>x.expression),
		queryPagingSetting: createQueryPagingSetting(10, 1),

        findPetsByStatusParameters: defaultFindPetsByStatusParameters(),
        findPetsByTagsParameters: defaultFindPetsByTagsParameters(),
        getPetByIdParameters: defaultGetPetByIdParameters(),
        updatePetWithFormParameters: defaultUpdatePetWithFormParameters(),
        uploadFileParameters: defaultUploadFileParameters(),
        deletePetParameters: defaultDeletePetParameters()


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


		// 3.2.Get.2. FindPetsByTags - /pet/findByTags
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


		// 3.2.Get.3. GetPetById - /pet/{petId}
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


		// 3.2.Post.1. AddPet - /pet
        builder.addCase(addPet.pending, (state) => {
            // console.log("addPet.pending");
        });
        builder.addCase(addPet.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("addPet.fulfilled");
        });
        builder.addCase(addPet.rejected, (state, action) => {
            // console.log("addPet.rejected");
        });


		// 3.2.Post.2. UpdatePetWithForm - /pet/{petId}
        builder.addCase(updatePetWithForm.pending, (state) => {
            // console.log("updatePetWithForm.pending");
        });
        builder.addCase(updatePetWithForm.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			// TODO: please write your logic to delete one or many entries in entityAdapter
			// entityAdapter.upsertOne(state, payload);
            // console.log("updatePetWithForm.fulfilled");
        });
        builder.addCase(updatePetWithForm.rejected, (state, action) => {
            // console.log("updatePetWithForm.rejected");
        });


		// 3.2.Post.3. UploadFile - /pet/{petId}/uploadImage
        builder.addCase(uploadFile.pending, (state) => {
            // console.log("uploadFile.pending");
        });
        builder.addCase(uploadFile.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("uploadFile.fulfilled");
        });
        builder.addCase(uploadFile.rejected, (state, action) => {
            // console.log("uploadFile.rejected");
        });


		// 3.2.Put.1. UpdatePet - /pet
        builder.addCase(updatePet.pending, (state) => {
            // console.log("updatePet.pending");
        });
        builder.addCase(updatePet.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			entityAdapter.upsertOne(state, payload);
            // console.log("updatePet.fulfilled");
        });
        builder.addCase(updatePet.rejected, (state, action) => {
            // console.log("updatePet.rejected");
        });




		// 3.2.Delete.1. DeletePet - /pet/{petId}
        builder.addCase(deletePet.pending, (state) => {
            // console.log("deletePet.pending");
        });
        builder.addCase(deletePet.fulfilled, (state, { payload }) => {
            if(!payload)
                return;
			// TODO: please write your logic to delete one or many entries in entityAdapter
			// entityAdapter.removeOne(state, payload);
            // entityAdapter.removeMany(state, payload);
            // console.log("deletePet.fulfilled");
        });
        builder.addCase(deletePet.rejected, (state, action) => {
            // console.log("deletePet.rejected");
        });



    }
});

 // createEntityAdapter Usage #4, used in ToDoList.tsx
export const petSelectors = entityAdapter.getSelectors<RootState>(
    state => state.pet
  )
export default petSlice.reducer;


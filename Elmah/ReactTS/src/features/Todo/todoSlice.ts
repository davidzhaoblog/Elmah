import { createAsyncThunk, createEntityAdapter, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { todoApi } from 'src/apis/TodoApi';
import { IListRequest } from 'src/framework/IndexComponentBase';
import { createQueryPagingSetting } from 'src/framework/Queries/QueryPagingSetting';
import { closeSpinner } from 'src/layout/appSlice';
import { RootState } from 'src/store/CombinedReducers';
import { AppDispatch, AppThunk } from 'src/store/Store';
import { orderBys, Todo } from './types';

// 1. createEntityAdapter
const entityAdapter = createEntityAdapter<Todo>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (todo) => todo.id,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
  })
  

// 1. initialState 
const initialState = entityAdapter.getInitialState({
    criteria: null,
    orderBy: orderBys.find(x=>x.displayName),
    queryPagingSetting: createQueryPagingSetting(10, 1)
});

// 2. actions can dispatch
// 2.upsert upsert action can dispatch
export const upsert = createAsyncThunk(
    'upsert',
    async (payload: Todo) => {
        // const response = await entityStatusCodeApiClient.Upsert();
        // return response;

        return payload;
    }
)
// 2.delete delete action can dispatch
export const del = createAsyncThunk(
    'del',
    async (payload: Todo) => {
        // const response = await entityStatusCodeApiClient.Delete();
        // return response;
    }
)
// 2.getByIdentifier getByIdentifier action can dispatch
export const getByIdentifier = createAsyncThunk(
    'getByIdentifier',
    async (payload: Todo) => {
        // const response = await entityStatusCodeApiClient.GetByIdentifier();
        // return response;
    }
)
// 2.getIndexVM getIndexVM action can dispatch
export const getIndexVM = createAsyncThunk(
    'getIndexVM',
    async (payload: IListRequest<Todo>, {dispatch}) => {
        const response = await todoApi.GetIndexVM(payload);
        dispatch(closeSpinner());
        return response;
    }
)

// 3. slice
const todoSlice = createSlice({
    name: 'todos',
    initialState, // createEntityAdapter Usage #1
    reducers: {
        addTodo: entityAdapter.addOne,  // Usage createEntityAdapter #2
        toggleTodo(state, action: PayloadAction<Todo>) {
            const todo = entityAdapter.getSelectors().selectById(state, action.payload.id);  // Usage createEntityAdapter #3

            if (todo) {
                todo.completed = !todo.completed;
            }
        },
    },
    // 3.2. extraReducers
    extraReducers: builder => {
        // 3.2.upsert. upsert
        builder.addCase(upsert.pending, (state) => {
            // console.log("upsert.pending");
        });
        builder.addCase(upsert.fulfilled, (state, { payload }) => {
            entityAdapter.upsertOne(state, payload);
            // console.log("upsert.fulfilled");
        });
        builder.addCase(upsert.rejected, (state, action) => {
            // console.log("upsert.rejected");
        });
        // 3.2.delete. delete
        builder.addCase(del.pending, (state) => {
            // console.log("delete.pending");
        });
        builder.addCase(del.fulfilled, (state, { payload }) => {
            // console.log("delete.fulfilled");
        });
        builder.addCase(del.rejected, (state, action) => {
            // console.log("delete.rejected");
        });
        // 3.2.getByIdentifier. getByIdentifier
        builder.addCase(getByIdentifier.pending, (state) => {
            // console.log("getByIdentifier.pending");
        });
        builder.addCase(getByIdentifier.fulfilled, (state, { payload }) => {
            // console.log("getByIdentifier.fulfilled");
        });
        builder.addCase(getByIdentifier.rejected, (state, action) => {
            // console.log("getByIdentifier.rejected");
        });
        // 3.2.getIndexVM. getIndexVM
        builder.addCase(getIndexVM.pending, (state) => {
            // console.log("getIndexVM.pending");
        });
        builder.addCase(getIndexVM.fulfilled, (state, { payload }) => {
            entityAdapter.removeAll(state);
            entityAdapter.upsertMany(state, payload.result);
            state.queryPagingSetting = payload.queryPagingSetting;
            state.orderBy = payload.orderBy;
            // console.log("getIndexVM.fulfilled");
        });
        builder.addCase(getIndexVM.rejected, (state, action) => {
            // console.log("getIndexVM.rejected");
        });
    }
});

export const { toggleTodo } = todoSlice.actions;

export const addTodo = (
    text: string
): AppThunk => async (dispatch: AppDispatch) => {
    const newTodo : Todo = {
        id: Math.random().toString(36).substr(2, 9), // https://gist.github.com/gordonbrander/2230317,
        completed: false,
        text: text,
    }

    dispatch(todoSlice.actions.addTodo(newTodo))
}

 // createEntityAdapter Usage #4, used in ToDoList.tsx
export const todosSelectors = entityAdapter.getSelectors<RootState>(
    state => state.todos
  )
export default todoSlice.reducer;


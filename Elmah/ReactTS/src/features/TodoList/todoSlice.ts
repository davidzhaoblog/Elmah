import { createEntityAdapter, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from 'src/store/CombinedReducers';
import { AppDispatch, AppThunk } from 'src/store/Store';
import { Todo } from './types';

// const initialState : Todo[] = [];

// 1. createEntityAdapter
const todosAdapter = createEntityAdapter<Todo>({
    // Assume IDs are stored in a field other than `book.id`
    selectId: (todo) => todo.id,
    // Keep the "all IDs" array sorted based on book titles
    sortComparer: (a, b) => a.text.localeCompare(b.text),
  })
  

const todoSlice = createSlice({
    name: 'todos',
    initialState: todosAdapter.getInitialState(), // createEntityAdapter Usage #1
    reducers: {
        addTodo: todosAdapter.addOne,  // Usage createEntityAdapter #2
        toggleTodo(state, action: PayloadAction<Todo>) {
            const todo = todosAdapter.getSelectors().selectById(state, action.payload.id);  // Usage createEntityAdapter #3

            if (todo) {
                todo.completed = !todo.completed;
            }
        },
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
export const todosSelectors = todosAdapter.getSelectors<RootState>(
    state => state.todos
  )

export default todoSlice.reducer;


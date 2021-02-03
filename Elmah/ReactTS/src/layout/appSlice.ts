import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAlert, IAppState } from './types';

const initialState : IAppState = {
    drawerOpen: true,
    loading: false,
    alert: null,
}

const appSlice = createSlice({
    name: 'app',
    initialState,
    reducers: {
        toggleAppDrawer(state) {
            state.drawerOpen = ! state.drawerOpen;
        },
        showSpinner(state) {
            state.loading = true;
        },
        closeSpinner(state) {
            state.loading = false;
        },
        showAlert(state, action: PayloadAction<IAlert>) {
            state.alert = action.payload;
        },
        closeAlert(state) {
            state.alert = null;
        },
    }
});

export const { toggleAppDrawer, showSpinner, closeSpinner, showAlert, closeAlert } = appSlice.actions;
export default appSlice.reducer;

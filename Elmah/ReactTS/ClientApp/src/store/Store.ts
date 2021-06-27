import { Action, configureStore, getDefaultMiddleware } from '@reduxjs/toolkit';
import { ThunkAction } from 'redux-thunk';

import { reducers, RootState } from './CombinedReducers';

const store = configureStore({
    reducer: reducers
    // TODO: the following commented out code can suppress: "A non-serializable value was detected in the state, in the path:"
    ,
    middleware: getDefaultMiddleware({
        serializableCheck: {
            // Ignore these action types, Alert and whenever showAlert is called.
            ignoredActions: ['app/showAlert', 'logout/pending', 'persist/PERSIST'],
            // // Ignore these field paths in all actions
            // ignoredActionPaths: ['app.alert.buttons[0].handler'],
            //   // Ignore these paths in the state
            //   ignoredPaths: ['items.dates']
            // persist/PERSIST is from 'redux-persist/integration/react'
        }
    })
})

if (process.env.NODE_ENV === 'development' && module.hot) {
    module.hot.accept('./CombinedReducers', () => {
        const newRootReducer = require('./CombinedReducers').default
        store.replaceReducer(newRootReducer)
    })
}

export type AppDispatch = typeof store.dispatch
export type AppThunk = ThunkAction<void, RootState, null, Action<string>>

export default store;

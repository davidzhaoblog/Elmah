import { Action, configureStore, getDefaultMiddleware } from '@reduxjs/toolkit';
import { ThunkAction } from 'redux-thunk';

import { persistReducer } from 'redux-persist' // imports from redux-persist
import storage from 'redux-persist/lib/storage' // defaults to localStorage for web
import { reducers, RootState } from './CombinedReducers';

const persistConfig = { // configuration object for redux-persist
    key: 'root',
    storage, // define which storage to use
    // blacklist: ['navigation'], // navigation will not be persisted
    // whitelist: ['navigation'] // only navigation will be persisted
}
const persistedReducer = persistReducer(persistConfig, reducers) // create a persisted reducer

const store = configureStore({
    reducer: persistedReducer
    // TODO: the following commented out code can suppress: "A non-serializable value was detected in the state, in the path:"
    ,
    middleware: getDefaultMiddleware({
        serializableCheck: {
            // Ignore these action types, Alert and whenever showAlert is called.
            ignoredActions: ['app/showAlert', 'logout/pending'],
            // // Ignore these field paths in all actions
            // ignoredActionPaths: ['app.alert.buttons[0].handler'],
            //   // Ignore these paths in the state
            //   ignoredPaths: ['items.dates']
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
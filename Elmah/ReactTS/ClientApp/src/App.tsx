import * as React from 'react';
import './App.css';
import { BrowserRouter as Router } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './store/Store';

import { persistStore } from 'redux-persist' // imports from redux-persist
import { PersistGate } from 'redux-persist/integration/react'

import blue from '@material-ui/core/colors/blue';
import { createMuiTheme, ThemeProvider } from '@material-ui/core';
import { pink } from '@material-ui/core/colors';
import MasterLayout from './layout/MasterLayout';
import MomentUtils from '@date-io/moment';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import "./i18n"
import './i18n';
import { useTranslation } from 'react-i18next';

const theme = createMuiTheme({
  palette: {
    primary: blue,
    secondary: pink
  }
})

const persistor = persistStore(store); // used to create the persisted store, persistor will be used in the next step

function App() {
  const { t } = useTranslation(["UIStringResourcePerApp"]);
  document.title = t("UIStringResourcePerApp:Application_Title");  
  
  return (
    <MuiPickersUtilsProvider utils={MomentUtils}>
      <Provider store={store}>
        <PersistGate loading={null} persistor={persistor}>
          <Router>
            <ThemeProvider theme={theme}>
              <MasterLayout theme={theme} />
            </ThemeProvider>
          </Router>
        </PersistGate>
      </Provider>
    </MuiPickersUtilsProvider>
  );
}

export default App;

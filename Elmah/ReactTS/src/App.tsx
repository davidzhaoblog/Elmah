import * as React from 'react';
import './App.css';
import { BrowserRouter as Router } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './store/Store';
import blue from '@material-ui/core/colors/blue';
import { createMuiTheme, ThemeProvider } from '@material-ui/core';
import { pink } from '@material-ui/core/colors';
import MasterLayout from './layout/MasterLayout';

const theme = createMuiTheme({
  palette: {
    primary: blue,
    secondary: pink
  }
})

class App extends React.Component {
  public render() {
    return (
      <Provider store={store}>
        <Router>
          <ThemeProvider theme={theme}>
            <MasterLayout theme={theme} />
          </ThemeProvider>
        </Router>
      </Provider>
    );
  }
}

export default App;

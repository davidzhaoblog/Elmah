import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import Link from '@material-ui/core/Link';
import ProTip from './ProTip';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';

import { useTranslation } from 'react-i18next';
import "./i18n"
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from './store/CombinedReducers';
import { increment } from './layout/appSlice';
import { NavLink, Route, Switch } from 'react-router-dom';
import ListItem from '@material-ui/core/ListItem';
import { ListItemIcon } from '@material-ui/core';
import { ListItemText } from '@material-ui/core';
import { Computer } from '@material-ui/icons';
import TodoList from './features/Todo/ListPage';
import { useCookies } from 'react-cookie';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
}));

function Copyright() {
  return (
    <Typography variant="body2" color="textSecondary" align="center">
      {'Copyright © '}
      <Link color="inherit" href="https://material-ui.com/">
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

export default function App() {
  const count = useSelector((state: RootState) => state.app.value)
  const dispatch = useDispatch()
  const [cookies, setCookie] = useCookies(['cookieTest']);

  const classes = useStyles();
  const { t } = useTranslation(["UIStringResourcePerApp"]);
  document.title = t("UIStringResourcePerApp:Application_Title");

  useEffect(() => {
    setCookie('cookieTest', 'cookie Test', {path: '/'})
  })

  return (
    // For full screen: use following
    // <div className={classes.root}></div>
    // For size lg/md/sm/sx: use following
    // <Container maxWidth="lg"></Container>
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <IconButton edge="start" className={classes.menuButton} color="inherit" aria-label="menu">
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" className={classes.title}>
            News
          </Typography>
          <Typography variant="h6" className={classes.title}>
            {count}
          </Typography>
          <Button color="inherit">Login</Button>
        </Toolbar>
      </AppBar>
      <Box my={4}>
        <Typography variant="h4" component="h1" gutterBottom>
          React 17.0.2
        </Typography>
        <Typography variant="h4" component="h1" gutterBottom>
          Material-UI ^4.11.4
        </Typography>
        <Typography variant="h6" className={classes.title}>
          React-Redux test: {count}
        </Typography>
        <Button color="inherit" aria-label="Increment value"
          onClick={() => dispatch(increment())}>Increment</Button>

        <Typography variant="h6" className={classes.title}>
          React-Router-Dom test:
        </Typography>
        <Switch>
          <Route path="/todo" component={TodoList} exact />
        </Switch>

        <NavLink exact={true} to={'/todo'} >
          <ListItem button={true}>
            <ListItemIcon>
              <Computer />
            </ListItemIcon>
            <ListItemText primary={'todo'} />
          </ListItem>
        </NavLink>

        <Typography variant="h6" className={classes.title}>
          React-Cookie test: {cookies['cookieTest']}
        </Typography>

        <ProTip />

        <Copyright />
      </Box>
    </div>
  );
}
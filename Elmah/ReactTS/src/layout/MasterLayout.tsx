import React, { useEffect, useState } from "react";
import { Route, useHistory } from 'react-router-dom'
import { useDispatch, useSelector } from "react-redux";
import { AppBar, Backdrop, Badge, CircularProgress, Hidden, IconButton, Menu, MenuItem, Theme, Toolbar, Typography } from "@material-ui/core";
import AccountCircle from "@material-ui/icons/AccountCircle";
import MenuIcon from '@material-ui/icons/Menu';
import NotificationIcon from '@material-ui/icons/Notifications';

const classNames = require('classnames');

import i18next from 'i18next';
import { useTranslation } from 'react-i18next';
// import {
//     Icon_Flag_EN,
//     Icon_Flag_ES
//   } from 'material-ui-country-flags';

import { PrivateRoute } from "src/features/Authentication/PrivateRoute";

import { showAlert, closeAlert, toggleAppDrawer } from "./appSlice";
import AppDrawer from "./AppDrawer";
import AlertDialog from "./AlertDialog";
import { useStyles } from './styles';

import { RootState } from "src/store/CombinedReducers";
import { logout } from "src/features/Authentication/authenticationSlice";
import Account from "src/features/Authentication/Account";
import DashboardPage from "src/features/DashboardPage";
import { todosSelectors } from "src/features/Todo/todoSlice";
import TodoList from "src/features/Todo/ListPage";
import { createLogoutAlertButtonsOptions } from "src/framework/ViewModels/IButtonOptions";

interface IMasterLayoutProps {
    theme: Theme;
}

export default function MasterLayout(props: IMasterLayoutProps): JSX.Element {
    // 1. states
    const classes = useStyles();
    const dispatch = useDispatch();
    const history = useHistory();

    const app = useSelector((state: RootState) => state.app);
    const auth = useSelector((state: RootState) => state.auth);

    // TODO: use todo list as notification for now.
    const unreadMessages = useSelector(
        (state: RootState) => todosSelectors.selectAll(state)
    );

    const [anchorElLanguage, setAnchorLanguage] = useState()
    const [anchorElMenu, setAnchorElMenu] = useState()
    const [notificationEl, setNotificationEl] = useState()

    const { t, i18n } = useTranslation(["UIStringResourcePerApp"]);
    const [language, setLanguage] = useState('')
    const [languages, setLanguages] = useState([])
    const changeLanguage = (language: string) => {
        i18n.changeLanguage(language);
        setLanguage(i18next.language);
        setAnchorLanguage(null);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        setLanguages(i18next.languages);
        i18n.changeLanguage(language);
        setLanguage(i18next.language);
    }, []);

    // 2.1. Drawer
    const handleDrawerOpen = () => {
        dispatch(toggleAppDrawer());
    };

    // 2.2. Menu
    const handleMenu = (event: any) => {
        setAnchorElMenu(event.currentTarget);
    };

    const handleMenuClose = (path?: string) => {
        setAnchorElMenu(null);
        navigate(path);
    };

    // 2.3. Menu
    const handleLanguage = (event: any) => {
        setAnchorLanguage(event.currentTarget);
    };

    const handleLanguageClose = (lang?: string) => {
        setAnchorLanguage(null);
    };

    // 2.3. Notification
    const handleNotificationMenu = (event: any) => {
        setNotificationEl(event.currentTarget);
    };

    const navigate = (path?: string) => {
        if (path) {
            history.push(path);
        }
    }

    // 2.4. Logout
    const confirmLogout = () => {
        dispatch(logout());
        handleMenuClose();
        dispatch(closeAlert());
        history.replace('/account/login');
    }
    const handleAlertClose = () => {
        dispatch(closeAlert());
    }
    const logoutAlertDialog = {
        title: t('UIStringResource:Account_LogInStatus_LogoutText'),
        message: t('UIStringResource:Account_LogInStatus_LogoutText'),
        buttons: createLogoutAlertButtonsOptions(confirmLogout, handleAlertClose)
    };

    const handleLogout = () => {
        dispatch(showAlert(logoutAlertDialog));
    };

    // 3. RenderXYZ()
    // 3.1. renderAppBar
    const renderAppBar = () => {
        if (auth && auth.isAuthenticated) {
            const openMenu = Boolean(anchorElMenu);
            const openLanguage = Boolean(anchorElLanguage);
            const notificationsOpen = Boolean(notificationEl);

            return (
                <AppBar
                    position="fixed"
                    className={classNames(classes.appBar, app.drawerOpen && classes.appBarShift)}
                >
                    <Toolbar disableGutters={!app.drawerOpen}>
                        <IconButton
                            color="inherit"
                            aria-label="open drawer"
                            onClick={(e) => handleDrawerOpen()}
                            className={classNames(classes.menuButton, app.drawerOpen && classes.hide)}
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography className={classes.fillSpace} color="inherit" noWrap={true}>
                            {t('UIStringResourcePerApp:Application_Title')}
                        </Typography>
                        <div>
                            <IconButton
                                aria-owns={notificationsOpen ? 'notifications' : null}
                                aria-haspopup="true"
                                color="inherit"
                                onClick={handleNotificationMenu}
                            >
                                <Badge badgeContent={unreadMessages.length} color="secondary">
                                    <NotificationIcon />
                                </Badge>
                            </IconButton>
                            <IconButton
                                aria-owns={openLanguage ? 'menu-appbar' : null}
                                aria-haspopup="true"
                                onClick={handleLanguage}
                                color="inherit"
                            >
                                {language}
                            </IconButton>
                            <Menu
                                id="language-appbar"
                                anchorEl={anchorElLanguage}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={openLanguage}
                                onClose={(e) => handleLanguageClose(null)}
                            >
                                {languages.map((lang: string) => {
                                    return (
                                        <MenuItem key={lang} onClick={(e) => changeLanguage(lang)}>{lang}</MenuItem>
                                    );
                                })}
                            </Menu>
                            <IconButton
                                aria-owns={openMenu ? 'menu-appbar' : null}
                                aria-haspopup="true"
                                onClick={handleMenu}
                                color="inherit"
                            >
                                <AccountCircle />
                            </IconButton>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorElMenu}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={openMenu}
                                onClose={(e) => handleMenuClose(null)}
                            >
                                {/* <MenuItem onClick={this.handleMenuClose.bind(this, '/account')}>{this.props.authentication.name}</MenuItem> */}
                                {/* <MenuItem onClick={(e) => handleMenuClose('/account')}>Whatever</MenuItem> */}
                                <MenuItem onClick={(e) => handleLogout()}>{t('UIStringResource:Account_LogInStatus_LogoutText')}</MenuItem>
                            </Menu>
                        </div>
                    </Toolbar>
                </AppBar>
            );
        }

        return null;
    }

    // 3.2. renderDrawer
    const renderDrawer = () => {
        return (
            <Hidden mdDown={!app.drawerOpen && true}>
                <AppDrawer
                    theme={props.theme}
                />
            </Hidden>
        );
    }

    // 3.3. renderAlert
    const renderAlert = () => {
        if (app.alert) {
            return (
                <AlertDialog title={app.alert.title} message={app.alert.message} buttons={app.alert.buttons} />
            );
        }

        return null
    }


    const renderAccount = () => {
        return (
            <Account />
        );
    };

    // 4. render
    return (

        <div className={classes.root}>
            {renderAppBar()}
            {renderDrawer()}

            <main className={classes.content}>
                <div className={classes.toolbar} />
                <PrivateRoute path='/' exact={true} component={DashboardPage} />
                <PrivateRoute path='/todolist' component={TodoList} />
                <Route path='/account' component={renderAccount} />
                {renderAlert()}
                <Backdrop className={classes.backdrop} open={app.loading}>
                    <CircularProgress color="inherit" />
                </Backdrop>
                {/* {renderSpinner()} */}
            </main>
        </div>
    );
}
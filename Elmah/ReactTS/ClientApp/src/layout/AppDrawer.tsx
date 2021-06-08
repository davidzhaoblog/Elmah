import React from "react";
import { NavLink } from 'react-router-dom';
import { Drawer, IconButton, Divider, ListItem, ListItemIcon, ListItemText, Theme } from '@material-ui/core';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import AccountCircleIcon from '@material-ui/icons/AccountCircle';
import { useStyles } from './styles';
import { RootState } from "src/store/CombinedReducers";
import { useDispatch, useSelector } from "react-redux";
import { toggleAppDrawer } from "./appSlice";
const classNames = require('classnames');

interface IAppDrawerProps {
    theme: Theme;
}

export default function AppDrawer(props: IAppDrawerProps): JSX.Element {
    const classes = useStyles();
    const dispatch = useDispatch();

    const app = useSelector((state: RootState) => state.app);
    const auth = useSelector((state: RootState) => state.auth);

    const handleAppDrawerClose = (event: any) => {
        dispatch(toggleAppDrawer());
    }

    const routes = [
        { path: '/', title: 'Dashboard', icon: () => <AccountCircleIcon /> },
        { path: '/account', title: 'Profile', icon: () => <AccountCircleIcon /> },
        { path: '/todolist', title: 'ToDo', icon: () => <AccountCircleIcon /> },

        { path: '/ELMAH_Error', title: 'ELMAH_Error', icon: () => <AccountCircleIcon /> },


        { path: '/ElmahApplication', title: 'ElmahApplication', icon: () => <AccountCircleIcon /> },


        { path: '/ElmahHost', title: 'ElmahHost', icon: () => <AccountCircleIcon /> },


        { path: '/ElmahSource', title: 'ElmahSource', icon: () => <AccountCircleIcon /> },


        { path: '/ElmahStatusCode', title: 'ElmahStatusCode', icon: () => <AccountCircleIcon /> },


        { path: '/ElmahType', title: 'ElmahType', icon: () => <AccountCircleIcon /> },


        { path: '/ElmahUser', title: 'ElmahUser', icon: () => <AccountCircleIcon /> },



        { path: '/PetStore/Order', title: 'Order', icon: () => <AccountCircleIcon /> },
        { path: '/PetStore/User', title: 'User', icon: () => <AccountCircleIcon /> },
        { path: '/PetStore/Pet', title: 'Pet', icon: () => <AccountCircleIcon /> },
    ]
    
    return (
        <Drawer
            hidden={!auth.isAuthenticated}
            variant="permanent"
            classes={{
                paper: classNames(classes.drawerPaper, !app.drawerOpen && classes.drawerPaperClose),
            }}
            open={app.drawerOpen}
        >
            <div className={classes.toolbar}>
                <IconButton onClick={handleAppDrawerClose}>
                    {props.theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
                </IconButton>
            </div>
            <Divider />
            {routes.map((route, index) => {
                return (
                    <NavLink key={index} exact={true} activeClassName={classes.current} className={classes.link} to={route.path} >
                        <ListItem button={true}>
                            <ListItemIcon>
                                {route.icon()}
                            </ListItemIcon>
                            <ListItemText primary={route.title} />
                        </ListItem>
                    </NavLink>
                );
            })}
            <Divider />
        </Drawer>
    );
}


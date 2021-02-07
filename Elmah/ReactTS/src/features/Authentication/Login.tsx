import React, { useState } from 'react'
import { useDispatch } from 'react-redux'
import { useHistory, useLocation } from 'react-router-dom';
import { createStyles, makeStyles, Theme, } from '@material-ui/core/styles';
import { Button, FormControl, Icon, Input, InputAdornment, InputLabel, Paper } from '@material-ui/core';
const queryString = require('query-string');

import { login } from './authenticationSlice';
import { showSpinner } from 'src/layout/appSlice';

interface stateType {
    from: { pathname: string }
 }
 
export default function LoginPage(): JSX.Element {
    const dispatch = useDispatch();
    const history = useHistory();
    const classes = useStyles();
    const [email,setEmail] = useState("");
    const [password,setPassword] = useState("");
    const location = useLocation<stateType>();
    const { search } = useLocation();
    const values = queryString.parse(search)
    // console.log(values) // "top"

    const { from } = location.state || { from: { pathname: values.redirect || "/" } };

    const handleEmailAddressChange = (event: any) => {
        setEmail(event.target.value)
    }

    const handlePasswordChange = (event: any) => {
        setPassword(event.target.value)
    }

    const handleSubmit = (e:any) => {
        e.preventDefault();
        dispatch(showSpinner());
        dispatch(login({email, password, from, rememberMe: true}));
        history.replace(from);
    }
    
    return (

        <div className={classes.container}>
        <Paper className={classes.paper}>
            <h2>{'Login'}</h2>
            <FormControl required={true} fullWidth={true} className={classes.field}>
                <InputLabel htmlFor="email">Email Address</InputLabel>
                <Input
                    value={email}
                    onChange={handleEmailAddressChange}
                    id="email"
                    startAdornment={
                        <InputAdornment position="start">
                            <Icon>email</Icon>
                        </InputAdornment>}
                />
            </FormControl>
            <FormControl required={true} fullWidth={true} className={classes.field}>
                <InputLabel htmlFor="password">Password</InputLabel>
                <Input
                    value={password}
                    onChange={handlePasswordChange}
                    type="password"
                    id="password"
                    startAdornment={
                        <InputAdornment position="start">
                            <Icon>lock</Icon>
                        </InputAdornment>}
                />
            </FormControl>
            <div className={classes.actions}>
                <Button className={classes.button}>
                    Cancel
                </Button>
                <Button
                    onClick={handleSubmit}
                    color="primary"
                    className={classes.button}>
                    Submit
                </Button>
            </div>
        </Paper>
    </div>      
    );
}

const useStyles = makeStyles((theme: Theme) => createStyles({
    container: {
        display: 'flex',
        justifyContent: 'center'
    },
    paper: theme.mixins.gutters({
        paddingTop: 16,
        paddingBottom: 16,
        marginTop: theme.spacing.length * 3,
        width: '30%',
        display: 'flex',
        flexDirection: 'column',
        alignContent: 'center',
        [theme.breakpoints.down('md')]: {
            width: '100%',
        },
    }),
    field: {
        marginTop: theme.spacing.length * 3
    },
    actions: theme.mixins.gutters({
        paddingTop: 16,
        paddingBottom: 16,
        marginTop: theme.spacing.length * 3,
        display: 'flex',
        flexDirection: 'row',
        alignContent: 'center'
    }),
    button: {
        marginRight: theme.spacing.length
    },
  }));
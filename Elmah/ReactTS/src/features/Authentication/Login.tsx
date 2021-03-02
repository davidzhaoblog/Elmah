import React from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Redirect, useLocation } from 'react-router-dom';
import { Avatar, Button, Container, CssBaseline, Link, Grid, Paper, Typography, FormControlLabel } from '@material-ui/core';
import { AccountCircle as AccountCircleIcon } from '@material-ui/icons';
import { useForm, Controller } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
const queryString = require('query-string');
import { useTranslation } from 'react-i18next';
import "src/i18n"

import { StyledTextField } from 'src/components/controls/StyledTextField';
import { StyledCheckbox } from 'src/components/controls/StyledCheckbox';

import { login } from './authenticationSlice';
import { showSpinner } from 'src/layout/appSlice';
import { useStyles } from './styles';
import { RootState } from 'src/store/CombinedReducers';

interface stateType {
    from: { pathname: string }
}

export default function LoginPage(): JSX.Element {
    // const { t } = useTranslation();
    const { t  } = useTranslation(["UIStringResource"]);
    const dispatch = useDispatch();
    const classes = useStyles();
    const location = useLocation<stateType>();
    const { search } = useLocation();
    const values = queryString.parse(search)
    const auth = useSelector((state: RootState) => state.auth);
    // console.log(values) // "top"
    
    const { register, setValue, handleSubmit, control, errors, formState } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: {
            email: '',
            password: '',
            remember: true,
        },
    });

    const { from } = location.state || { from: { pathname: values.redirect || "/" } };

    const onSubmit = (data: any) => {
        dispatch(showSpinner());
        dispatch(login(data));
        setValue('password', '');
    }

    return (
        <>
        {(auth && auth.isAuthenticated) &&
            <Redirect to={from} />
        }
        <Container component='main' maxWidth='xs'>
            <DevTool control={control} />
            <CssBaseline />
            <Paper className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <AccountCircleIcon style={{ fontSize: 45 }} />
                </Avatar>
                <Typography component='h1' variant='h4'>
                    {t('UIStringResource:Account_LogIn_TitleText')}
                </Typography>
                <form
                    className={classes.form}
                    noValidate
                    onSubmit={handleSubmit(onSubmit)}
                >
                    <StyledTextField
                        name='email'
                        label={t('UIStringResource:Account_ChangePassword_TitleText')}
                        variant='outlined'
                        margin='normal'
                        inputRef={register({
                            required: 'You must provide the email address!',
                            pattern: {
                                value: /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
                                message: 'You must provide a valid email address!',
                            },
                        })}
                        autoComplete='email'
                        error={!!errors.email}
                        fullWidth
                        autoFocus
                    />
                    {errors.email && (
                        <span className={classes.error}>{errors.email.message}</span>
                    )}
                    <StyledTextField
                        name='password'
                        label='Password'
                        type='password'
                        variant='outlined'
                        margin='normal'
                        inputRef={register({
                            required: 'You must provide a password.',
                            minLength: {
                                value: 6,
                                message: 'Your password must be greater than 6 characters',
                            },
                        })}
                        error={!!errors.password}
                        fullWidth
                        autoComplete='current-password'
                    />
                    {errors.password && (
                        <span className={classes.error}>{errors.password.message}</span>
                    )}

                    <Grid container>
                        <Grid item xs>
                            <Link href='#' variant='body2' className={classes.link}>
                                Forgot password?
                            </Link>
                        </Grid>
                        <Grid item>
                            <FormControlLabel
                                label='Remember me'
                                name='remember'
                                control={
                                    <StyledCheckbox
                                        className={classes.checkBox}
                                        inputRef={register()}
                                    />
                                }
                            />
                        </Grid>
                    </Grid>
                    <Grid container>
                        <FormControlLabel
                            control={
                                <Controller
                                    control={control}
                                    name='checkTest'
                                    defaultValue={true}
                                    render={({ onChange, value }) => (
                                        <StyledCheckbox
                                            className={classes.checkBox}
                                            onChange={e => onChange(e.target.checked)}
                                            checked={value}
                                        />
                                    )}
                                />
                            }
                            label='Remember Me'
                        />
                    </Grid>

                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        disabled={!formState.isValid}
                        className={classes.submit}
                    >
                        Sign In
                    </Button>
                    <Grid container>
                        <Grid item>
                            <Link href='#' variant='body2' className={classes.link}>
                                {'New to this platform? Create an Acount.'}
                            </Link>
                        </Grid>
                    </Grid>
                </form>
            </Paper>
        </Container>
        </>
    );
}

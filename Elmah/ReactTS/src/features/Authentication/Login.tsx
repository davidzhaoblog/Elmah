import React from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Redirect, useLocation } from 'react-router-dom';
import { Avatar, Button, Container, CssBaseline, Link, Grid, Paper, Typography, FormControlLabel } from '@material-ui/core';
import { AccountCircle as AccountCircleIcon } from '@material-ui/icons';
import { useForm } from 'react-hook-form';
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
    const { t } = useTranslation(["UIStringResource"]);
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

    const formValidations = {
        email: {
            required: t('UIStringResource:Common_EmailRequiredErrorMessage'),
            pattern: {
                value: /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
                message: t('UIStringResource:Common_EmailFormatErrorMessage'),
            }
        },
        password: {
            required: t('UIStringResource:Common_PasswordRequiredErrorMessage'),
            pattern: {
                value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
                message: t('UIStringResource:Common_PasswordErrorMessage'),
            }
        }
    };

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
                            label={t('UIStringResource:Common_EmailLabelText')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.email)}
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
                            label={t('UIStringResource:Common_PasswordLabelText')}
                            type='password'
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.password)}
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
                                    {t('UIStringResource:Account_PasswordRecovery_TitleText')}
                                </Link>
                            </Grid>
                        </Grid>
                        <Grid container>
                            <Grid item>
                                <FormControlLabel
                                    label={t('UIStringResource:Account_LogIn_RememberMeText')}
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

                        <Button
                            type='submit'
                            fullWidth
                            variant='contained'
                            disabled={!formState.isValid}
                            className={classes.submit}
                        >
                            {t('UIStringResource:Account_LogIn_LoginButtonText')}
                    </Button>
                        <Grid container>
                            <Grid item>
                                <Link href='#' variant='body2' className={classes.link}>
                                    {t('UIStringResource:Account_LogIn_Registerasanewuser')}
                                </Link>
                            </Grid>
                        </Grid>
                    </form>
                </Paper>
            </Container>
        </>
    );
}

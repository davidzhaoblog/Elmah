import * as React from 'react'
import { FormControl, FormControlLabel, FormLabel, Grid, makeStyles, Radio, RadioGroup } from '@material-ui/core';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { CssTextField } from 'src/features/Authentication/styles';

export default function Edit() {
    const classes = useStyles();

    const { register, handleSubmit, control, errors } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: {
            email: '',
            password: '',
            remember: true,
        },
    });

    const onSubmit = (data: any) => {

    }

    return (
        <div>
            <form className={classes.form} noValidate onSubmit={handleSubmit(onSubmit)}>
                <Grid container>
                    <DevTool control={control} />
                    <Grid item xs={6} className={classes.gridLeft}>
                        <CssTextField
                            name='email'
                            label='Email Address'
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
                        <CssTextField
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
                    </Grid>
                    <Grid item xs={6} className={classes.gridRight}>
                    <CssTextField
                            name='email'
                            label='Email Address'
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
                        <CssTextField
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
                        <FormControl component="fieldset">
                            <FormLabel component="legend">Gender</FormLabel>
                            <RadioGroup row aria-label="gender" name="gender1">
                                <FormControlLabel value="female" control={<Radio />} label="Female" />
                                <FormControlLabel value="male" control={<Radio />} label="Male" />
                                <FormControlLabel value="other" control={<Radio />} label="Other" />
                            </RadioGroup>
                        </FormControl>
                    </Grid>
                </Grid>
            </form>
        </div>
    );
}

const mingColor = '#387780';
const emeraldGreenColor = '#62C370';
export const useStyles = makeStyles(theme => {
    return {
        paper: {
            margin: theme.spacing(4, 0),
            display: 'flex',
            color: '#387780',
            flexDirection: 'column',
            alignItems: 'center',
            border: `1px solid ${emeraldGreenColor}`,
            borderRadius: '2rem',
            padding: '1.5rem 2.5rem',
        },
        avatar: {
            margin: theme.spacing(3),
            backgroundColor: emeraldGreenColor,
            fontSize: 50,
        },
        form: {
            marginTop: theme.spacing(4),
            width: '100%',
        },
        gridLeft: {
            paddingRight: theme.spacing(1),
        },
        gridRight: {
            paddingLeft: theme.spacing(1),
        },
        submit: {
            margin: theme.spacing(3, 0, 2),
            backgroundColor: emeraldGreenColor,
            color: 'white',
            padding: '50 50',
        },
        link: {
            color: mingColor,
            textDecoration: 'none !important',
        },
        checkBox: {
            color: `${emeraldGreenColor} !important`,
        },
        error: {
            color: 'red',
        },
    };
});

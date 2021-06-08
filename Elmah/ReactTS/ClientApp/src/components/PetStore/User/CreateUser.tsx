import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { FormTypes, IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import FormPopup from '../../FormPopup';

import { createUser } from 'src/features/PetStore/UserSlice';
import { User, createUserDefault } from 'src/features//PetStore/User';
import { FormControl } from '@material-ui/core';
import { StyledTextField } from '../../controls/StyledTextField';

export default function CreateUser(props: IFormProps<User> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
	const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);

    const { openPopup, setOpenPopup } = props;

    const formValidations = {
        id: {

        },
        username: {

        },
        firstName: {

        },
        lastName: {

        },
        email: {

        },
        password: {

        },
        phone: {

        },
        userStatus: {

        }
    };

	// TODO: add "setValue," if you need setValue
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createUserDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item } : createUserDefault()
    const popupButtonsOptions = createEditFormButtonsOptions(t('UIStringResource:Save'), t('UIStringResource:Reset'), () => { reset({ ...inputData }) }, t('UIStringResource:Cancel'), closePopup);

    const onSubmit = (data: any) => {
        const dataToUpsert = { id: 0, ...props.item, ...data };
        dispatch(createUser(dataToUpsert))
        console.log(data);

        setOpenPopup(false);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        reset(inputData);
    }, []);

    const renderItem = () => {
        return (
            <Grid container={true}>
                <DevTool control={control} />
                <Grid item lg={12}>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='id'
                            label={t('UIStringResource_PetStore:Id')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.id)}
                            error={!!errors.id}
                            fullWidth
                            autoFocus
                        />
                        {errors.id && (
                            <span className={classes.error}>{errors.id.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='username'
                            label={t('UIStringResource_PetStore:Username')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.username)}
                            error={!!errors.username}
                            fullWidth
                            autoFocus
                        />
                        {errors.username && (
                            <span className={classes.error}>{errors.username.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='firstName'
                            label={t('UIStringResource_PetStore:FirstName')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.firstName)}
                            error={!!errors.firstName}
                            fullWidth
                            autoFocus
                        />
                        {errors.firstName && (
                            <span className={classes.error}>{errors.firstName.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='lastName'
                            label={t('UIStringResource_PetStore:LastName')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.lastName)}
                            error={!!errors.lastName}
                            fullWidth
                            autoFocus
                        />
                        {errors.lastName && (
                            <span className={classes.error}>{errors.lastName.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='email'
                            label={t('UIStringResource_PetStore:Email')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.email)}
                            error={!!errors.email}
                            fullWidth
                            autoFocus
                        />
                        {errors.email && (
                            <span className={classes.error}>{errors.email.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='password'
                            label={t('UIStringResource_PetStore:Password')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.password)}
                            error={!!errors.password}
                            fullWidth
                            autoFocus
                        />
                        {errors.password && (
                            <span className={classes.error}>{errors.password.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='phone'
                            label={t('UIStringResource_PetStore:Phone')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.phone)}
                            error={!!errors.phone}
                            fullWidth
                            autoFocus
                        />
                        {errors.phone && (
                            <span className={classes.error}>{errors.phone.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='userStatus'
                            label={t('UIStringResource_PetStore:UserStatus')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.userStatus)}
                            error={!!errors.userStatus}
                            fullWidth
                            autoFocus
                        />
                        {errors.userStatus && (
                            <span className={classes.error}>{errors.userStatus.message}</span>
                        )}
                    </FormControl>
                </Grid>
            </Grid>
        );
    }

    return (
        <>
            {props.wrapperType === WrapperTypes.DialogForm &&
                <FormPopup
                    title={t('UIStringResource_PetStore:User')}
                    openPopup={openPopup}
                    setOpenPopup={setOpenPopup}
                    submitDisabled={!formState.isValid}
                    handleSubmit={handleSubmit(onSubmit)}
                    buttons={popupButtonsOptions}
                >
                    {renderItem()}
                </FormPopup>
            }
            {props.wrapperType !== WrapperTypes.DialogForm &&
                <>
                    {renderItem()}
                </>
            }
        </>
    );
}


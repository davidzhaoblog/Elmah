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

import { updatePet } from 'src/features/PetStore/PetSlice';
import { Pet, createPetDefault } from 'src/features//PetStore/Pet';
import { FormControl } from '@material-ui/core';
import { StyledTextField } from '../../controls/StyledTextField';

export default function UpdatePet(props: IFormProps<Pet> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
	const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);

    const { openPopup, setOpenPopup } = props;

    const formValidations = {
        id: {

        },
        name: {
            required: t('UIStringResourcePet_PetStore:Name_is_required'),
        },
        category: {

        },
        photoUrls: {
            required: t('UIStringResourcePet_PetStore:PhotoUrls_is_required'),
        },
        tags: {

        },
        status: {

        }
    };

	// TODO: add "setValue," if you need setValue
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createPetDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item } : createPetDefault()
    const popupButtonsOptions = createEditFormButtonsOptions(t('UIStringResource:Save'), t('UIStringResource:Reset'), () => { reset({ ...inputData }) }, t('UIStringResource:Cancel'), closePopup);

    const onSubmit = (data: any) => {
        const dataToUpsert = { id: 0, ...props.item, ...data };
        dispatch(updatePet(dataToUpsert))
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
                            name='name'
                            label={t('UIStringResource_PetStore:Name')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.name)}
                            error={!!errors.name}
                            fullWidth
                            autoFocus
                        />
                        {errors.name && (
                            <span className={classes.error}>{errors.name.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='status'
                            label={t('UIStringResource_PetStore:Status')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.status)}
                            error={!!errors.status}
                            fullWidth
                            autoFocus
                        />
                        {errors.status && (
                            <span className={classes.error}>{errors.status.message}</span>
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
                    title={t('UIStringResource_PetStore:Pet')}
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


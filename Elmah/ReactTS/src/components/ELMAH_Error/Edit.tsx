import React, { useEffect } from 'react'
import { useForm } from 'react-hook-form';
import { useDispatch } from 'react-redux';
import { DevTool } from '@hookform/devtools';
import { Grid } from '@material-ui/core';
import { CssTextField } from 'src/features/Authentication/styles';
import { FormTypes, IFormProps } from 'src/framework/ViewModels/IFormProps';
import FormPopup from '../FormPopup';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import { createELMAH_ErrorDefault, ELMAH_Error } from 'src/features/ELMAH_Error/types';
import { upsert } from 'src/features/ELMAH_Error/elmah_ErrorSlice';

export default function Edit(props: IFormProps<ELMAH_Error> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { openPopup, setOpenPopup } = props;

    const { register, setValue, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createELMAH_ErrorDefault()
    });

    const closePopup = () => {
        // setRecordForEdit(item)
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? props.item : createELMAH_ErrorDefault()
    const popupButtonsOptions = createEditFormButtonsOptions(()=>{reset({...inputData})}, closePopup);

    const onSubmit = (data: any) => {
        if (!data.user.trim()) {
            return
        }
        const dataToUpsert = { errorId: 0, ...props.item, ...data };
        dispatch(upsert(dataToUpsert))

        setValue('user', '');
        setOpenPopup(false);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        reset(inputData);
    }, []);

    return (
        <FormPopup
            title="ELMAH Error Form"
            openPopup={openPopup}
            setOpenPopup={setOpenPopup}
            submitDisabled={!formState.isValid}
            handleSubmit={handleSubmit(onSubmit)}
            buttons={popupButtonsOptions}
        >
            <Grid container={true}>
                <DevTool control={control} />
                <Grid item lg={12}>
                    <CssTextField
                        name='user'
                        label='user'
                        variant='outlined'
                        margin='normal'
                        inputRef={register({
                            required: 'You must provide the user!',
                            minLength: {
                                value: 6,
                                message: 'user must be greater than 6 characters',
                            },
                        })}
                        error={!!errors.user}
                        fullWidth
                        autoFocus
                    />
                    {errors.user && (
                        <span className={classes.error}>{errors.user.message}</span>
                    )}
                </Grid>
            </Grid>
        </FormPopup>
    );
}

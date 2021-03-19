import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { FormControl, Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { StyledTextField } from '../controls/StyledTextField';

import { FormTypes, IFormProps } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import FormPopup from '../FormPopup';

import { 

} from 'src/features/listSlices';

import { upsert } from 'src/features/ElmahStatusCode/Slice';
import { createElmahStatusCodeDefault, ElmahStatusCode } from 'src/features/ElmahStatusCode/Types';

export default function Edit(props: IFormProps<ElmahStatusCode> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup } = props;

    const formValidations = {
        statusCode: {
            required: t('UIStringResourcePerEntity:StatusCode_is_required'),
        },
        name: {
            maxLength: {
                value: 50,
                message: t('UIStringResourcePerEntity:The_length_of_Name_should_be_0_to_50'),
            }
        },

    };



	// TODO: add "setValue," if you need setValue
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createElmahStatusCodeDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item } : createElmahStatusCodeDefault()
    const popupButtonsOptions = createEditFormButtonsOptions(() => { reset({ ...inputData }) }, closePopup);

    const onSubmit = (data: any) => {
        const dataToUpsert = { errorId: 0, ...props.item, ...data };
        dispatch(upsert(dataToUpsert))
        console.log(data);

        setOpenPopup(false);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        reset(inputData);

    }, []);

    return (
        <FormPopup
            title={t('UIStringResourcePerApp:ElmahStatusCode')}
            openPopup={openPopup}
            setOpenPopup={setOpenPopup}
            submitDisabled={!formState.isValid}
            handleSubmit={handleSubmit(onSubmit)}
            buttons={popupButtonsOptions}
        >
            <Grid container={true}>
                <DevTool control={control} />
                <Grid item lg={12}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='statusCode'
                            label={t('UIStringResourcePerEntity:StatusCode')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.statusCode)}
                            error={!!errors.statusCode}
                            fullWidth
                            autoFocus
                        />
                        {errors.statusCode && (
                            <span className={classes.error}>{errors.statusCode.message}</span>
                        )}
					</FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='name'
                            label={t('UIStringResourcePerEntity:Name')}
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
                </Grid>
            </Grid>
        </FormPopup>
    );
}



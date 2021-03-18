import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { FormControl, FormControlLabel, Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { StyledTextField } from '../controls/StyledTextField';

import { RootState } from 'src/store/CombinedReducers';
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
        user: {
            required: true,
            maxLength: {
                value: 50,
                message: t('UIStringResourcePerEntity:The_length_of_User_should_be_1_to_50'),
            }
        }
    };



    const { register, setValue, handleSubmit, control, errors, formState, reset } = useForm({
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

        setValue('user', '');
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
                        <StyledTextField
                            name='statusCode'
                            label={t('Elmah.Resx.UIStringResourcePerEntity:StatusCode')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.statusCode)}
                            error={!!errors.statusCode}
                            fullWidth
                            autoFocus
                        />
                        {errors.user && (
                            <span className={classes.error}>{errors.statusCode.message}</span>
                        )}
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='name'
                            label={t('Elmah.Resx.UIStringResourcePerEntity:Name')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.name)}
                            error={!!errors.name}
                            fullWidth
                            autoFocus
                        />
                        {errors.user && (
                            <span className={classes.error}>{errors.name.message}</span>
                        )}
                    </FormControl>
                </Grid>
            </Grid>
        </FormPopup>
    );
}



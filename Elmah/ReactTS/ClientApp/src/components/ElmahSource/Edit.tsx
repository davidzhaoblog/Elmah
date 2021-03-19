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

import { upsert } from 'src/features/ElmahSource/Slice';
import { createElmahSourceDefault, ElmahSource } from 'src/features/ElmahSource/Types';

export default function Edit(props: IFormProps<ElmahSource> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup } = props;

    const formValidations = {
        source: {
            minLength: {
                value: 1,
                message: t('UIStringResourcePerEntity:The_length_of_Source_should_be_1_to_60'),
            },
            maxLength: {
                value: 60,
                message: t('UIStringResourcePerEntity:The_length_of_Source_should_be_1_to_60'),
            }
        },

    };



	// TODO: add "setValue," if you need setValue
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createElmahSourceDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item } : createElmahSourceDefault()
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
            title={t('UIStringResourcePerApp:ElmahSource')}
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
                            name='source'
                            label={t('UIStringResourcePerEntity:Source')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.source)}
                            error={!!errors.source}
                            fullWidth
                            autoFocus
                        />
                        {errors.source && (
                            <span className={classes.error}>{errors.source.message}</span>
                        )}
					</FormControl>
                </Grid>
            </Grid>
        </FormPopup>
    );
}



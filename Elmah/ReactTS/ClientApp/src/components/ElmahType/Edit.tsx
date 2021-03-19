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

import { upsert } from 'src/features/ElmahType/Slice';
import { createElmahTypeDefault, ElmahType } from 'src/features/ElmahType/Types';

export default function Edit(props: IFormProps<ElmahType> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup } = props;

    const formValidations = {
        type: {
            minLength: {
                value: 1,
                message: t('UIStringResourcePerEntity:The_length_of_Type_should_be_1_to_100'),
            },
            maxLength: {
                value: 100,
                message: t('UIStringResourcePerEntity:The_length_of_Type_should_be_1_to_100'),
            }
        },

    };



	// TODO: add "setValue," if you need setValue
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createElmahTypeDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item } : createElmahTypeDefault()
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
            title={t('UIStringResourcePerApp:ElmahType')}
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
                            name='type'
                            label={t('UIStringResourcePerEntity:Type')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.type)}
                            error={!!errors.type}
                            fullWidth
                            autoFocus
                        />
                        {errors.type && (
                            <span className={classes.error}>{errors.type.message}</span>
                        )}
					</FormControl>
                </Grid>
            </Grid>
        </FormPopup>
    );
}



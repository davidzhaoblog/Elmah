import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { FormControl, Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { StyledTextField } from '../controls/StyledTextField';

import { FormTypes, IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import FormPopup from '../FormPopup';

import { 

} from 'src/features/listSlices';

import { upsert } from 'src/features/ElmahHost/Slice';
import { createElmahHostDefault, ElmahHost } from 'src/features/ElmahHost/Types';

export default function Edit(props: IFormProps<ElmahHost> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup } = props;

    const formValidations = {
        host: {
            minLength: {
                value: 1,
                message: t('UIStringResourcePerEntity:The_length_of_Host_should_be_1_to_50'),
            },
            maxLength: {
                value: 50,
                message: t('UIStringResourcePerEntity:The_length_of_Host_should_be_1_to_50'),
            }
        },

    };



	// TODO: add "setValue," if you need setValue
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createElmahHostDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item } : createElmahHostDefault()
    const popupButtonsOptions = createEditFormButtonsOptions(t('UIStringResource:Save'), t('UIStringResource:Reset'), () => { reset({ ...inputData }) }, t('UIStringResource:Cancel'), closePopup);

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

    const renderItem = () => {
        return (
            <Grid container={true}>
                <DevTool control={control} />
                <Grid item lg={12}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='host'
                            label={t('UIStringResourcePerEntity:Host')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.host)}
                            error={!!errors.host}
                            fullWidth
                            autoFocus
                        />
                        {errors.host && (
                            <span className={classes.error}>{errors.host.message}</span>
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
                    title={t('UIStringResourcePerApp:ElmahHost')}
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


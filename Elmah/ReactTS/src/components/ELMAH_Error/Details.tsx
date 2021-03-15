import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux';
import { useForm } from 'react-hook-form';
import { Grid, InputLabel, Typography } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { FormTypes, IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { createELMAH_ErrorDefault, ELMAH_Error } from 'src/features/ELMAH_Error/types';
import { upsert } from 'src/features/ELMAH_Error/elmah_ErrorSlice';
import FormPopup from '../FormPopup';
import { StyledCheckbox } from '../controls/StyledCheckbox';

export default function Details(props: IFormProps<ELMAH_Error> & IPopupProps) {
    console.log(props);
    console.log(props.item);

    const dispatch = useDispatch();
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup } = props;

    const { setValue, handleSubmit, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createELMAH_ErrorDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item, testCheckBox: true } : createELMAH_ErrorDefault()
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


    const renderItem = () => {
        return (
            <div className={classes.root}>
            <Grid container spacing={3}>
              <Grid item xs>
                <InputLabel shrink>{t('UIStringResourcePerEntity:user')}</InputLabel>
                    <Typography>{props.item?.user}</Typography>
              </Grid>
              <Grid item xs>
              <InputLabel shrink>{t('UIStringResourcePerEntity:Host')}</InputLabel>
                    <Link to={{ pathname: '/elmah_error/details/4b090093-ffaa-4ee9-a891-83cb0a1019cc' }} >{t('UIStringResourcePerApp:ElmahApplication')}</Link>
              </Grid>
              <Grid item xs>
              <InputLabel shrink>{t('UIStringResourcePerEntity:testCheckBox')}</InputLabel>
                    <StyledCheckbox checked={props.item.testCheckBox} name="testCheckBox" disabled />
              </Grid>
            </Grid>
            <Grid container spacing={3}>
              <Grid item xs>
                <Paper className={classes.paper}>
                <InputLabel shrink>{t('UIStringResourcePerEntity:testCheckBox')}</InputLabel>
                    <StyledCheckbox checked={props.item.testCheckBox} name="testCheckBox" disabled />
                    
                </Paper>
              </Grid>
              <Grid item xs={6}>
                <Paper className={classes.paper}>xs=6</Paper>
              </Grid>
              <Grid item xs>
                <Paper className={classes.paper}>xs</Paper>
              </Grid>
            </Grid>
          </div>
        );
    };

    return (
        <>
            {props.wrapperType === WrapperTypes.DialogForm &&
                <FormPopup
                    title={t('UIStringResourcePerApp:ELMAH_Error')}
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

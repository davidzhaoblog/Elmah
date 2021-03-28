import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux';
import { Controller, useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { FormControl, Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { InputLabel } from '@material-ui/core';
import { Select } from '@material-ui/core';
// import { Typography } from '@material-ui/core';
// import { Controller } from 'react-hook-form';
// import { KeyboardDatePicker } from '@material-ui/pickers';
import { StyledTextField } from '../controls/StyledTextField';
import { useSelector } from 'react-redux';
import { RootState } from 'src/store/CombinedReducers';

import { ISearchFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createSearchFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import FormPopup from '../FormPopup';
import { showSpinner } from 'src/layout/appSlice';

import {
    elmahApplicationListSelector, getElmahApplicationList,
    elmahHostListSelector, getElmahHostList,
    elmahSourceListSelector, getElmahSourceList,
    elmahStatusCodeListSelector, getElmahStatusCodeList,
    elmahTypeListSelector, getElmahTypeList,
    elmahUserListSelector, getElmahUserList
} from 'src/features/listSlices';

import { ELMAH_ErrorCommonCriteria } from 'src/features/ELMAH_Error/Types';
import { getIndexVM } from 'src/features/ELMAH_Error/Slice';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { convertToDateTimeRange, getPreDefinedDateTimeRanges, PreDefinedDateTimeRanges } from 'src/framework/Queries/PreDefinedDateTimeRanges';
import { gePredefinedBooleanValues } from 'src/framework/Queries/PredefinedBooleanValues';

export default function IndexSearch(props: ISearchFormProps<ELMAH_ErrorCommonCriteria> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { t } = useTranslation(["translation", "UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup, criteria, orderBy, queryPagingSetting } = props;

    const formValidations = {
        elmahApplication_Name: {
        },
        errorId: {
            required: t('UIStringResourcePerEntity:ErrorId_is_required'),
        },
        elmahHost_Name: {
        },
        elmahSource_Name: {
        },
        elmahStatusCode_Name: {
        },
        elmahType_Name: {
        },
        elmahUser_Name: {
        },
        application: {
        },
        host: {
        },
        type: {
        },
        source: {
        },
        message: {
            minLength: {
                value: 1,
                message: t('UIStringResourcePerEntity:The_length_of_Message_should_be_1_to_500'),
            },
            maxLength: {
                value: 500,
                message: t('UIStringResourcePerEntity:The_length_of_Message_should_be_1_to_500'),
            }
        },
        user: {
        },
        statusCode: {
        },
        timeUtc: {
            required: t('UIStringResourcePerEntity:TimeUtc_is_required'),
        },
        sequence: {
        },
        allXml: {
            minLength: {
                value: 1,
                message: t('UIStringResourcePerEntity:The_length_of_AllXml_should_be_1_to_'),
            }
        },
        testBoolean: {
        },
    };

    const elmahApplicationList = useSelector(
        (state: RootState) => elmahApplicationListSelector.selectAll(state)
    );
    const elmahHostList = useSelector(
        (state: RootState) => elmahHostListSelector.selectAll(state)
    );
    const elmahSourceList = useSelector(
        (state: RootState) => elmahSourceListSelector.selectAll(state)
    );
    const elmahStatusCodeList = useSelector(
        (state: RootState) => elmahStatusCodeListSelector.selectAll(state)
    );
    const elmahTypeList = useSelector(
        (state: RootState) => elmahTypeListSelector.selectAll(state)
    );
    const elmahUserList = useSelector(
        (state: RootState) => elmahUserListSelector.selectAll(state)
    );


    // TODO: add "setValue," if you need setValue
    const { register, setValue, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: criteria
    });

    const timeUtcRangePredefinedList = getPreDefinedDateTimeRanges();
    const onChanged_TimeUtcRangePredefined = (predefined: PreDefinedDateTimeRanges) => {
        const timeUtcRange = convertToDateTimeRange(predefined);
        setValue('timeUtcRange.lower', timeUtcRange.lower);
        setValue('timeUtcRange.upper', timeUtcRange.upper);
    }

    const testBooleanPredefinedList = gePredefinedBooleanValues();

    const closePopup = () => {
        setOpenPopup(false)
    }

    const popupButtonsOptions = createSearchFormButtonsOptions(() => { reset(criteria) }, closePopup);

    const onSubmit = (data: any) => {
        dispatch(showSpinner());
        dispatch(getIndexVM({ criteria: data, orderBy, queryPagingSetting }));

        console.log(data);

        setOpenPopup(false);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        dispatch(getElmahApplicationList());
        dispatch(getElmahHostList());
        dispatch(getElmahSourceList());
        dispatch(getElmahStatusCodeList());
        dispatch(getElmahTypeList());
        dispatch(getElmahUserList());
        reset(criteria);

    }, []);

    const renderItem = () => {
        return (
            <Grid container={true}>
                <DevTool control={control} />
                <Grid item lg={12}>
                    {/* <Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:ErrorId')}</InputLabel>
						<Typography>{props.criteria?.errorId}</Typography>
					</Grid> */}
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='stringContains_AllColumns'
                            label={t('Search')}
                            inputRef={register()}
                            variant='outlined'
                            margin='normal'
                            fullWidth
                            autoFocus
                        />
                    </FormControl>
                </Grid>
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:Application')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:Application')}
                            name='application'
                            inputRef={register(formValidations.application)}
                        >
                            <option aria-label="None" value="" />
                            {elmahApplicationList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:Host')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:Host')}
                            name='host'
                            inputRef={register(formValidations.host)}
                        >
                            <option aria-label="None" value="" />
                            {elmahHostList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:Type')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:Type')}
                            name='type'
                            inputRef={register(formValidations.type)}
                        >
                            <option aria-label="None" value="" />
                            {elmahTypeList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:Source')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:Source')}
                            name='source'
                            inputRef={register(formValidations.source)}
                        >
                            <option aria-label="None" value="" />
                            {elmahSourceList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item lg={12}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='message'
                            label={t('UIStringResourcePerEntity:Message')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.message)}
                            error={!!errors.message}
                            fullWidth
                            autoFocus
                        />
                        {errors.message && (
                            <span className={classes.error}>{errors.message.message}</span>
                        )}
                    </FormControl>
                </Grid>
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:User')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:User')}
                            name='user'
                            inputRef={register(formValidations.user)}
                        >
                            <option aria-label="None" value="" />
                            {elmahUserList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:StatusCode')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:StatusCode')}
                            name='statusCode'
                            inputRef={register(formValidations.statusCode)}
                        >
                            <option aria-label="None" value="" />
                            {elmahStatusCodeList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item lg={4}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:Source')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:TimeUtc')}
                            name='timeUtcRangePredefined'
                            inputRef={register(formValidations.source)}
                            onChange={e => onChanged_TimeUtcRangePredefined(PreDefinedDateTimeRanges[e.target.value.toString()])}
                        >
                            {timeUtcRangePredefinedList.map((item: any) => {
                                return (
                                    <option value={item} key={item}>{t('' + item)}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item lg={4}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Controller
                            as={
                                <KeyboardDatePicker
                                    label={t('UIStringResourcePerEntity:TimeUtc')}
                                    clearable
                                    format="MMM DD, yyyy"
                                    views={["year", "month", "date"]}
                                    inputVariant="outlined"
                                    margin="dense"
                                    InputAdornmentProps={{ position: "start" }}
                                    value=""
                                    onChange={() => { }}
                                />
                            }
                            name='timeUtcRange.lower'
                            defaultValue={new Date()}
                            control={control}
                        />
                    </FormControl>
                </Grid>
                <Grid item lg={4}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Controller
                            as={
                                <KeyboardDatePicker
                                    label={t('UIStringResourcePerEntity:TimeUtc')}
                                    clearable
                                    format="MMM DD, yyyy"
                                    views={["year", "month", "date"]}
                                    inputVariant="outlined"
                                    margin="dense"
                                    InputAdornmentProps={{ position: "start" }}
                                    value=""
                                    onChange={() => { }}
                                />
                            }
                            name='timeUtcRange.upper'
                            defaultValue={new Date()}
                            control={control}
                        />
                    </FormControl>
                    {/* <Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:Sequence')}</InputLabel>
						<Typography>{props.item?.sequence}</Typography>
					</Grid> */}
                </Grid>
                <Grid item lg={12}>                    
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='allXml'
                            label={t('UIStringResourcePerEntity:AllXml')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.allXml)}
                            error={!!errors.allXml}
                            fullWidth
                            autoFocus
                        />
                        {errors.allXml && (
                            <span className={classes.error}>{errors.allXml.message}</span>
                        )}
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:StatusCode')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:StatusCode')}
                            name='testBoolean'
                            inputRef={register(formValidations.testBoolean)}
                        >
                            {testBooleanPredefinedList.map((item: any) => {
                                return (
                                    <option value={item} key={item}>{t('' + item)}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                </Grid>
            </Grid>
        );
    }

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



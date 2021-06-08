import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { FormControl, Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { ISearchFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createSearchFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import FormPopup from '../FormPopup';
import { showSpinner } from 'src/layout/appSlice';

import { StyledTextField } from '../controls/StyledTextField';
import { useSelector } from 'react-redux';
import { RootState } from 'src/store/CombinedReducers';
import { InputLabel } from '@material-ui/core';
import { Select } from '@material-ui/core';
import { Controller } from 'react-hook-form';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { convertToDateTimeRange, getPreDefinedDateTimeRanges, PreDefinedDateTimeRanges } from 'src/framework/Queries/PreDefinedDateTimeRanges';

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

export default function Search(props: ISearchFormProps<ELMAH_ErrorCommonCriteria> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { t } = useTranslation(["translation", "UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup, criteria, orderBy, queryPagingSetting } = props;

	// all form validations are empty, can be removed if not in use
    const formValidations = {
		application: {},
		host: {},
		source: {},
		statusCode: {},
		type: {},
		user: {},
		message: {},
		allXml: {},
		timeUtcRange: {},
		stringContains_AllColumns: {}
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

    // TODO: add "setValue," if you need setValue, setValue will be added when DateTimeRange
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

    const closePopup = () => {
        setOpenPopup(false)
    }

    const popupButtonsOptions = createSearchFormButtonsOptions(t('UIStringResource:Search'), t('UIStringResource:Reset'), () => { reset(criteria) }, t('UIStringResource:Cancel'), closePopup);

    const onSubmit = (data: any) => {
        dispatch(showSpinner());
        dispatch(getIndexVM({ criteria: data, orderBy, queryPagingSetting }));

        // console.log(data);

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
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='stringContains_AllColumns'
                            label={t('Search')}
                            inputRef={register(formValidations.stringContains_AllColumns)}
                            variant='outlined'
                            margin='normal'
                            fullWidth
                            autoFocus
                        />
                          {errors.stringContains_AllColumns && (
                            <span className={classes.error}>{errors.stringContains_AllColumns.message}</span>
                        )}
					</FormControl>
                </Grid>
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerApp:ElmahUser')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerApp:ElmahUser')}
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
                        <InputLabel shrink>{t('UIStringResourcePerApp:ElmahType')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerApp:ElmahType')}
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
                        <InputLabel shrink>{t('UIStringResourcePerApp:ElmahStatusCode')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerApp:ElmahStatusCode')}
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
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerApp:ElmahSource')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerApp:ElmahSource')}
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
                <Grid item lg={6}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerApp:ElmahHost')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerApp:ElmahHost')}
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
                        <InputLabel shrink>{t('UIStringResourcePerApp:ElmahApplication')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerApp:ElmahApplication')}
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
                <Grid item lg={4}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:TimeUtcRange')}</InputLabel>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:TimeUtc')}
                            name='timeUtcRangePredefined'
                            inputRef={register(formValidations.timeUtcRange)}
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


import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { FormControl, Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { InputLabel, Select } from '@material-ui/core';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { StyledTextField } from '../controls/StyledTextField';

import { RootState } from 'src/store/CombinedReducers';
import { FormTypes, IFormProps } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import FormPopup from '../FormPopup';

import { 
    elmahApplicationListSelector, getElmahApplicationList,
    elmahHostListSelector, getElmahHostList,
    elmahSourceListSelector, getElmahSourceList,
    elmahStatusCodeListSelector, getElmahStatusCodeList,
    elmahTypeListSelector, getElmahTypeList,
    elmahUserListSelector, getElmahUserList
} from 'src/features/listSlices';

import { upsert } from 'src/features/ELMAH_Error/Slice';
import { createELMAH_ErrorDefault, ELMAH_Error } from 'src/features/ELMAH_Error/Types';

export default function Edit(props: IFormProps<ELMAH_Error> & IPopupProps) {
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

    const { register, setValue, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createELMAH_ErrorDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item } : createELMAH_ErrorDefault()
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
        dispatch(getElmahApplicationList());
        dispatch(getElmahHostList());
        dispatch(getElmahSourceList());
        dispatch(getElmahStatusCodeList());
        dispatch(getElmahTypeList());
        dispatch(getElmahUserList());
    }, []);

    return (
        <FormPopup
            title={t('UIStringResourcePerApp:ELMAH_Error')}
            openPopup={openPopup}
            setOpenPopup={setOpenPopup}
            submitDisabled={!formState.isValid}
            handleSubmit={handleSubmit(onSubmit)}
            buttons={popupButtonsOptions}
        >
            <Grid container={true}>
                <DevTool control={control} />
                <Grid item lg={12}>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:ErrorId')}</InputLabel>
						<Typography>{props.item.errorId}</Typography>
					</Grid>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:Application')}
                            name='application'
                            inputRef={register}
                        >
                            <option aria-label="None" value="" />
                            {elmahApplicationList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:Host')}
                            name='host'
                            inputRef={register}
                        >
                            <option aria-label="None" value="" />
                            {elmahHostList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:Type')}
                            name='type'
                            inputRef={register}
                        >
                            <option aria-label="None" value="" />
                            {elmahTypeList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:Source')}
                            name='source'
                            inputRef={register}
                        >
                            <option aria-label="None" value="" />
                            {elmahSourceList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='message'
                            label={t('Elmah.Resx.UIStringResourcePerEntity:Message')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.message)}
                            error={!!errors.message}
                            fullWidth
                            autoFocus
                        />
                        {errors.user && (
                            <span className={classes.error}>{errors.message.message}</span>
                        )}
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:User')}
                            name='user'
                            inputRef={register}
                        >
                            <option aria-label="None" value="" />
                            {elmahUserList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Select
                            native
                            label={t('UIStringResourcePerEntity:StatusCode')}
                            name='statusCode'
                            inputRef={register}
                        >
                            <option aria-label="None" value="" />
                            {elmahStatusCodeList.map((item: any) => {
                                return (
                                    <option value={item.value} key={item.value}>{item.name}</option>
                                );
                            })}
                        </Select>
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <Controller
                            as={
                                <KeyboardDatePicker
                                    label={t('Elmah.Resx.UIStringResourcePerEntity:TimeUtc')}
                                    clearable
                                    format="MMM DD, yyyy"
                                    views={["year", "month", "date"]}
                                    inputVariant="outlined"
                                    margin="dense"
                                    InputAdornmentProps={{ position: "start" }}
                                    error={!!errors?.timeUtc}
                                    value=""
                                    onChange={() => {}}
                                />
                            }
                            name='timeUtc'
                            defaultValue={new Date()}
                            rules={{ required: "Field Required" }}
                            control={control}
                        />
                    </FormControl>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:Sequence')}</InputLabel>
						<Typography>{props.item.sequence}</Typography>
					</Grid>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='allXml'
                            label={t('Elmah.Resx.UIStringResourcePerEntity:AllXml')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.allXml)}
                            error={!!errors.allXml}
                            fullWidth
                            autoFocus
                        />
                        {errors.user && (
                            <span className={classes.error}>{errors.allXml.message}</span>
                        )}
                    </FormControl>
                </Grid>
            </Grid>
        </FormPopup>
    );
}



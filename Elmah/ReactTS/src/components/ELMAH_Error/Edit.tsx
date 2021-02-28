import React, { useEffect } from 'react'
import { Controller, useForm } from 'react-hook-form';
import { useDispatch, useSelector } from 'react-redux';
import { DevTool } from '@hookform/devtools';
import { FormControl, FormControlLabel, Grid, InputLabel, Select } from '@material-ui/core';
import { RootState } from 'src/store/CombinedReducers';
import { FormTypes, IFormProps } from 'src/framework/ViewModels/IFormProps';
import FormPopup from '../FormPopup';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import { createELMAH_ErrorDefault, ELMAH_Error } from 'src/features/ELMAH_Error/types';
// import { upsert } from 'src/features/ELMAH_Error/elmah_ErrorSlice';
import { elmahHostListSelector, getElmahHostList } from 'src/features/listSlices';
import { StyledCheckbox } from '../controls/StyledCheckbox';
import { StyledTextField } from '../controls/StyledTextField';
import { KeyboardDatePicker } from '@material-ui/pickers';

export default function Edit(props: IFormProps<ELMAH_Error> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { openPopup, setOpenPopup } = props;

    const elmahHostList = useSelector(
        (state: RootState) => elmahHostListSelector.selectAll(state)
    );

    const { register, setValue, handleSubmit, control, errors, formState, reset } = useForm({
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
        // const dataToUpsert = { errorId: 0, ...props.item, ...data };
        // dispatch(upsert(dataToUpsert))
        console.log(data);

        setValue('user', '');
        setOpenPopup(false);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        reset(inputData);
        dispatch(getElmahHostList());
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
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
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
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel htmlFor="outlined-age-native-simple">Host</InputLabel>
                        <Select
                            native
                            label="host"
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
                        <Controller
                            as={
                                <KeyboardDatePicker
                                    clearable
                                    autoOk
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
                            name="timeUtc"
                            defaultValue={props.item.timeUtc}
                            rules={{ required: "Field Required" }}
                            control={control}
                        />
                    </FormControl>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <FormControlLabel
                            control={
                                <Controller
                                    control={control}
                                    name='testCheckBox'
                                    defaultValue={true}
                                    render={({ onChange, value }) => (
                                        <StyledCheckbox
                                            className={classes.checkBox}
                                            onChange={e => onChange(e.target.checked)}
                                            checked={value}
                                            disabled
                                        />
                                    )}
                                />
                            }
                            label='testCheckBox'
                        />
                    </FormControl>
                </Grid>
            </Grid>
        </FormPopup>
    );
}

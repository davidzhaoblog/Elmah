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
import FormPopup from '../../FormPopup';
import { showSpinner } from 'src/layout/appSlice';

import { StyledTextField } from '../../controls/StyledTextField';

import { FindPetsByStatusCriteria } from 'src/apis/PetStore/PetCriteria';
import { getIndexVM } from 'src/features/ELMAH_Error/Slice';

export default function Search(props: ISearchFormProps<FindPetsByStatusCriteria> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { t } = useTranslation(["translation", "UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup, criteria, orderBy, queryPagingSetting } = props;

	// all form validations are empty, can be removed if not in use
    const formValidations = {
		status: {},
    };

    // const elmahApplicationList = useSelector(
    //     (state: RootState) => elmahApplicationListSelector.selectAll(state)
    // );
    // const elmahHostList = useSelector(
    //     (state: RootState) => elmahHostListSelector.selectAll(state)
    // );
    // const elmahSourceList = useSelector(
    //     (state: RootState) => elmahSourceListSelector.selectAll(state)
    // );
    // const elmahStatusCodeList = useSelector(
    //     (state: RootState) => elmahStatusCodeListSelector.selectAll(state)
    // );
    // const elmahTypeList = useSelector(
    //     (state: RootState) => elmahTypeListSelector.selectAll(state)
    // );
    // const elmahUserList = useSelector(
    //     (state: RootState) => elmahUserListSelector.selectAll(state)
    // );

    // TODO: add "setValue," if you need setValue, setValue will be added when DateTimeRange
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: criteria
    });


    // const timeUtcRangePredefinedList = getPreDefinedDateTimeRanges();
    // const onChanged_TimeUtcRangePredefined = (predefined: PreDefinedDateTimeRanges) => {
    //     const timeUtcRange = convertToDateTimeRange(predefined);
    //     setValue('timeUtcRange.lower', timeUtcRange.lower);
    //     setValue('timeUtcRange.upper', timeUtcRange.upper);
    // }

    const closePopup = () => {
        setOpenPopup(false)
    }

    const popupButtonsOptions = createSearchFormButtonsOptions(() => { reset(criteria) }, closePopup);

    const onSubmit = (data: any) => {
        dispatch(showSpinner());
        dispatch(getIndexVM({ criteria: data, orderBy, queryPagingSetting }));

        // console.log(data);

        setOpenPopup(false);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        // dispatch(getElmahApplicationList());
        // dispatch(getElmahHostList());
        // dispatch(getElmahSourceList());
        // dispatch(getElmahStatusCodeList());
        // dispatch(getElmahTypeList());
        // dispatch(getElmahUserList());
        reset(criteria);

    }, []);

    const renderItem = () => {
        return (
            <Grid container={true}>
                <DevTool control={control} />
                <Grid item lg={12}>
                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='status'
                            label={t('Search')}
                            inputRef={register(formValidations.status)}
                            variant='outlined'
                            margin='normal'
                            fullWidth
                            autoFocus
                        />
                          {errors.status && (
                            <span className={classes.error}>{errors.status.message}</span>
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


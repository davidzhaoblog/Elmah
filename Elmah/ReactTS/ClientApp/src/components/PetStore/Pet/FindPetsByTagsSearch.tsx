import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { FormControl, Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { StyledTextField } from 'src/components/controls/StyledTextField';
import { ISearchFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createSearchFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import FormPopup from '../../FormPopup';
import { showSpinner } from 'src/layout/appSlice';

import { FindPetsByTagsParameters } from 'src/apis/PetStore/PetParameters';
import { findPetsByTags } from 'src/features/PetStore/PetSlice';

export default function FindPetsByTagsSearch(props: ISearchFormProps<FindPetsByTagsParameters> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
	const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);

    const { openPopup, setOpenPopup, criteria } = props;

	// all form validations are empty, can be removed if not in use
    const formValidations = {
		tags: {}
    };

    // TODO: add "setValue," if you need setValue, setValue will be added when DateTimeRange
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: criteria
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

	const popupButtonsOptions = createSearchFormButtonsOptions(t('UIStringResource:Search'), t('UIStringResource:Reset'), () => { reset(criteria) }, t('UIStringResource:Cancel'), closePopup);

    const onSubmit = (data: any) => {
        dispatch(showSpinner());
        dispatch(findPetsByTags(data));

        // console.log(data);

        setOpenPopup(false);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        reset(criteria);

    }, []);

    const renderItem = () => {
        return (
            <Grid container={true}>
                <DevTool control={control} />

                <Grid item lg={12}>
					<FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='tags'
                            label={t('UIStringResource_PetStore:Tags')}
                            inputRef={register(formValidations.tags)}
                            variant='outlined'
                            margin='normal'
                            fullWidth
                            autoFocus
                        />
                          {errors.tags && (
                            <span className={classes.error}>{errors.tags[0].message}</span>
                        )}
					</FormControl>
                </Grid>
                <Grid item lg={12}>
                </Grid>

            </Grid>
        );
    }

    return (
        <>
            {props.wrapperType === WrapperTypes.DialogForm &&
                <FormPopup
                    title={t('UIStringResource_PetStore:Pet')}
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


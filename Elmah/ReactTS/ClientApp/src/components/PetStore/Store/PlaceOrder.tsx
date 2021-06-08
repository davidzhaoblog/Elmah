import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import { Grid } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { FormTypes, IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import FormPopup from '../../FormPopup';

import { placeOrder } from 'src/features/PetStore/OrderSlice';
import { Order, createOrderDefault } from 'src/features//PetStore/Order';
import { FormControl } from '@material-ui/core';
import { Controller } from 'react-hook-form';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { StyledTextField } from '../../controls/StyledTextField';
import { FormControlLabel } from '@material-ui/core';
import { StyledCheckbox } from '../../controls/StyledCheckbox';

export default function PlaceOrder(props: IFormProps<Order> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
	const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);

    const { openPopup, setOpenPopup } = props;

    const formValidations = {
        id: {

        },
        petId: {

        },
        quantity: {

        },
        shipDate: {

        },
        status: {

        },
        complete: {

        }
    };

	// TODO: add "setValue," if you need setValue
    const { register, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createOrderDefault()
    });

    const closePopup = () => {
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? { ...props.item } : createOrderDefault()
    const popupButtonsOptions = createEditFormButtonsOptions(t('UIStringResource:Save'), t('UIStringResource:Reset'), () => { reset({ ...inputData }) }, t('UIStringResource:Cancel'), closePopup);

    const onSubmit = (data: any) => {
        const dataToUpsert = { id: 0, ...props.item, ...data };
        dispatch(placeOrder(dataToUpsert))
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
                            name='id'
                            label={t('UIStringResource_PetStore:Id')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.id)}
                            error={!!errors.id}
                            fullWidth
                            autoFocus
                        />
                        {errors.id && (
                            <span className={classes.error}>{errors.id.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='petId'
                            label={t('UIStringResource_PetStore:PetId')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.petId)}
                            error={!!errors.petId}
                            fullWidth
                            autoFocus
                        />
                        {errors.petId && (
                            <span className={classes.error}>{errors.petId.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='quantity'
                            label={t('UIStringResource_PetStore:Quantity')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.quantity)}
                            error={!!errors.quantity}
                            fullWidth
                            autoFocus
                        />
                        {errors.quantity && (
                            <span className={classes.error}>{errors.quantity.message}</span>
                        )}
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <Controller
                            as={
                                <KeyboardDatePicker
                                    label={t('UIStringResource_PetStore:ShipDate')}
                                    clearable
                                    format="MMM DD, yyyy"
                                    views={["year", "month", "date"]}
                                    inputVariant="outlined"
                                    margin="dense"
                                    InputAdornmentProps={{ position: "start" }}
                                    error={!!errors?.shipDate}
                                    value=""
                                    onChange={() => {}}
                                />
                            }
                            name='shipDate'
                            defaultValue={new Date()}
                            rules={formValidations.shipDate}
                            control={control}
                        />
                    </FormControl>

                    <FormControl variant="outlined" className={classes.formControl}>
                        <StyledTextField
                            name='status'
                            label={t('UIStringResource_PetStore:Status')}
                            variant='outlined'
                            margin='normal'
                            inputRef={register(formValidations.status)}
                            error={!!errors.status}
                            fullWidth
                            autoFocus
                        />
                        {errors.status && (
                            <span className={classes.error}>{errors.status.message}</span>
                        )}
                    </FormControl>

                    <FormControlLabel
                        control={
                            <Controller
                                control={control}
                                name='complete'
                                defaultValue={true}
                                render={({ onChange, value }) => (
                                    <StyledCheckbox
                                        className={classes.checkBox}
                                        onChange={e => onChange(e.target.checked)}
                                        checked={value}
                                    />
                                )}
                            />
                        }
                        label={t('UIStringResource_PetStore:Complete')}
                    />
                </Grid>
            </Grid>
        );
    }

    return (
        <>
            {props.wrapperType === WrapperTypes.DialogForm &&
                <FormPopup
                    title={t('UIStringResource_PetStore:Order')}
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


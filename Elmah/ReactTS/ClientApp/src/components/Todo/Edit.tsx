import React, { useEffect } from 'react'
import { useForm } from 'react-hook-form';
import { useDispatch } from 'react-redux';
import { DevTool } from '@hookform/devtools';
import { Grid } from '@material-ui/core';
import { FormTypes, IFormProps } from 'src/framework/ViewModels/IFormProps';
import { createTodoDefault, Todo } from 'src/features/Todo/types';
import { upsert } from 'src/features/Todo/todoSlice';
import FormPopup from '../FormPopup';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { useStyles } from 'src/features/formStyles';
import { CssTextField } from '../controls/CssTextField';

export default function Edit(props: IFormProps<Todo> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { openPopup, setOpenPopup } = props;

    const { register, setValue, handleSubmit, control, errors, formState, reset } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: props.type === FormTypes.Edit ? props.item : createTodoDefault()
    });

    const closePopup = () => {
        // setRecordForEdit(item)
        setOpenPopup(false)
    }

    const inputData = props.type === FormTypes.Edit ? props.item : createTodoDefault()
    const popupButtonsOptions = createEditFormButtonsOptions(()=>{reset({...inputData})}, closePopup);

    const onSubmit = (data: any) => {
        if (!data.text.trim()) {
            return
        }
        const dataToUpsert = { id: 0, completed: false, ...props.item, ...data };
        dispatch(upsert(dataToUpsert))

        setValue('text', '');
        setOpenPopup(false);
    }

    useEffect(() => {
        // you can do async server request and fill up form
        reset(inputData);
    }, []);

    return (
        <FormPopup
            title="Todo Form"
            openPopup={openPopup}
            setOpenPopup={setOpenPopup}
            submitDisabled={!formState.isValid}
            handleSubmit={handleSubmit(onSubmit)}
            buttons={popupButtonsOptions}
        >
            <Grid container={true}>
                <DevTool control={control} />
                <Grid item lg={12}>
                    <CssTextField
                        name='text'
                        label='text'
                        variant='outlined'
                        margin='normal'
                        inputRef={register({
                            required: 'You must provide the text!',
                            minLength: {
                                value: 6,
                                message: 'Text must be greater than 6 characters',
                            },
                        })}
                        error={!!errors.text}
                        fullWidth
                        autoFocus
                    />
                    {errors.text && (
                        <span className={classes.error}>{errors.text.message}</span>
                    )}
                </Grid>
            </Grid>
        </FormPopup>
    );
}

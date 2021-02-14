import * as React from 'react'
import { useForm } from 'react-hook-form';
import { useDispatch } from 'react-redux';
import { DevTool } from '@hookform/devtools';
import { Grid, makeStyles } from '@material-ui/core';
import { CssTextField } from 'src/features/Authentication/styles';
import { IFormProps } from 'src/framework/ViewModels/IFormProps';
import { Todo } from 'src/features/Todo/types';
import { upsert } from 'src/features/Todo/todoSlice';
import FormPopup from '../FormPopup';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';

export default function Edit(props: IFormProps<Todo> & IPopupProps) {
    const dispatch = useDispatch();
    const classes = useStyles();
    const { openPopup, setOpenPopup } = props;

    const { register, setValue, handleSubmit, control, errors, formState } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: {
            id: 0,
            text: '',
            completed: false,
        }
    });

    const closePopup = () => {
        // setRecordForEdit(item)
        setOpenPopup(false)
    }

    const popupButtonsOptions = createEditFormButtonsOptions(closePopup);

    const onSubmit = (data: any) => {
        if (!data.text.trim()) {
            return
        }
        dispatch(upsert({ ...props.item, ...data, id: 0, completed: false }))

        setValue('text', '');
        setOpenPopup(false);
    }

    return (
        <FormPopup
            title="Todo Form"
            openPopup={openPopup}
            setOpenPopup={setOpenPopup}
            submitDisabled={!formState.isValid}
            handleSubmit={handleSubmit(onSubmit)}
            buttons={popupButtonsOptions}
        >
            <Grid container>
                <DevTool control={control} />
                <Grid item xs={6} className={classes.gridLeft}>
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

const mingColor = '#387780';
const emeraldGreenColor = '#62C370';
export const useStyles = makeStyles(theme => {
    return {
        paper: {
            margin: theme.spacing(4, 0),
            display: 'flex',
            color: '#387780',
            flexDirection: 'column',
            alignItems: 'center',
            border: `1px solid ${emeraldGreenColor}`,
            borderRadius: '2rem',
            padding: '1.5rem 2.5rem',
        },
        avatar: {
            margin: theme.spacing(3),
            backgroundColor: emeraldGreenColor,
            fontSize: 50,
        },
        form: {
            marginTop: theme.spacing(4),
            width: '100%',
        },
        gridLeft: {
            paddingRight: theme.spacing(1),
        },
        gridRight: {
            paddingLeft: theme.spacing(1),
        },
        submit: {
            margin: theme.spacing(3, 0, 2),
            backgroundColor: emeraldGreenColor,
            color: 'white',
            padding: '50 50',
        },
        link: {
            color: mingColor,
            textDecoration: 'none !important',
        },
        checkBox: {
            color: `${emeraldGreenColor} !important`,
        },
        error: {
            color: 'red',
        },
    };
});

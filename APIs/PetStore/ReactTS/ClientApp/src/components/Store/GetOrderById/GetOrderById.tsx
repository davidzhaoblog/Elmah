import React from 'react'
import { Card, CardContent, Grid } from '@material-ui/core';

import { InputLabel } from '@material-ui/core';
import { Typography } from '@material-ui/core';
import { Controller } from 'react-hook-form';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { ReadOnlyTextField } from '../controls/ReadOnlyTextField';
import { StyledCheckbox } from '../controls/StyledCheckbox';

import { useTranslation } from 'react-i18next';

import { IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { useStyles } from 'src/features/formStyles';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import FormPopup from '../FormPopup';

import { Order } from 'src/features/PetStore/Order/Order';

export default function Details(props: IFormProps<Order> & IPopupProps) {
    // console.log(props);
    // console.log(props.item);

    const classes = useStyles();
	const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);

    const { openPopup, setOpenPopup } = props;

    const closePopup = () => {
        setOpenPopup(false)
    }

    const popupButtonsOptions = createEditFormButtonsOptions(() => {}, closePopup);

    const renderItem = () => {
        return (
            <Card className={classes.root} variant="outlined">
                <CardContent>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Id')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.id}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:PetId')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.petId}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Quantity')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.quantity}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<KeyboardDatePicker
                        inputProps={{
                            readOnly: true,
                        }}
                        disableToolbar
                        variant="inline"
                        format="MMM DD, yyyy"
                        margin="normal"
                        id="date-picker-inline"
                        label={t('UIStringResource_PetStore:ShipDate')}
                        value={props.item.shipDate}
                        onChange={e => { }}
                        readOnly={true}
                        TextFieldComponent={ReadOnlyTextField}
                    />
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Status')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.status}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Complete')}</InputLabel>
                    <StyledCheckbox checked={props.item.complete} disabled />
                </div>
                </CardContent>
                {/* <CardActions>
                    <Button size="small">Learn More</Button>
                </CardActions> */}
            </Card>
        );
    };

    return (
        <>
            {props.wrapperType === WrapperTypes.DialogForm &&
                <FormPopup
                    title={t('UIStringResource_PetStore:<+')}
                    openPopup={openPopup}
                    setOpenPopup={setOpenPopup}
                    submitDisabled={true}
                    handleSubmit={() => {}}
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


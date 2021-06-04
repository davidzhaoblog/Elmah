import React from 'react'
import { Card, CardContent, Grid } from '@material-ui/core';

import { InputLabel } from '@material-ui/core';
import { Typography } from '@material-ui/core';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { ReadOnlyTextField } from '../../controls/ReadOnlyTextField';
import { StyledCheckbox } from '../../controls/StyledCheckbox';

import { useTranslation } from 'react-i18next';

import { IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { useStyles } from 'src/features/formStyles';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import FormPopup from '../../FormPopup';

import { Order } from 'src/features/PetStore/Order';

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
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Id')}</InputLabel>
					<Typography>{props.item?.id}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:PetId')}</InputLabel>
					<Typography>{props.item?.petId}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Quantity')}</InputLabel>
					<Typography>{props.item?.quantity}</Typography>
                </Grid>
                <Grid item lg>
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
                        value={props.item?.shipDate}
                        onChange={e => { }}
                        readOnly={true}
                        TextFieldComponent={ReadOnlyTextField}
                    />
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Status')}</InputLabel>
					<Typography>{props.item?.status}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Complete')}</InputLabel>
					<StyledCheckbox checked={props.item?.complete} name="complete" disabled />
                </Grid>
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
                    title={t('UIStringResource_PetStore:Order')}
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


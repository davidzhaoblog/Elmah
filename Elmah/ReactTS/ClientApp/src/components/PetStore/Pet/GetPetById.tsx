import React from 'react'
import { Card, CardContent, Grid } from '@material-ui/core';

import { InputLabel } from '@material-ui/core';
import { Typography } from '@material-ui/core';

import { useTranslation } from 'react-i18next';

import { IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { useStyles } from 'src/features/formStyles';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import FormPopup from '../FormPopup';

import { Pet } from 'src/features/PetStore/Pet/Pet';

export default function Details(props: IFormProps<Pet> & IPopupProps) {
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
					<InputLabel shrink>{t('UIStringResource_PetStore:Name')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.name}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Status')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.status}</Typography>
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


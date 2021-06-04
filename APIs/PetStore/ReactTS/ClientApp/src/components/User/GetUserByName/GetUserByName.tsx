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

import { User } from 'src/features/PetStore/User/User';

export default function Details(props: IFormProps<User> & IPopupProps) {
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
					<InputLabel shrink>{t('UIStringResource_PetStore:Username')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.username}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:FirstName')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.firstName}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:LastName')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.lastName}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Email')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.email}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Password')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.password}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Phone')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.phone}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:UserStatus')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.userStatus}</Typography>
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


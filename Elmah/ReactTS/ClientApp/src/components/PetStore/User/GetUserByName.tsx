import React from 'react'
import { Card, CardContent, Grid } from '@material-ui/core';

import { InputLabel } from '@material-ui/core';
import { Typography } from '@material-ui/core';

import { useTranslation } from 'react-i18next';

import { IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { useStyles } from 'src/features/formStyles';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import FormPopup from '../../FormPopup';

import { User } from 'src/features/PetStore/User';

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
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Id')}</InputLabel>
					<Typography>{props.item?.id}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Username')}</InputLabel>
					<Typography>{props.item?.username}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:FirstName')}</InputLabel>
					<Typography>{props.item?.firstName}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:LastName')}</InputLabel>
					<Typography>{props.item?.lastName}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Email')}</InputLabel>
					<Typography>{props.item?.email}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Password')}</InputLabel>
					<Typography>{props.item?.password}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:Phone')}</InputLabel>
					<Typography>{props.item?.phone}</Typography>
                </Grid>
                <Grid item lg>
					<InputLabel shrink>{t('UIStringResource_PetStore:UserStatus')}</InputLabel>
					<Typography>{props.item?.userStatus}</Typography>
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
                    title={t('UIStringResource_PetStore:User')}
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


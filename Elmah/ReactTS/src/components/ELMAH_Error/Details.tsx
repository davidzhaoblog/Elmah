import React from 'react'
import { Card, CardContent, Grid, InputLabel, Typography } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { useStyles } from 'src/features/formStyles';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { ELMAH_Error } from 'src/features/ELMAH_Error/types';
import FormPopup from '../FormPopup';
import { StyledCheckbox } from '../controls/StyledCheckbox';

export default function Details(props: IFormProps<ELMAH_Error> & IPopupProps) {
    console.log(props);
    console.log(props.item);

    const classes = useStyles();
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

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
                        <InputLabel shrink>{t('UIStringResourcePerEntity:user')}</InputLabel>
                        <Typography>{props.item?.user}</Typography>
                    </Grid>
                    <Grid item lg>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:Host')}</InputLabel>
                        <Link to={{ pathname: '/elmah_error/details/4b090093-ffaa-4ee9-a891-83cb0a1019cc' }} >{t('UIStringResourcePerApp:ElmahApplication')}</Link>
                    </Grid>
                    <Grid item lg>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:testCheckBox')}</InputLabel>
                        <StyledCheckbox checked={props.item.testCheckBox} name="testCheckBox" disabled />
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
                    title={t('UIStringResourcePerApp:ELMAH_Error')}
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

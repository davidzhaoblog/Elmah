import React from 'react'
import { Card, CardContent, Grid, InputLabel } from '@material-ui/core';
import { Typography } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { useStyles } from 'src/features/formStyles';
import { createCloseButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import FormPopup from '../FormPopup';
import { ElmahStatusCode } from 'src/features/ElmahStatusCode/Types';

export default function Details(props: IFormProps<ElmahStatusCode> & IPopupProps) {
    // console.log(props);
    // console.log(props.item);

    const classes = useStyles();
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

    const { openPopup, setOpenPopup } = props;

    const closePopup = () => {
        setOpenPopup(false)
    }

    const popupButtonsOptions = createCloseButtonsOptions(t('UIStringResource:Close'), closePopup);

    const renderItem = () => {
        return (
            <Card className={classes.root} variant="outlined">
                <CardContent>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:StatusCode')}</InputLabel>
						<Typography>{props.item?.statusCode}</Typography>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:Name')}</InputLabel>
						<Typography>{props.item?.name}</Typography>
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
                    title={t('UIStringResourcePerApp:ElmahStatusCode')}
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



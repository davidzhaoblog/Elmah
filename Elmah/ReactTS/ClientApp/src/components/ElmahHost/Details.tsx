import React from 'react'
import { Card, CardContent, Grid, InputLabel } from '@material-ui/core';

import { useTranslation } from 'react-i18next';

import { IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { useStyles } from 'src/features/formStyles';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import FormPopup from '../FormPopup';
import { ElmahHost } from 'src/features/ElmahHost/Types';

export default function Details(props: IFormProps<ElmahHost> & IPopupProps) {
    // console.log(props);
    // console.log(props.item);

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
						<InputLabel shrink>{t('UIStringResourcePerEntity:Host')}</InputLabel>
						<Typography>{props.item.host}</Typography>
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
                    title={t('UIStringResourcePerApp:ElmahHost')}
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



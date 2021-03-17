import React from 'react'
import { Card, CardContent, Grid, InputLabel } from '@material-ui/core';
import { Link } from 'react-router-dom';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { ReadOnlyTextField } from '../controls/ReadOnlyTextField';
import { Typography } from '@material-ui/core';
import { useTranslation } from 'react-i18next';

import { IFormProps, WrapperTypes } from 'src/framework/ViewModels/IFormProps';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import { useStyles } from 'src/features/formStyles';
import { createEditFormButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import FormPopup from '../FormPopup';
import { ELMAH_Error } from 'src/features/ELMAH_Error/Types';

export default function Details(props: IFormProps<ELMAH_Error> & IPopupProps) {
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
						<InputLabel shrink>{t('UIStringResourcePerEntity:ErrorId')}</InputLabel>
						<Typography>{props.item.errorId}</Typography>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:ElmahApplication')}</InputLabel>
						<Link to={{ pathname: '/elmahapplication/details/' + props.item?.application}} >{props.item?.elmahApplication_Name}</Link>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:ElmahHost')}</InputLabel>
						<Link to={{ pathname: '/elmahhost/details/' + props.item?.host}} >{props.item?.elmahHost_Name}</Link>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:ElmahType')}</InputLabel>
						<Link to={{ pathname: '/elmahtype/details/' + props.item?.type}} >{props.item?.elmahType_Name}</Link>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:ElmahSource')}</InputLabel>
						<Link to={{ pathname: '/elmahsource/details/' + props.item?.source}} >{props.item?.elmahSource_Name}</Link>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:Message')}</InputLabel>
						<Typography>{props.item.message}</Typography>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:ElmahUser')}</InputLabel>
						<Link to={{ pathname: '/elmahuser/details/' + props.item?.user}} >{props.item?.elmahUser_Name}</Link>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:ElmahStatusCode')}</InputLabel>
						<Link to={{ pathname: '/elmahstatuscode/details/' + props.item?.statusCode}} >{props.item?.elmahStatusCode_Name}</Link>
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
							label={t('UIStringResourcePerEntity:TimeUtc')}
							value={props.item.timeUtc}
							onChange={e => { }}
							readOnly={true}
							TextFieldComponent={ReadOnlyTextField}
						/>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:Sequence')}</InputLabel>
						<Typography>{props.item.sequence}</Typography>
					</Grid>
					<Grid item lg>
						<InputLabel shrink>{t('UIStringResourcePerEntity:AllXml')}</InputLabel>
						<Typography>{props.item.allXml}</Typography>
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



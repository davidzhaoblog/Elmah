import * as React from 'react'
import { useDispatch } from 'react-redux';
import { Button, Typography, Accordion, AccordionSummary, Avatar, Divider, AccordionActions, AccordionDetails, InputLabel } from '@material-ui/core';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import clsx from 'clsx';
import { useTranslation } from 'react-i18next';

import { IListItemProps } from 'src/framework/ViewModels/IListItemProps';
import { FormTypes } from 'src/framework/ViewModels/IFormProps';
import { closeAlert, showAlert } from 'src/layout/appSlice';
import { createDeleteAlertButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { ELMAH_Error } from 'src/features/ELMAH_Error/types';
import { del } from 'src/features/ELMAH_Error/elmah_ErrorSlice';
import { StyledCheckbox } from '../controls/StyledCheckbox';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { ReadOnlyTextField } from '../controls/ReadOnlyTextField';
import { StyledTextField } from '../controls/StyledTextField';

export default function ListItem(props: IListItemProps<ELMAH_Error>) {
    const classes = props.classes;
    const dispatch = useDispatch();
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerEntity"]);

    const [expanded, setExpanded] = React.useState<string | false>(false);

    const handleChange = (panel: string) => (event: React.ChangeEvent<{}>, isExpanded: boolean) => {
        setExpanded(isExpanded ? panel : false);
    };

    // 2.1. Delete
    const handleDelete = () => {
        const confirmLDelete = () => {
            dispatch(del(props.item));
            dispatch(closeAlert());
        }
        const handleAlertClose = () => {
            dispatch(closeAlert());
        }

        const deleteAlertDialog = {
            title: t('UIStringResource:Delete'),
            message: 'You are deleting ' + props.item.user,
            buttons: createDeleteAlertButtonsOptions(confirmLDelete, handleAlertClose)
        };

        dispatch(showAlert(deleteAlertDialog));
    };

    return (
        <Accordion key={props.item.errorId.toString()} expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
            <AccordionSummary className={classes.summary} expandIcon={<ExpandMoreIcon />}>
                <Avatar className={classes.avatar} />
                <Typography className={classes.heading} variant="h1" component="h1">{props.item.errorId}</Typography>
                <Typography className={classes.secondaryHeading} variant="h2" component="h3">{props.item.user}</Typography>
            </AccordionSummary>
            <AccordionDetails>
                <div className={classes.column} >
                    <InputLabel shrink>{t('UIStringResourcePerEntity:user')}</InputLabel>
                    <Typography className={classes.labelData}>{props.item.user}</Typography>


                    <StyledTextField
                        label={t('UIStringResourcePerEntity:User')}
                        value={props.item.user}
                        variant='standard'
                        margin='normal'
                        fullWidth
                        autoFocus
                    />

                </div>
                <div className={classes.column}>
                    <KeyboardDatePicker
                        inputProps={{
                            readOnly: true,
                        }}
                        disableToolbar
                        variant="inline"
                        format="MMM DD, yyyy"
                        margin="normal"
                        id="date-picker-inline"
                        label={t('UIStringResourcePerEntity:timeUtc')}
                        value={props.item.timeUtc}
                        onChange={e => { }}
                        readOnly={true}
                        TextFieldComponent={ReadOnlyTextField}
                    />
                </div>
                <div className={clsx(classes.column)}>
                    <div>
                        <InputLabel shrink>{t('UIStringResourcePerEntity:user')}</InputLabel>
                        <StyledCheckbox checked={props.item.testCheckBox} name="testCheckBox" disabled />
                    </div>
                    <div>
                        <InputLabel shrink>{t('UIStringResourcePerApp:ElmahApplication')}</InputLabel>
                        <Link to={{ pathname: '/elmahapplication/details', state: {application: "aaaa"} }}>{t('UIStringResourcePerApp:ElmahApplication')}</Link>
                    </div>
                </div>
            </AccordionDetails>
            <Divider />
            <AccordionActions>
                <Button size="small" onClick={(e) => props.openFormInPopup(FormTypes.Edit, props.item)}>{t('UIStringResource:Edit')}</Button>
                <Button size="small" onClick={(e) => handleDelete()} color="primary">{t('UIStringResource:Delete')}</Button>
            </AccordionActions>
        </Accordion>
    );
}

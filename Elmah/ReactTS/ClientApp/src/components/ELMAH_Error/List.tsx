import * as React from 'react'

import { useDispatch } from 'react-redux';
import { Button, Accordion, AccordionSummary, Avatar, Divider, AccordionActions, AccordionDetails, InputLabel } from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { useTranslation } from 'react-i18next';
import clsx from 'clsx';

import { IListItemProps } from 'src/framework/ViewModels/IListItemProps';
import { FormTypes } from 'src/framework/ViewModels/IFormProps';
import { closeAlert, showAlert } from 'src/layout/appSlice';
import { createDeleteAlertButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { IListProps } from 'src/framework/ViewModels/IListProps';

import { Link } from 'react-router-dom';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { ReadOnlyTextField } from '../controls/ReadOnlyTextField';
import { Typography } from '@material-ui/core';

import { del } from 'src/features/ELMAH_Error/Slice';
import { ELMAH_Error } from 'src/features/ELMAH_Error/Types';

function ListItem(props: IListItemProps<ELMAH_Error>) {
    const classes = props.classes;
    const dispatch = useDispatch();
  const { t } = useTranslation(["UIStringResource", "UIStringResourcePerApp", "UIStringResourcePerEntity"]);

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
            message: 'You are deleting ' + props.item.errorId,
            buttons: createDeleteAlertButtonsOptions(confirmLDelete, handleAlertClose)
        };

        dispatch(showAlert(deleteAlertDialog));
    };

    return (
        <Accordion key={props.item.errorId.toString()} expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
            <AccordionSummary className={classes.summary} expandIcon={<ExpandMoreIcon />}>
                <Avatar className={classes.avatar} />
                <Typography className={classes.heading} variant="h1" component="h1">Take some data from AccordionDetails</Typography>
                <Typography className={classes.heading} variant="h1" component="h1">or Add descriptions</Typography>
            </AccordionSummary>
            <AccordionDetails>
                <div className={clsx(classes.column)}>
                    <InputLabel shrink>{t('UIStringResourcePerEntity:ErrorId')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.errorId}</Typography>
                    <InputLabel shrink>{t('UIStringResourcePerApp:ElmahApplication')}</InputLabel>
					<Link to={{ pathname: '/elmahapplication/details/' + props.item?.elmahApplication_Name}} >{props.item?.elmahApplication_Name}</Link>
                    <InputLabel shrink>{t('UIStringResourcePerApp:ElmahHost')}</InputLabel>
					<Link to={{ pathname: '/elmahhost/details/' + props.item?.elmahHost_Name}} >{props.item?.elmahHost_Name}</Link>
                    <InputLabel shrink>{t('UIStringResourcePerApp:ElmahType')}</InputLabel>
					<Link to={{ pathname: '/elmahtype/details/' + props.item?.elmahType_Name}} >{props.item?.elmahType_Name}</Link>
                    <InputLabel shrink>{t('UIStringResourcePerApp:ElmahSource')}</InputLabel>
					<Link to={{ pathname: '/elmahsource/details/' + props.item?.elmahSource_Name}} >{props.item?.elmahSource_Name}</Link>
                    <InputLabel shrink>{t('UIStringResourcePerEntity:Message')}</InputLabel>
					<Typography className={classes.labelData}>{props.item.message}</Typography>
                    <InputLabel shrink>{t('UIStringResourcePerApp:ElmahUser')}</InputLabel>
					<Link to={{ pathname: '/elmahuser/details/' + props.item?.elmahUser_Name}} >{props.item?.elmahUser_Name}</Link>
                    <InputLabel shrink>{t('UIStringResourcePerApp:ElmahStatusCode')}</InputLabel>
					<Link to={{ pathname: '/elmahstatuscode/details/' + props.item?.elmahStatusCode_Name}} >{props.item?.elmahStatusCode_Name}</Link>
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
                    <InputLabel shrink>{t('UIStringResourcePerEntity:Sequence')}</InputLabel>
					<Typography className={classes.labelData}>{props.item.sequence}</Typography>
                    <InputLabel shrink>{t('UIStringResourcePerEntity:AllXml')}</InputLabel>
					<Typography className={classes.labelData}>{props.item.allXml}</Typography>
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

export default function List(props: IListProps<ELMAH_Error>) {
    return (
        <div>
            {props.items.map((item: any) => {
                return (
                    <ListItem key={item.errorId} item={item} classes={props.classes} openFormInPopup={props.openFormInPopup} />
                );
            })}
        </div>
    );
}


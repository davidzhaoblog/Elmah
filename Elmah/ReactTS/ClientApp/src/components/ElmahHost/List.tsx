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

import { Typography } from '@material-ui/core';

import { del } from 'src/features/ElmahHost/Slice';
import { ElmahHost } from 'src/features/ElmahHost/Types';

function ListItem(props: IListItemProps<ElmahHost>) {
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
            message: 'You are deleting ' + props.item.host,
            buttons: createDeleteAlertButtonsOptions(confirmLDelete, handleAlertClose)
        };

        dispatch(showAlert(deleteAlertDialog));
    };

    return (
        <Accordion key={props.item.host.toString()} expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
            <AccordionSummary className={classes.summary} expandIcon={<ExpandMoreIcon />}>
                <Avatar className={classes.avatar} />
                <Typography className={classes.heading} variant="h1" component="h1">Take some data from AccordionDetails</Typography>
                <Typography className={classes.heading} variant="h1" component="h1">or Add descriptions</Typography>
            </AccordionSummary>
            <AccordionDetails>
                <div className ={clsx(classes.column)}>
<InputLabel shrink>{t('UIStringResourcePerEntity:Host')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.host}</Typography>
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

export default function List(props: IListProps<ElmahHost>) {
    return (
        <div>
            {props.items.map((item: any) => {
                return (
                    <ListItem key={item.host} item={item} classes={props.classes} openFormInPopup={props.openFormInPopup} />
                );
            })}
        </div>
    );
}

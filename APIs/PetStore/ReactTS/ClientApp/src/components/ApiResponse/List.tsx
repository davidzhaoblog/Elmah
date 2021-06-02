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

import { InputLabel } from '@material-ui/core';
import { Typography } from '@material-ui/core';

import { ApiResponse } from 'src/features//PetStore/ApiResponse';

function ListItem(props: IListItemProps<ApiResponse>) {
    const classes = props.classes;
    const dispatch = useDispatch();
	const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);

    const [expanded, setExpanded] = React.useState<string | false>(false);

    const handleChange = (panel: string) => (event: React.ChangeEvent<{}>, isExpanded: boolean) => {
        setExpanded(isExpanded ? panel : false);
    };




    return (
        <Accordion key={props.item.errorId.toString()} expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
            <AccordionSummary className={classes.summary} expandIcon={<ExpandMoreIcon />}>
                <Avatar className={classes.avatar} />
                <Typography className={classes.heading} variant="h1" component="h1">Take some data from AccordionDetails</Typography>
                <Typography className={classes.heading} variant="h1" component="h1">or Add descriptions</Typography>
            </AccordionSummary>
            <AccordionDetails>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Code')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.code}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Type')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.type}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Message')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.message}</Typography>
                </div>
            </AccordionDetails>
            <Divider />
            <AccordionActions>


            </AccordionActions>
        </Accordion>
    );
}

export default function List(props: IListProps<ApiResponse>) {
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


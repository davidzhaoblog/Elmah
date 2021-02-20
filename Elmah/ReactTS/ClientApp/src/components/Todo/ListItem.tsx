import * as React from 'react'
import { Button, Typography, Accordion, AccordionSummary, Avatar, Divider, AccordionActions } from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { IListItemProps } from 'src/framework/ViewModels/IListItemProps';
import { Todo } from 'src/features/Todo/types';
import { FormTypes } from 'src/framework/ViewModels/IFormProps';
import { useDispatch } from 'react-redux';
import { closeAlert, showAlert } from 'src/layout/appSlice';
import { createDeleteAlertButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { del } from 'src/features/Todo/todoSlice';

export default function ListItem(props: IListItemProps<Todo>) {
    const classes = props.classes;
    const dispatch = useDispatch();

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
            title: 'Delete',
            message: 'You are deleting ' + props.item.text,
            buttons: createDeleteAlertButtonsOptions(confirmLDelete, handleAlertClose)
        };

        dispatch(showAlert(deleteAlertDialog));
    };

    return (
        <Accordion key={props.item.id} expanded={true}>
            <AccordionSummary className={classes.summary} expandIcon={<ExpandMoreIcon />}>
                <Avatar className={classes.avatar} />
                <Typography className={classes.heading}>{props.item.id}</Typography>
                <Typography className={classes.secondaryHeading}>{props.item.text}</Typography>
            </AccordionSummary>
            {/* <AccordionDetails>
                <Typography>
                    {props.item.completed}
                </Typography>
            </AccordionDetails> */}
            <Divider />
            <AccordionActions>
                <Button size="small" onClick={(e) => props.openFormInPopup(FormTypes.Edit, props.item)}>Edit</Button>
                <Button size="small" onClick={(e) => handleDelete()} color="primary">Delete</Button>
            </AccordionActions>
        </Accordion>
    );
}

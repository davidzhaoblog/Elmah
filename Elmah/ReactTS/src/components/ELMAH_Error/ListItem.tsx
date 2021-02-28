import * as React from 'react'
import { Button, Typography, Accordion, AccordionSummary, Avatar, Divider, AccordionActions, AccordionDetails, TextField, TextFieldProps } from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { IListItemProps } from 'src/framework/ViewModels/IListItemProps';
import { FormTypes } from 'src/framework/ViewModels/IFormProps';
import { useDispatch } from 'react-redux';
import { closeAlert, showAlert } from 'src/layout/appSlice';
import { createDeleteAlertButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';
import { ELMAH_Error } from 'src/features/ELMAH_Error/types';
import { del } from 'src/features/ELMAH_Error/elmah_ErrorSlice';
import { StyledCheckbox } from '../controls/StyledCheckbox';
import { KeyboardDatePicker } from '@material-ui/pickers';
import clsx from 'clsx';

const TextFieldComponent = (props: TextFieldProps) => {
    return <TextField {...props}                      inputProps={{
        readOnly: true,
    }} />
  }

export default function ListItem(props: IListItemProps<ELMAH_Error>) {
    const classes = props.classes;
    const dispatch = useDispatch();
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
            title: 'Delete',
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
                <div className={classes.column} />
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
                        label="timeUtc"
                        value={props.item.timeUtc}
                        onChange={e => { }}
                        readOnly={true}
                        TextFieldComponent={TextFieldComponent}
                    />
                </div>
                <div className={clsx(classes.column, classes.helper)}>
                    <StyledCheckbox checked={props.item.testCheckBox} name="testCheckBox" disabled />
                </div>
            </AccordionDetails>
            <Divider />
            <AccordionActions>
                <Button size="small" onClick={(e) => props.openFormInPopup(FormTypes.Edit, props.item)}>Edit</Button>
                <Button size="small" onClick={(e) => handleDelete()} color="primary">Delete</Button>
            </AccordionActions>
        </Accordion>
    );
}

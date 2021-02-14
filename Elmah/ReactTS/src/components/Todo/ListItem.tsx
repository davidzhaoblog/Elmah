import * as React from 'react'
import { Button, Typography, Accordion, AccordionSummary, Avatar, Divider, AccordionActions } from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { IListItemProps } from 'src/framework/ViewModels/IListItemProps';

export default function ListItem(props: IListItemProps) {
    const classes = props.classes;

    return (
        <Accordion key={props.item.id} expanded={true}>
            <AccordionSummary className={classes.summary} expandIcon={<ExpandMoreIcon />}>
                <Avatar className={classes.avatar} src={props.item.avatar} />
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
                <Button size="small">Edit</Button>
                <Button size="small" color="primary">
                    Delete
                            </Button>
            </AccordionActions>
        </Accordion>
    );
}

import * as React from 'react';
import { Button, Accordion, AccordionSummary, Avatar, Divider, AccordionActions, AccordionDetails } from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { useTranslation } from 'react-i18next';
import clsx from 'clsx';

import { IListItemProps } from 'src/framework/ViewModels/IListItemProps';
import { FormTypes } from 'src/framework/ViewModels/IFormProps';
import { IListProps } from 'src/framework/ViewModels/IListProps';

import { useDispatch } from 'react-redux';
import { closeAlert, showAlert } from 'src/layout/appSlice';
import { createDeleteAlertButtonsOptions } from 'src/framework/ViewModels/IButtonOptions';

import { InputLabel } from '@material-ui/core';
import { Typography } from '@material-ui/core';

import { Pet } from 'src/features//PetStore/Pet';

import { deletePet } from 'src/features//PetStore/PetSlice';


function ListItem(props: IListItemProps<Pet>) {
    const classes = props.classes;
	const { t } = useTranslation(["UIStringResource", "UIStringResource_PetStore"]);
    const dispatch = useDispatch();

    const [expanded, setExpanded] = React.useState<string | false>(false);

    const handleChange = (panel: string) => (event: React.ChangeEvent<{}>, isExpanded: boolean) => {
        setExpanded(isExpanded ? panel : false);
    };


  // Delete.1 DeletePet -- /pet/{petId}
    const handleDeletePet = (item: Pet) => {
        const confirmDeletePet = () => {
            dispatch(deletePet({ petId: props.item.id, api_key: ''}));
            dispatch(closeAlert());
        }
        const handleAlertClose = () => {
            dispatch(closeAlert());
        }

        const deletePetAlertDialog = {
            title: t('UIStringResource_PetStore:DeletePet'),
            message: t('UIStringResource:Do_you_want_to_delete') + " " + props.item.id,
            buttons: createDeleteAlertButtonsOptions(t('UIStringResource:Delete'), confirmDeletePet, t('UIStringResource:Cancel'),handleAlertClose)
        };

        dispatch(showAlert(deletePetAlertDialog));
    };

    return (
        <Accordion key={props.item.id.toString()} expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
            <AccordionSummary className={classes.summary} expandIcon={<ExpandMoreIcon />}>
                <Avatar className={classes.avatar} />
                <Typography className={classes.heading} variant="h1" component="h1">Take some data from AccordionDetails</Typography>
                <Typography className={classes.heading} variant="h1" component="h1">or Add descriptions</Typography>
            </AccordionSummary>
            <AccordionDetails>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Id')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.id}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Name')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.name}</Typography>
                </div>
                <div className ={clsx(classes.column)}>
					<InputLabel shrink>{t('UIStringResource_PetStore:Status')}</InputLabel>
                    <Typography className={classes.heading} variant="h1" component="h1">{props.item.status}</Typography>
                </div>
            </AccordionDetails>
            <Divider />
            <AccordionActions>

                <Button size="small" onClick={(e) => handleDeletePet(props.item)} color="primary">{t('UIStringResource:Delete')}</Button>



                <Button size="small" onClick={(e) => props.openFormInPopup(FormTypes.Edit, props.item)}>{t('UIStringResource:Edit')}</Button>


                <Button size="small" onClick={(e) => props.openFormInPopup(FormTypes.Edit, props.item)}>{t('UIStringResource:Edit')}</Button>


                <Button size="small" onClick={(e) => props.openFormInPopup(FormTypes.Edit, props.item)}>{t('UIStringResource:Edit')}</Button>


            </AccordionActions>
        </Accordion>
    );
}

export default function FindPetsByStatus(props: IListProps<Pet>) {
    return (
        <div>
            {props.items.map((item: any) => {
                return (
                    <ListItem key={item.id} item={item} classes={props.classes} openFormInPopup={props.openFormInPopup} />
                );
            })}
        </div>
    );
}


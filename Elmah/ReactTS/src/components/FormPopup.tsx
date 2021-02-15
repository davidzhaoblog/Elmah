import React from 'react'
import { Button, Dialog, DialogTitle, DialogContent, makeStyles, Typography, DialogActions } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import ButtonList from './ButtonList';

export default function FormPopup(props: IPopupProps) {
    const { title, children, openPopup, setOpenPopup, buttons, handleSubmit } = props;
    const classes = useStyles();

    return (
        <Dialog open={openPopup} maxWidth="md" fullWidth={true} classes={{ paper: classes.dialogWrapper }}>
            <DialogTitle className={classes.dialogTitle}>
                <div style={{ display: 'flex' }}>
                    <Typography variant="h6" component="div" style={{ flexGrow: 1 }}>
                        {title}
                    </Typography>
                    <Button
                        onClick={() => { setOpenPopup(false) }}>
                        <CloseIcon />
                    </Button>
                </div>
            </DialogTitle>
            <form noValidate onSubmit={handleSubmit}>
                <DialogContent dividers>
                    {children}
                </DialogContent>
                <DialogActions>
                    <ButtonList buttons={buttons} submitDisabled={props.submitDisabled} />
                </DialogActions>
            </form>
        </Dialog>
    )
}

const useStyles = makeStyles(theme => ({
    form: {
        marginTop: theme.spacing(4),
        width: '100%',
    },
    dialogWrapper: {
        padding: theme.spacing(2),
        position: 'absolute',
        top: theme.spacing(5)
    },
    dialogTitle: {
        paddingRight: '0px'
    }
}))

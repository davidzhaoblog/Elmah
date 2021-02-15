import React from 'react'
import { Button, Dialog, DialogTitle, DialogContent, makeStyles, Typography, DialogActions, useTheme, useMediaQuery } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import { IPopupProps } from 'src/framework/ViewModels/IPopupProps';
import ButtonList from './ButtonList';

export default function Popup(props: IPopupProps) {

    const { title, children, openPopup, setOpenPopup, buttons } = props;
    const classes = useStyles();
    const theme = useTheme();
    const fullScreen = useMediaQuery(theme.breakpoints.down('sm'));
    
    return (
        <Dialog open={openPopup} maxWidth="lg" fullWidth={true} fullScreen={fullScreen} classes={{ paper: classes.dialogWrapper }}>
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
            <DialogContent dividers>
                {children}
            </DialogContent>
            <DialogActions>
                <ButtonList buttons={buttons} />
            </DialogActions>
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

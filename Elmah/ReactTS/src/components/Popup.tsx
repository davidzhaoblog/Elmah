import React from 'react'
import { Button, Dialog, DialogTitle, DialogContent, makeStyles, Typography, DialogActions } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';

interface IPopupProps {
    title: string;
    children: any;
    openPopup: any;
    setOpenPopup: (open: boolean) => void;
}

export default function Popup(props: IPopupProps) {

    const { title, children, openPopup, setOpenPopup } = props;
    const classes = useStyles();

    return (
        <Dialog open={openPopup} maxWidth="md" classes={{ paper: classes.dialogWrapper }}>
            <DialogTitle className={classes.dialogTitle}>
                <div style={{ display: 'flex' }}>
                    <Typography variant="h6" component="div" style={{ flexGrow: 1 }}>
                        {title}
                    </Typography>
                    <Button
                        onClick={()=>{setOpenPopup(false)}}>
                        <CloseIcon />
                    </Button>
                </div>
            </DialogTitle>
            <DialogContent dividers>
                {children}
            </DialogContent>
            <DialogActions>
                <Button color="primary">
                        Confirm
                </Button>
            </DialogActions>
        </Dialog>
    )
}

const useStyles = makeStyles(theme => ({
    dialogWrapper: {
        padding: theme.spacing(2),
        position: 'absolute',
        top: theme.spacing(5)
    },
    dialogTitle: {
        paddingRight: '0px'
    }
}))

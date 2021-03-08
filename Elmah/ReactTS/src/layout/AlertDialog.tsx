import * as React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { IAlert } from 'src/framework/ViewModels/IAlert';

export default function AlertDialog(props: IAlert): JSX.Element {
    return (
        <Dialog
            open={props !== null}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
        >
            <DialogTitle id="alert-dialog-title">{props.title}</DialogTitle>
            <DialogContent>
                <DialogContentText id="alert-dialog-description">
                    {props.message}
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                {props.buttons.map((button: any) => {
                    return (<Button onClick={button.handler} color={button.color} key={button.label}>
                        {button.label}
                    </Button>
                    );
                })}
            </DialogActions>
        </Dialog>
    );
}

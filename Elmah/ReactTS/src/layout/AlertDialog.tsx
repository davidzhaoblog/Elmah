import * as React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { useSelector } from 'react-redux';
import { RootState } from 'src/store/CombinedReducers';

export default function AlertDialog(): JSX.Element {
    const app = useSelector((state: RootState) => state.app);

    return (
        <Dialog
            open={app.alert !== null}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
        >
            <DialogTitle id="alert-dialog-title">{app.alert.title}</DialogTitle>
            <DialogContent>
                <DialogContentText id="alert-dialog-description">
                    {app.alert.message}
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                {app.alert.buttons.map((button: any) => {
                    return (<Button onClick={button.handler} color="primary" key={button.label}>
                        {button.label}
                    </Button>
                    );
                })}
            </DialogActions>
        </Dialog>
    );
}

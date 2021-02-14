import { Button } from "@material-ui/core";
import React from "react";
import { IButtonOptions } from "src/framework/ViewModels/IButtonOptions";


export default function DialogButtonList(props: { buttons: IButtonOptions[], submitDisabled?: boolean }): JSX.Element {
    const { submitDisabled } = props;

    return (
        <>
            {props.buttons.map((button: IButtonOptions) => {
                return (
                    <DialogButton button={button} key={button.label} submitDisabled={submitDisabled} />
                )
            })}
        </>
    );
}


export function DialogButton(props: { button: IButtonOptions, submitDisabled?: boolean }): JSX.Element {
    const { submitDisabled } = props;

    return (
        <>
            {props.button.type === 'submit'
                ? (<Button type='submit' color={props.button.color} disabled={submitDisabled}>
                    {props.button.label}
                </Button>)
                : (<Button onClick={props.button.handler} color={props.button.color}>
                    {props.button.label}
                </Button>)
            }
        </>
    );
}
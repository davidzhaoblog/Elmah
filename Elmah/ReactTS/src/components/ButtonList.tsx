import React from "react";
import { Button } from "@material-ui/core";
import { IButtonOptions } from "src/framework/ViewModels/IButtonOptions";


export default function ButtonList(props: { buttons: IButtonOptions[], submitDisabled?: boolean }): JSX.Element {
    const { submitDisabled } = props;

    return (
        <>
            {props.buttons.map((button: IButtonOptions) => {
                return <TypedButton key={button.label + button.type} button={button} submitDisabled={submitDisabled} />
            })}
        </>
    );
}

function TypedButton(props: { button: IButtonOptions, submitDisabled?: boolean }): JSX.Element {
    return (
        <>
            {(props.button.type === 'submit')
                ? (<Button type={props.button.type} color={props.button.color} disabled={props.submitDisabled}>
                    {props.button.label}
                </Button>)
                : (<Button key={props.button.label + props.button.type} type={props.button.type} onClick={props.button.handler} color={props.button.color}>
                    {props.button.label}
                </Button>)
            }
        </>
    )
}
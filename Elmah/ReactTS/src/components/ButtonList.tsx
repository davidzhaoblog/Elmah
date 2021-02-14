import { Button } from "@material-ui/core";
import React from "react";
import { IButtonOptions } from "src/framework/ViewModels/IButtonOptions";


export default function ButtonList(props: { buttons: IButtonOptions[], submitDisabled?: boolean }): JSX.Element {
    const { submitDisabled } = props;

    return (
        <>
            {props.buttons.map((button: IButtonOptions) => {
                return (
                    <>
                        {button.type === 'submit'
                            ? (<Button type='submit' color={button.color} disabled={submitDisabled} key={button.label}>
                                {button.label}
                            </Button>)
                            : (<Button onClick={button.handler} color={button.color} key={button.label}>
                                {button.label}
                            </Button>)
                        }
                    </>
                )
            })}
        </>
    );
}

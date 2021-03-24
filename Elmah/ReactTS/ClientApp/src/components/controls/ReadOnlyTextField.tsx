import TextField, { TextFieldProps } from "@material-ui/core/TextField/TextField"
import React from "react"

export const ReadOnlyTextField = (props: TextFieldProps) => {
    return <TextField {...props} inputProps={{
        readOnly: true,
    }} />
  }
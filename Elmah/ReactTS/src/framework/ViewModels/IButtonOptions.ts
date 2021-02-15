import { PropTypes } from "@material-ui/core";

export type ButtonType = JSX.IntrinsicElements['button']['type']

export interface IButtonOptions {
  label: string;
  color: PropTypes.Color;
  type?: ButtonType;
  handler?: () => void;
}

function createSubmitFormButtonsOptions(submitLabel: string, resetLabel: string, resetHandler: () => void, cancelLabel: string, cancelHandler: () => void): IButtonOptions[] {
  return [
    { label: submitLabel, color: 'primary', type: "submit" },
    { label: resetLabel, color: 'default', type: "button", handler: resetHandler },
    { label: cancelLabel, color: 'default', type: "button", handler: cancelHandler }];
}

export function createEditFormButtonsOptions(resetHandler: () => void, cancelhandler: () => void): IButtonOptions[] {
  return createSubmitFormButtonsOptions("Save", 'Reset', resetHandler, "Cancel", cancelhandler);
}

export function createCreateFormButtonsOptions(resetHandler: () => void, cancelhandler: () => void): IButtonOptions[] {
  return createSubmitFormButtonsOptions("Create", 'Reset', resetHandler, "Cancel", cancelhandler);
}

export function createLogoutAlertButtonsOptions(confirmHandler: () => void, cancelhandler: () => void): IButtonOptions[] {
  return [
    { label: "Log out", color: 'primary', handler: confirmHandler },
    { label: "Cancel", color: 'default', handler: cancelhandler }];  
}

export function createDeleteAlertButtonsOptions(confirmHandler: () => void, cancelhandler: () => void): IButtonOptions[] {
  return [
    { label: "Delete", color: 'primary', handler: confirmHandler },
    { label: "Cancel", color: 'default', handler: cancelhandler }];  
}

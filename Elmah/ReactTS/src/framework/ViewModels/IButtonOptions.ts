import { PropTypes } from "@material-ui/core";

export interface IButtonOptions {
  label: string;
  color: PropTypes.Color;
  type?: string;
  handler?: () => void;
}

function createSubmitFormButtonsOptions(submitLabel: string, cancelLabel: string, cancelhandler: () => void): IButtonOptions[] {
  return [
    { label: submitLabel, color: 'primary', type: 'submit' },
    { label: cancelLabel, color: 'default', handler: cancelhandler }];
}

export function createEditFormButtonsOptions(cancelhandler: () => void): IButtonOptions[] {
  return createSubmitFormButtonsOptions("Save", "Cancel", cancelhandler);
}

export function createCreateFormButtonsOptions(cancelhandler: () => void): IButtonOptions[] {
  return createSubmitFormButtonsOptions("Create", "Cancel", cancelhandler);
}

export function createLogoutAlertButtonsOptions(confirmHandler: () => void, cancelhandler: () => void): IButtonOptions[] {
  return [
    { label: "Log out", color: 'primary', handler: confirmHandler },
    { label: "Cancel", color: 'default', handler: cancelhandler }];  
}

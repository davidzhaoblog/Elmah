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

export function createEditFormButtonsOptions(saveLabel: string, resetLabel: string, resetHandler: () => void, cancelLabel: string, cancelhandler: () => void): IButtonOptions[] {
  return createSubmitFormButtonsOptions(saveLabel, resetLabel, resetHandler, cancelLabel, cancelhandler);
}
// return createSubmitFormButtonsOptions(t('UIStringResource:Save'), t('UIStringResource:Refresh'), resetHandler, t('UIStringResource:Cancel'), cancelhandler);

export function createCreateFormButtonsOptions(createLabel: string, resetLabel: string, resetHandler: () => void, canelLabel: string, cancelhandler: () => void): IButtonOptions[] {
  return createSubmitFormButtonsOptions(createLabel, resetLabel, resetHandler, canelLabel, cancelhandler);
}
// return createSubmitFormButtonsOptions(t('UIStringResource:Create'), t('UIStringResource:Refresh'), resetHandler, t('UIStringResource:Cancel'), cancelhandler);

export function createLogoutAlertButtonsOptions(logoutLabel: string, confirmHandler: () => void, cancelLabel: string, cancelhandler: () => void): IButtonOptions[] {
  return [
    { label: logoutLabel, color: 'primary', handler: confirmHandler },
    { label: cancelLabel, color: 'default', handler: cancelhandler }];  
}
// { label: t('UIStringResource:Account_LogInStatus_LogoutText'), color: 'primary', handler: confirmHandler },
// { label: t('UIStringResource:Cancel'), color: 'default', handler: cancelhandler }];  

export function createDeleteAlertButtonsOptions(deleteLabel: string, confirmHandler: () => void, cancelLabel: string, cancelhandler: () => void): IButtonOptions[] {
  return [
    { label: deleteLabel, color: 'primary', handler: confirmHandler },
    { label: cancelLabel, color: 'default', handler: cancelhandler }];  
}
// { label: t('UIStringResource:Delete'), color: 'primary', handler: confirmHandler },
// { label: t('UIStringResource:Cancel'), color: 'default', handler: cancelhandler }];  

export function createSearchFormButtonsOptions(searchLabel: string, resetLabel: string, resetHandler: () => void, cancelLabel: string, cancelhandler: () => void): IButtonOptions[] {
  return createSubmitFormButtonsOptions(searchLabel, resetLabel, resetHandler, cancelLabel, cancelhandler);
}
//  return createSubmitFormButtonsOptions(t('UIStringResource:Search'), t('UIStringResource:Refresh'), resetHandler, t('UIStringResource:Cancel'), cancelhandler);

export function createCloseButtonsOptions(cancelLabel: string, cancelhandler: () => void): IButtonOptions[] {
  return [
    { label: cancelLabel, color: 'default', handler: cancelhandler }];  
}
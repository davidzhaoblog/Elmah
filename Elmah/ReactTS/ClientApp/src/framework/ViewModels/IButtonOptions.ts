import { PropTypes } from "@material-ui/core";
import { useTranslation } from 'react-i18next';

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
  const { t } = useTranslation(["UIStringResource"]);
  return createSubmitFormButtonsOptions(t('UIStringResource:Save'), t('UIStringResource:Refresh'), resetHandler, t('UIStringResource:Cancel'), cancelhandler);
}

export function createCreateFormButtonsOptions(resetHandler: () => void, cancelhandler: () => void): IButtonOptions[] {
  const { t } = useTranslation(["UIStringResource"]);
  return createSubmitFormButtonsOptions(t('UIStringResource:Create'), t('UIStringResource:Refresh'), resetHandler, t('UIStringResource:Cancel'), cancelhandler);
}

export function createLogoutAlertButtonsOptions(confirmHandler: () => void, cancelhandler: () => void): IButtonOptions[] {
  const { t } = useTranslation(["UIStringResource"]);
  return [
    { label: t('UIStringResource:Account_LogInStatus_LogoutText'), color: 'primary', handler: confirmHandler },
    { label: t('UIStringResource:Cancel'), color: 'default', handler: cancelhandler }];  
}

export function createDeleteAlertButtonsOptions(confirmHandler: () => void, cancelhandler: () => void): IButtonOptions[] {
  const { t } = useTranslation(["UIStringResource"]);
  return [
    { label: t('UIStringResource:Delete'), color: 'primary', handler: confirmHandler },
    { label: t('UIStringResource:Cancel'), color: 'default', handler: cancelhandler }];  
}

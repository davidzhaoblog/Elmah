import { IButtonOptions } from "./IButtonOptions";

export interface IPopupProps {
  title?: string;
  children?: any;
  openPopup: any;
  setOpenPopup: (open: boolean) => void;
  submitDisabled?: boolean;
  handleSubmit?: (e: any) => void;
  buttons?: IButtonOptions[];
}
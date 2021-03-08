import { IButtonOptions } from "./IButtonOptions";

export interface IAlert {
  title?: string;
  message: string;
  buttons?: IButtonOptions[];
}
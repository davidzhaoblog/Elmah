import { IAlert } from "./IAlert";

export interface IAppState {
  drawerOpen: boolean;
  loading: boolean;
  alert: IAlert;
}

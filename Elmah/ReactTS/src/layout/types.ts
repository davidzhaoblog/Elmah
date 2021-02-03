export interface IAppState {
    drawerOpen: boolean;
    loading: boolean;
    alert: IAlert;
}

interface IAlertButtonOptions {
    label: string;
    handler: () => void;
}

export interface IAlert {
    title?: string;
    message: string;
    buttons?: IAlertButtonOptions[];
}
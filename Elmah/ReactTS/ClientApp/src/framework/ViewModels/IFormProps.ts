export interface IFormProps<TItem>
{
  classes?: any;
  wrapperType: WrapperTypes;
  type: FormTypes;
  item?: TItem;
}

export enum FormTypes {
  Create,
  Edit,
  View,
}

export enum WrapperTypes {
  RegularPage,
  FormPage,
  RegularDialog,
  DialogForm,
}
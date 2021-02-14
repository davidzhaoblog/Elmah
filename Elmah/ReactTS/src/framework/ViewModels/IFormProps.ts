export interface IFormProps<TItem>
{
  classes?: any;
  type: FormTypes;
  item?: TItem;
}

export enum FormTypes {
  Create,
  Edit,
  View,
}
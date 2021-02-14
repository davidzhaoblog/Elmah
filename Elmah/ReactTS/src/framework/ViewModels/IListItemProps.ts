import { FormTypes } from 'src/framework/ViewModels/IFormProps';

export interface IListItemProps<TItem> {
  item: TItem;
  classes?: any;
  openFormInPopup: (type: FormTypes, item: TItem) => void;
}

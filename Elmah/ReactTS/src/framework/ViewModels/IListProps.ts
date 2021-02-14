import { FormTypes } from 'src/framework/ViewModels/IFormProps';

export interface IListProps<TItem>
{
  classes?: any;
  items: TItem[];
  openFormInPopup: (type: FormTypes, item: TItem) => void;
}

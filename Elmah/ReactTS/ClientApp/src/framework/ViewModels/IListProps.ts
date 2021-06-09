export interface IListProps<TItem>
{
  classes?: any;
  items: TItem[];
  openFormInPopup: (type: string, item: TItem) => void;
}

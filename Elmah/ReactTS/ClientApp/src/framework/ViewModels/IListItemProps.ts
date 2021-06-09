export interface IListItemProps<TItem> {
  item: TItem;
  classes?: any;
  openFormInPopup: (type: string, item: TItem) => void;
}

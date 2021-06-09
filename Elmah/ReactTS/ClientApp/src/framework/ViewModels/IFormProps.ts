import { QueryOrderBySetting } from "../Queries/QueryOrderBySetting";
import { QueryPagingSetting } from "../Queries/QueryPagingSetting";

export interface IFormProps<TItem>
{
  classes?: any;
  wrapperType: WrapperTypes;
  type: string;
  item?: TItem;
}

export interface ISearchFormProps<TCriteria>
{
  classes?: any;
  wrapperType: WrapperTypes;
  type: string;
  criteria?: TCriteria;
  orderBy : QueryOrderBySetting;
  queryPagingSetting: QueryPagingSetting;
}

export enum FormTypes {
  Create = "Create",
  Edit = "Edit",
  View = "View",
  Search = "Search",
}

export enum WrapperTypes {
  RegularPage,
  FormPage,
  RegularDialog,
  DialogForm,
}
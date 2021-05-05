import { QueryOrderBySetting } from "../Queries/QueryOrderBySetting";
import { QueryPagingSetting } from "../Queries/QueryPagingSetting";

export interface IFormProps<TItem>
{
  classes?: any;
  wrapperType: WrapperTypes;
  type: FormTypes;
  item?: TItem;
}

export interface ISearchFormProps<TCriteria>
{
  classes?: any;
  wrapperType: WrapperTypes;
  type: FormTypes;
  criteria?: TCriteria;
  orderBy : QueryOrderBySetting;
  queryPagingSetting: QueryPagingSetting;
}

export enum FormTypes {
  Create,
  Edit,
  View,
  Search,
}

export enum WrapperTypes {
  RegularPage,
  FormPage,
  RegularDialog,
  DialogForm,
}
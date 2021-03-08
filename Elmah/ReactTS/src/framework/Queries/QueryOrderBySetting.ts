// import { Dictionary } from '../Dictionary'
import { QueryOrderDirections } from './QueryOrderDirections'
export interface QueryOrderBySetting
{
  propertyName: string;
  direction: QueryOrderDirections;
  displayName: string;
  expression: string;
  // fontIcon: string;
  // fontIconFamily: string;
  // isSelected: boolean;
  // clientSideActions: any;
  // errors: Dictionary<string[]>;
  // hasErrors: boolean;
}

export interface QueryUnitContains<T>
{
  isToCompare: boolean;
  valueToBeContained: T;
}

export interface QueryUnitEquals<T>
{
  isToCompare: boolean;
  valueToCompare: T;
}

export interface QueryUnitRange<T>
{
  isToCompare: boolean;
  isToCompareLowerBound: boolean;
  isToIncludeLowerBound: boolean;
  lowerBound: T;
  isToCompareUpperBound: boolean;
  isToIncludeUpperBound: boolean;
  upperBound: T;
}

export interface QueryUnitSelectedList<T>
{
  isToCompare: boolean;
  selectedList: T[];
}

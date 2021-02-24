import { QueryUnitContains, QueryUnitEquals, QueryUnitRange, QueryUnitSelectedList } from "./QueryUnits";

export const convertQueryUnitContains = <T>(input: T): QueryUnitContains<T> => {
  return {
    isToCompare: Boolean(input),
    valueToBeContained: input,
  };
}

export const convertQueryUnitEquals = <T>(input: T): QueryUnitEquals<T> => {
  return {
    isToCompare: Boolean(input),
    valueToCompare: input,
  };
}

export const convertQueryUnitRange = <T>(lower: T, upper: T): QueryUnitRange<T> => {
  return {
    isToCompare: Boolean(lower) && Boolean(upper),
    isToCompareLowerBound: Boolean(lower),
    isToIncludeLowerBound: true,
    lowerBound: lower,
    isToCompareUpperBound: Boolean(upper),
    isToIncludeUpperBound: false,
    upperBound: upper,
  };
}

export const convertQueryUnitSelectedList = <T>(input: T[]): QueryUnitSelectedList<T> => {
  return {
    isToCompare: Boolean(input),
    selectedList: input,
  };
}


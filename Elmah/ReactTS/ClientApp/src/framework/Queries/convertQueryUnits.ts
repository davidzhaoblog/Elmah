import { Range } from "../Models/Range";
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

export const convertQueryUnitRange = <T>(input: Range<T>): QueryUnitRange<T> => {
  return {
    isToCompare: Boolean(input.lower) && Boolean(input.upper),
    isToCompareLowerBound: Boolean(input.lower),
    isToIncludeLowerBound: true,
    lowerBound: input.lower,
    isToCompareUpperBound: Boolean(input.upper),
    isToIncludeUpperBound: false,
    upperBound: input.upper,
  };
}

export const convertQueryUnitSelectedList = <T>(input: T[]): QueryUnitSelectedList<T> => {
  return {
    isToCompare: Boolean(input),
    selectedList: input,
  };
}


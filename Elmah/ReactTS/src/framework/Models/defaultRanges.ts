import { Range } from './Range'

export const defaultBooleanRange = () : Range<boolean> =>
{
  return {lower: false, upper: true};
}

export const defaultDateRange = () : Range<string> =>
{
  var yesterday = new Date();
  yesterday.setDate(yesterday.getDate() - 1);
  return {lower: yesterday.toString(), upper: (new Date()).toString()};
}

export const defaultNumberRange = () : Range<number> =>
{
  return {lower: 0, upper: 1};
}

export const defaultStringRange = () : Range<string> =>
{
  return {lower: 'a', upper: 'z'};
}

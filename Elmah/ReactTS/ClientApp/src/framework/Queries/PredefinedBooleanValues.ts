export enum PredefinedBooleanValues
{
    All = 'All',
    True = 'True',
    False = 'False',
}

export function getPredefinedBooleanValues(): any[] {
    const keys = Object.values(PredefinedBooleanValues); 
    return keys;
}
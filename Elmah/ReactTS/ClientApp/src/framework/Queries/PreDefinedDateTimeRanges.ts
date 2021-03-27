import { Range } from 'src/framework/Models/Range'

export enum PreDefinedDateTimeRanges
{
    Unknown = "Unknown",
    Custom = "Custom",
    LastTenYears = "LastTenYears",
    LastFiveYears = "LastFiveYears",
    LastYear = "LastYear",
    LastSixMonths = "LastSixMonths",
    LastThreeMonths = "LastThreeMonths",
    LastMonth = "LastMonth",
    LastWeek = "LastWeek",
    Yesterday = "Yesterday",
    // LastHour = "LastHour",
    ThisYear = "ThisYear",
    ThisMonth = "ThisMonth",
    ThisWeek = "ThisWeek",
    Today = "Today",
    // ThisHour = "",
    // NextHour = "",
    Tomorrow = "Tomorrow",
    NextWeek = "NextWeek",
    NextMonth = "NextMonth",
    NextThreeMonths = "NextThreeMonths",
    NextSixMonths = "NextSixMonths",
    NextYear = "NextYear",
    NextFiveYears = "NextFiveYears",
    NextTenYears = "NextTenYears",
}

export function getPreDefinedDateTimeRanges(): any[] {
    const keys = Object.values(PreDefinedDateTimeRanges); 
    return keys;
}


export function convertToDateTimeRange(input: PreDefinedDateTimeRanges): Range<string> {

    let lower = new Date();
    let upper = new Date();

    if(input == PreDefinedDateTimeRanges.Custom) {
    }
    else if(input == PreDefinedDateTimeRanges.Unknown) {
        lower = null;
        upper = null;
    }
    else {
        if(input == PreDefinedDateTimeRanges.LastFiveYears){
            lower.setFullYear(lower.getFullYear() - 5);
        }
        else if(input == PreDefinedDateTimeRanges.LastMonth){
            lower.setMonth(lower.getMonth() - 1);
        }
        else if(input == PreDefinedDateTimeRanges.LastSixMonths){
            lower.setMonth(lower.getMonth() - 6);
        }
        else if(input == PreDefinedDateTimeRanges.LastTenYears){
            lower.setFullYear(lower.getFullYear() - 10);
        }
        else if(input == PreDefinedDateTimeRanges.LastThreeMonths){
            lower.setMonth(lower.getMonth() - 3);
        }
        else if(input == PreDefinedDateTimeRanges.LastWeek){
            lower.setDate(lower.getFullYear() - 7);
        }
        else if(input == PreDefinedDateTimeRanges.LastYear){
            lower.setFullYear(lower.getFullYear() - 1);
        }
        else if(input == PreDefinedDateTimeRanges.NextFiveYears){
            upper.setFullYear(lower.getFullYear() + 5);
        }
        else if(input == PreDefinedDateTimeRanges.NextMonth){
            upper.setMonth(lower.getMonth() + 1);
        }
        else if(input == PreDefinedDateTimeRanges.NextSixMonths){
            upper.setMonth(lower.getMonth() + 6);
        }
        else if(input == PreDefinedDateTimeRanges.NextTenYears){
            upper.setFullYear(lower.getFullYear() + 10);
        }
        else if(input == PreDefinedDateTimeRanges.NextThreeMonths){
            upper.setMonth(lower.getMonth() + 3);
        }
        else if(input == PreDefinedDateTimeRanges.NextWeek){
            upper.setDate(lower.getDate() + 7);
        }
        else if(input == PreDefinedDateTimeRanges.NextYear){
            upper.setFullYear(lower.getFullYear() + 1);
        }
        else if(input == PreDefinedDateTimeRanges.ThisMonth){
            lower.setDate(1);
            upper.setMonth(upper.getMonth() + 1);
            upper.setDate(1);
        }
        // TODO: how to get last week, this week and next week in typescript/angular 7 Date().
        // else if(input == PreDefinedDateTimeRanges.ThisWeek){
        //     let datePipe = new DatePipe("en-US");
        //     var weekDay = datePipe.

        // }
        else if(input == PreDefinedDateTimeRanges.ThisYear){
            lower.setMonth(1);
            lower.setDate(1);
            lower.setHours(0);
            lower.setMinutes(0);
            lower.setSeconds(0);
            lower.setMilliseconds(0);
            upper.setFullYear(lower.getFullYear() + 1);
            upper.setMonth(1);
            upper.setDate(1);
            upper.setHours(0);
            upper.setMinutes(0);
            upper.setSeconds(0);
            upper.setMilliseconds(0);
        }
        else if(input == PreDefinedDateTimeRanges.Today){
            lower.setHours(0);
            lower.setMinutes(0);
            lower.setSeconds(0);
            lower.setMilliseconds(0);
            upper.setDate(lower.getDate() + 1);
            upper.setHours(0);
            upper.setMinutes(0);
            upper.setSeconds(0);
            upper.setMilliseconds(0);
        }
        else if(input == PreDefinedDateTimeRanges.Tomorrow){
            lower.setDate(lower.getDate() + 1);
            lower.setHours(0);
            lower.setMinutes(0);
            lower.setSeconds(0);
            lower.setMilliseconds(0);
            upper.setDate(lower.getDate() + 2);
            upper.setHours(0);
            upper.setMinutes(0);
            upper.setSeconds(0);
            upper.setMilliseconds(0);
        }
        else if(input == PreDefinedDateTimeRanges.Yesterday){
            lower.setDate(lower.getDate() - 1);
            lower.setHours(0);
            lower.setMinutes(0);
            lower.setSeconds(0);
            lower.setMilliseconds(0);
            upper.setHours(0);
            upper.setMinutes(0);
            upper.setSeconds(0);
            upper.setMilliseconds(0);
        }
    }

    return {lower: lower?.toISOString(), upper: upper?.toISOString()};

}

// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getDateRange(referenceDate, type) {
    var lowerBound = moment(referenceDate);
    var upperBound = moment(referenceDate);
    switch (type) {
        case "LastTenYears":
            return { "LowerBound": lowerBound.subtract(10, "years").startOf("years"), "UpperBound": upperBound };
            break;
        case "LastFiveYears":
            return { "LowerBound": lowerBound.subtract(5, "years").startOf("years"), "UpperBound": upperBound };
            break;
        case "LastYear":
            return { "LowerBound": lowerBound.subtract(1, "years").startOf("years"), "UpperBound": upperBound };
            break;
        case "LastSixMonths":
            return { "LowerBound": lowerBound.subtract(6, "months").startOf("months"), "UpperBound": upperBound };
            break;
        case "LastThreeMonths":
            return { "LowerBound": lowerBound.subtract(3, "months").startOf("months"), "UpperBound": upperBound };
            break;
        case "LastMonth":
            return { "LowerBound": lowerBound.subtract(1, "months").startOf("months"), "UpperBound": upperBound };
            break;
        case "LastWeek":
            return { "LowerBound": lowerBound.subtract(1, "weeks").startOf("weeks"), "UpperBound": upperBound };
            break;
        case "Yesterday":
            return { "LowerBound": lowerBound.subtract(1, "days").startOf("days"), "UpperBound": upperBound };
            break;
        case "ThisYear":
            return { "LowerBound": lowerBound.startOf("years"), "UpperBound": upperBound.endOf("years") };
            break;
        case "ThisMonth":
            return { "LowerBound": lowerBound.startOf("months"), "UpperBound": upperBound.endOf("months") };
            break;
        case "ThisWeek":
            return { "LowerBound": lowerBound.startOf("weeks"), "UpperBound": upperBound.endOf("weeks") };
            break;
        case "Today":
            return { "LowerBound": lowerBound.startOf("days"), "UpperBound": upperBound.endOf("days") };
            break;
        case "Tomorrow":
            return { "LowerBound": lowerBound.add(1, "days").startOf("days"), "UpperBound": upperBound.add(1, "days").endOf("days") };
            break;
        case "NextWeek":
            return { "LowerBound": lowerBound.add(1, "weeks").startOf("weeks"), "UpperBound": upperBound.add(1, "weeks").endOf("weeks") };
            break;
        case "NextMonth":
            return { "LowerBound": lowerBound.add(1, "months").startOf("months"), "UpperBound": upperBound.add(1, "months").endOf("months") };
            break;
        case "NextThreeMonths":
            return { "LowerBound": lowerBound, "UpperBound": upperBound.add(3, "months").endOf("months") };
            break;
        case "NextSixMonths":
            return { "LowerBound": lowerBound, "UpperBound": upperBound.add(6, "months").endOf("months") };
            break;
        case "NextYear":
            return { "LowerBound": lowerBound, "UpperBound": upperBound.add(1, "years").endOf("years") };
            break;
        case "NextFiveYears":
            return { "LowerBound": lowerBound, "UpperBound": upperBound.add(5, "years").endOf("years") };
            break;
        case "NextTenYears":
            return { "LowerBound": lowerBound, "UpperBound": upperBound.add(10, "years").endOf("years") };
            break;
        case "Custom":
        case "Unknown":
            return { "LowerBound": lowerBound.subtract(10, "years").startOf("years"), "UpperBound": upperBound };
            break;
    }
}
function preDefinedDateTimeRangeChanged(self, masterName) {
    var thisInput = $(self);
    var inputLowerBound = $('input[name="' + masterName + 'Lower"]');
    var inputUpperBound = $('input[name="' + masterName + 'Upper"]');
    if ($(thisInput).val() == 'Custom') {
        inputLowerBound[0].removeAttribute('readonly');
        inputUpperBound[0].removeAttribute('readonly');
    }
    else {
        inputLowerBound[0].setAttribute("readonly", "readonly");
        inputUpperBound[0].setAttribute("readonly", "readonly");

        var range = getDateRange(moment(), $(thisInput).val());
        inputLowerBound.val(range.LowerBound.format('YYYY-MM-DDThh:mm'));
        inputUpperBound.val(range.UpperBound.format('YYYY-MM-DDThh:mm'));
    }
}

const datePickerOptions = {
    autoclose: false,
    showClose: true,
    beforeShowDay: $.noop,
    calendarWeeks: false,
    clearBtn: true,
    daysOfWeekDisabled: [],
    endDate: Infinity,
    forceParse: true,
    format: 'mm/dd/yyyy',
    keyboardNavigation: true,
    language: 'en',
    minViewMode: 0,
    orientation: 'auto',
    rtl: false,
    startDate: -Infinity,
    startView: 2,
    todayBtn: true,
    todayHighlight: false,
    weekStart: 0,
    enableOnReadonly: false
};

function pageIndexChanged(pageIndex) {
    $("#pageIndexH").val(pageIndex);
    $("thisform").submit();
}

$(document).ready($(function () {
    $("#pageSize").change(function (e) {
        $("#pageSizeH").val(e.target.value);
        $("thisform").submit();
    });
    $("#orderBy").change(function (e) {
        $("#orderByH").val(e.target.value);
        $("thisform").submit();
    });
}));

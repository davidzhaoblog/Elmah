// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// 1.Start CascadingDropdown
$(".ddlcascading").change(function (e) {
    const childlisturl = $(this).data("childlisturl");
    const targetchild = $(this).data("targetchild");

    let query = {
        pageIndex: 1, pageSize: 10000 // get all.
    };

    var sameTargetChilds = $('select[data-targetchild="' + targetchild + '"]');
    console.log(sameTargetChilds);
    $.each(sameTargetChilds, function (index, item) {
        const selectedValue = $(item).find(":selected").val();
        const queryparamname = $(item).data("queryparamname");
        query[queryparamname] = selectedValue;
        //console.log(queryparamname);
        //console.log(selectedValue);
    });
    $.ajax({
        type: "GET",
        url: childlisturl,
        data: query,
        async: false,
        contentType: "application/json",
        success: function (response) {
            if (response.status == 200 && !!response.responseBody) {
                console.log($('select[name="' + targetchild + '"]'));
                let firstOption = $('select[name="' + targetchild + '"] option:first-child').attr("selected", 'selected')
                console.log(firstOption);
                $('select[name="' + targetchild + '"]').empty();
                $('select[name="' + targetchild + '"]').append(firstOption);
                $.each(response.responseBody, function (index, item) {
                    $('select[name="' + targetchild + '"]').append('<option value="' + item.value + '">' + item.name + '</option>');
                });
            }
            // console.log(response);
        },
        failure: function (response) {
            // console.log(response);
        },
        error: function (response) {
            // console.log(response);
        }
    });

    //console.log(childlisturl);
    //console.log(targetchild);
});
// 1.End CascadingDropdown

// 2.Start Predefined DateTime Range Changed
$(".datetime-predefinedrange").change(function (e) {
    preDefinedDateTimeRangeChanged($(e.target), $(e.target).attr("name"));
})
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
        if (!!range.LowerBound) {
            inputLowerBound.val(range.LowerBound.format('YYYY-MM-DDThh:mm'));
        }
        else {
            inputLowerBound.val(null);
        }
        if (!!range.UpperBound) {
            inputUpperBound.val(range.UpperBound.format('YYYY-MM-DDThh:mm'));
        }
        else {
            inputUpperBound.val(null);
        }
    }
}
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
        case "AllTime":
            return { "LowerBound": null, "UpperBound": null };
            break;
    }
}
// 2.End Predefined DateTime Range Changed

// 3.Start Pagination and OrderBy
$(document).ready($(function () {
    $(".page-link").click(function (e) {
        $($(this).closest(".list-container-submit").data("updatetarget")).val($(this).data("updatetarget"));
        $($(this).closest(".list-container-submit").data("submittarget")).submit();
    });
}));

$(document).ready($(function () {
    $(".selectchange-submit").change(function (e) {
        $($(this).data("updatetarget")).val(e.target.value);
        $($(this).data("submittarget")).submit();
    });
}));
// 3.End Pagination and OrderBy

// 4.Start. Clear all select/input inside .advanced-search-field when .advanced-search-button button clicked
$(document).ready($(function () {
    $('.advanced-search-button').click(function (e) {
        $('.advanced-search-field div div select').val('')
        $('.advanced-search-field div div input').val('')
    });
}));
// 4.End. Clear all select/input inside .advanced-search-field when .advanced-search-button button clicked

// 5.start form ajax-submit: POST .ajax-partial-load-post
$(document).ready($(function () {
    $('.ajax-partial-load-post-formdata').submit(function (e) {
        e.preventDefault();
        const url = $(this).data("partial-url");
        const updateTarget = $(this).data("updatetarget");
        var formData = new FormData($(this)[0]);

        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            async: false,
            processData: false,
            contentType: false,
            dataType: "html",
            success: function (response) {
                $(updateTarget).html(response);
                // console.log(response);
            },
            failure: function (response) {
                // console.log(response);
            },
            error: function (response) {
                // console.log(response);
            }
        });
    });

    // requires complicated json conversion. not suggested to use it.
    //$('.ajax-partial-load-post-json').submit(function (e) {
    //    e.preventDefault();
    //    const url = $(this).data("partial-url");
    //    const updateTarget = $(this).data("updatetarget");
    //    var data = $(this)
    //        //.filter(function (index, element) {
    //        //    console.log($(element).val());
    //        //    return !!$(element).val();
    //        //})
    //        .serializeArray();
    //    var jsonData = {};
    //    $.map(data, function (n, i) {
    //        // jsonData[n['name'].replace("Query.", "query.")] = n['value'];
    //        jsonData["query." + lowerCaseWords(n['name'].replace("Query.", ""))] = n['value'];
    //    });

    //    console.log(jsonData);
    //    console.log(data);

    //    $.ajax({
    //        type: "POST",
    //        url: url,
    //        data: JSON.stringify(jsonData),
    //        async: false,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "html",
    //        success: function (response) {
    //            $(updateTarget).html(response);
    //            // console.log(response);
    //        },
    //        failure: function (response) {
    //            // console.log(response);
    //        },
    //        error: function (response) {
    //            // console.log(response);
    //        }
    //    });
    //});

    $('.ajax-partial-load-get').submit(function (e) {
        e.preventDefault();
        const url = $(this).data("partial-url");
        const updateTarget = $(this).data("updatetarget");
        var data = $(this)
            //.filter(function (index, element) {
            //    console.log($(element).val());
            //    return !!$(element).val();
            //})
            .serialize();

        console.log(data);
        $.ajax({
            type: "GET",
            url: url,
            data: data,
            async: false,
            dataType: "html",
            success: function (response) {
                $(updateTarget).html(response);
                // console.log(response);
            },
            failure: function (response) {
                // console.log(response);
            },
            error: function (response) {
                // console.log(response);
            }
        });
    });
}));
// 5.end form ajax-submit

// 6.Start. PagedViewOptions clicked
$(document).ready($(function () {
    $('.radio-pagedviewoption-submit').click(function (e) {
        $($(this).data("updatetarget")).val($(this).data("value"));
        $($(this).data("submittarget")).submit();
    });
}));
// 6.End. PagedViewOptions clicked


//function lowerCaseWords(str) {
//    return (str + '').replace(/^([A-Z])|\s+([A-Z])/g, function ($1) {
//        return $1.toLowerCase();
//    });
//}
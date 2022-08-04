// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// 1.Start CascadingDropdown
/* data-targetchild
 * data-nt-queryparamname
 * 
 * .nt-ddlcascading
 */
$(".nt-ddlcascading").change(function (e) {
    const childlisturl = $(this).data("childlisturl");
    const targetchild = $(this).data("targetchild");

    let query = {
        pageIndex: 1, pageSize: 10000 // get all.
    };

    var sameTargetChilds = $('select[data-targetchild="' + targetchild + '"]');
    // console.log(sameTargetChilds);
    $.each(sameTargetChilds, function (index, item) {
        const selectedValue = $(item).find(":selected").val();
        const queryparamname = $(item).data("nt-queryparamname");
        query[queryparamname] = selectedValue;
        //console.log(queryparamname);
        //console.log(selectedValue);
    });
    $.ajax({
        type: "GET",
        url: childlisturl,
        data: query,
        async: false,
        dataType: "html",
        success: function (response) {
            if (response.status == 200 && !!response.responseBody) {
                // console.log($('select[name="' + targetchild + '"]'));
                let firstOption = $('select[name="' + targetchild + '"] option:first-child').attr("selected", 'selected')
                // console.log(firstOption);
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
/* 
 * data-nt-targetchild
 *
 * .nt-ddlcascading
 * .nt-datetime-predefinedrange
 */
$(".nt-datetime-predefinedrange").change(function (e) {
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
/* 
 * data-nt-submittarget
 *
 * .nt-page-size-submit
 * .nt-order-by-submit
 * .nt-page-size
 * .nt-order-by
 * 
 */
$(document).ready($(function () {
    $(".nt-page-size-submit").change(function (e) {
        $($(this).data("nt-submittarget")).find(".nt-page-size").val(e.target.value);
        $($(this).data("nt-submittarget")).submit();
    });
}));
$(document).ready($(function () {
    $(".nt-order-by-submit").change(function (e) {
        $($(this).data("nt-submittarget")).find(".nt-order-by").val(e.target.value);
        $($(this).data("nt-submittarget")).submit();
    });
}));
// 3.End Pagination and OrderBy

// 4.Start. Clear all select/input inside .nt-advanced-search-field when .nt-advanced-search-button button clicked
/*
 * .nt-advanced-search-button
 * .nt-advanced-search-field
 */
$(document).ready($(function () {
    $('.nt-advanced-search-button').click(function (e) {
        $('.nt-advanced-search-field div div select').val('')
        $('.nt-advanced-search-field div div input').val('')
    });
}));
// 4.End. Clear all select/input inside .advanced-search-field when .advanced-search-button button clicked

// 5.start form ajax-submit: POST .ajax-partial-load-post
/* 
 * data-nt-partial-url
 * data-nt-updatetarget
 * data-nt-pageindex
 * 
 * .nt-ajax-partial-load-post-formdata
 * .nt-ajax-partial-load-get
 * .nt-list-container-submit
 * .nt-page-index
 * .nt-paged-view-options
 * .btn-nt-load-more
 *
 */

function pageLinkClicked(self) {
    const theForm = $($(self).closest(".nt-list-wrapper").data("nt-submittarget"));
    $(theForm).find(".nt-page-index").val($(self).data("nt-pageindex"));
    const url = $(theForm).data("nt-partial-url");
    const updateTarget = $(theForm).data("nt-updatetarget");
    var formData = new FormData($(theForm)[0]);
    const pagedViewOption = $(theForm).children(".nt-paged-view-options").val();
    const pageIndex = $(theForm).children(".nt-page-index").val();
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            const toAppend = $(response);
            if (pagedViewOption !== "Tiles" || pageIndex == 1) {
                $(updateTarget).html(toAppend);
            }
            else {
                $(updateTarget).children(".btn-nt-load-more").remove()
                $(updateTarget).append(toAppend);
            }
            attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
            attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
            //console.log("success", response);
        },
        failure: function (response) {
            // console.log("failure", response);
        },
        error: function (response) {
            // console.log("error", response);
        }
    });
    return false;
}

$(document).ready($(function () {
    $('.nt-ajax-partial-load-post-formdata').submit(function (e) {
        const url = $(this).data("nt-partial-url");
        const updateTarget = $(this).data("nt-updatetarget");
        var formData = new FormData($(this)[0]);
        const pagedViewOption = $(this).children(".nt-paged-view-options").val();
        const pageIndex = $(this).children(".nt-page-index").val();
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            async: false,
            processData: false,
            contentType: false,
            dataType: "html",
            success: function (response) {
                const toAppend = $(response);
                if (pagedViewOption !== "Tiles" || pageIndex == 1) {
                    $(updateTarget).html(toAppend);
                }
                else {
                    $(updateTarget).children(".btn-nt-load-more").remove()
                    $(updateTarget).append(toAppend);
                }
                attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
                attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
            },
            failure: function (response) {
                // console.log("failure", response);
            },
            error: function (response) {
                // console.log("error", response);
            }
        });
    });

    $('.nt-ajax-partial-load-get').submit(function (e) {
        const url = $(this).data("nt-partial-url");
        const updateTarget = $(this).data("nt-updatetarget");
        var data = $(this)
            //.filter(function (index, element) {
            //    console.log($(element).val());
            //    return !!$(element).val();
            //})
            .serialize();
        const pagedViewOption = $(this).children(".nt-paged-view-options").val();
        const pageIndex = $(this).children(".nt-page-index").val();
        //console.log(data);
        $.ajax({
            type: "GET",
            url: url,
            data: data,
            async: false,
            dataType: "html",
            success: function (response) {
                const toAppend = $(response);
                if (pagedViewOption !== "Tiles" || pageIndex == 1) {
                    $(updateTarget).html(toAppend);
                }
                else {
                    $(updateTarget).children(".btn-nt-load-more").remove()
                    $(updateTarget).append(toAppend);
                }
                attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
                attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
            },
            failure: function (response) {
                // console.log("failure", response);
            },
            error: function (response) {
                // console.log("error", response);
            }
        });
        e.preventDefault();
    });
}));
// 5.end form ajax-submit

// 6.Start. PagedViewOptions clicked
/*
 * data-nt-submittarget - e.g. #thisForm
 * data-nt-paginationoptionupdatevalue - e.g. // List-Pagination, Tiles-More, Slideshow-NoPagination
 * data-nt-value - PagedViewOption - List/Tile/Slideshow/EditableList
 * 
 * .nt-radio-pagedviewoption
 * 
 * nt-pagination-option-field
 * nt-paged-view-option-field
 * 
 * nt-submittarget
 */
$(document).ready($(function () {
    $('.nt-radio-pagedviewoption').click(function (e) {
        const submitTarget = $(this).data("nt-submittarget");
        const pagedViewOption = $(this).data("nt-value");
        if (pagedViewOption === "EditableList") {
            $(this).closest(".nt-list-wrapper").find(".nt-editable-list-buttons").show();
        }
        else {
            $(this).closest(".nt-list-wrapper").find(".nt-editable-list-buttons").hide();
        }
        // 1. update pagination-option
        $(submitTarget).find(".nt-pagination-option-field").val($(this).data("nt-paginationoptionupdatevalue")); // List-Pagination, Tiles-More, Slideshow-NoPagination
        // 2. update paged-view-option
        $(submitTarget).find(".nt-paged-view-option-field").val(pagedViewOption); // List-Pagination, Tiles-More, Slideshow-NoPagination
        $($(this).data("nt-submittarget")).submit();
        // console.log($(location));
        // Update QueryString - view with data-nt-value attribute
        let queryParams = new URLSearchParams(window.location.search);
        queryParams.set("PagedViewOption", $(this).data("nt-value"));
        history.pushState(null, null, "?" + queryParams.toString());

        $(".btn-nt-inline-editing").off();
        attachInlineEditingLaunchButtonClickEvent(".btn-nt-inline-editing");
    });
}));
// 6.End. PagedViewOptions clicked

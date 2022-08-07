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
            $(this).closest(".nt-list-wrapper").find(".nt-multiitem-editing-buttons").show();
            $(submitTarget).find(".nt-template-field").val("Edit");
        }
        else {
            $(this).closest(".nt-list-wrapper").find(".nt-multiitem-editing-buttons").hide();
            $(submitTarget).find(".nt-template-field").val("Details");
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

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
//$(document).ready($(function () {
//    showHidePagedViewOptionsRelatedButtons("#thisform");
//    $('.nt-radio-pagedviewoption').click(function (e) {
//        pageViewOptionsClickedEventHandler(this);
//    });
//}));


function pageViewOptionsClickedEventHandler(sourceButton) {
    const submitTarget = $(sourceButton).data("nt-submittarget");
    const pagedViewOption = $(sourceButton).data("nt-value");
    if (pagedViewOption === "EditableList") {
        $(sourceButton).closest(".nt-list-wrapper").find(".nt-multiitem-editing-buttons").show();
        $(submitTarget).find(".nt-template-field").val("Edit");
    }
    else {
        $(sourceButton).closest(".nt-list-wrapper").find(".nt-multiitem-editing-buttons").hide();
        $(submitTarget).find(".nt-template-field").val("Details");
    }
    //var fullScreenLoadingModal = bootstrap.Modal.getOrCreateInstance(document.getElementById('fullScreenLoading'));
    //fullScreenLoadingModal.show();
    // 1. update pagination-option
    $(submitTarget).find(".nt-pagination-option-field").val($(sourceButton).data("nt-paginationoptionupdatevalue")); // List-Pagination, Tiles-More, Slideshow-NoPagination

    // 2. update paged-view-option
    $(submitTarget).find(".nt-paged-view-option-field").val(pagedViewOption); // List-Pagination, Tiles-More, Slideshow-NoPagination
    //$($(sourceButton).data("nt-submittarget")).submit();
    indexSearchSubmit($($(sourceButton).data("nt-submittarget")));
    // console.log($(location));
    // Update QueryString - view with data-nt-value attribute
    let queryParams = new URLSearchParams(window.location.search);
    queryParams.set("PagedViewOption", $(sourceButton).data("nt-value"));
    history.pushState(null, null, "?" + queryParams.toString());

    $(".btn-nt-inline-editing").off();
    attachInlineEditingLaunchButtonClickEvent(".btn-nt-inline-editing");
}

function showHidePagedViewOptionsRelatedButtons(wrapperSelector) {
    const thePagedViewOptionField = $(wrapperSelector).find(".nt-paged-view-option-field");
    const currentView = $(thePagedViewOptionField).val();
    const listWrapper = $($(thePagedViewOptionField).closest(".nt-ajax-partial-load-post-formdata").data("nt-updatetarget")).
        closest(".nt-list-wrapper");
    const editableListButtons = $(listWrapper).find(".nt-multiitem-editing-buttons");
    const listBulkSelectFilter = $(listWrapper).find(".nt-bulk-select-filter");
    const listBulkActionContainer = $(listWrapper).find(".nt-bulk-actions-container");
    if (currentView == "EditableList") {
        // 1. show EditableList Buttons
        editableListButtons.show();
        listBulkSelectFilter.hide();
        listBulkActionContainer.hide();
    }
    else {
        editableListButtons.hide();
        listBulkSelectFilter.show();
    }
}

// 6.End. PagedViewOptions clicked

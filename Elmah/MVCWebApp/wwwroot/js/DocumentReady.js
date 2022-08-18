﻿/* this file contains all functions for different views of $(document).ready($(function() { ... }
 */

function documentReadyForSearchList(searchFormId, listWrapperId) {
    showHidePagedViewOptionsRelatedButtons("#" + searchFormId);
    attachFormDataChanged($("#" + listWrapperId + " form"));
    attachMultiItemsDeleteCheckboxEvent($("#" + listWrapperId + " form"));
    attachRefreshButtonClickEvent($("#" + listWrapperId + " .btn-nt-fresh"));
    attachAjaxLoadItemsSubmit("#" + searchFormId);
}


function attachRefreshButtonClickEvent(selector) {
    $(selector).click(function (e) {
        let button = e.currentTarget;
        const submitTarget = $(button).closest(".nt-list-wrapper").data("nt-submittarget");
        ajaxLoadItemsSubmit(theForm)
    });
}
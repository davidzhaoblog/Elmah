// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 * data-nt-filtername
 * data-nt-filtervalue
 * data-nt-checkbox-checked
 * data-nt-filter-{filterName}="{filterValue}"
 * 
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * 
 * 1.3. data-nt- in .nt-listitem
 * 
 * 1.4. data-nt- in trigger button .nt-btn-action-save or .nt-btn-action-delete
 * 
 * 2. css classes
 * 2.1. only in this modal
 * .nt-batchselect-filter
 * .btn-nt-batchselect-status
 * .nt-list-batchselect
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper
 * .nt-listitem
 * 
 * 2.3. bootstrap 5 css classses
 * .form-check-input :check
 * .bg-info
 * 
 * 2.4. in ajax response html
 * 
 * 3. Html tags
 * table tbody
 */

$(document).ready(function () {
    attachBatchSelectStatusClickEventHandler(".nt-batchselect-filter .btn-nt-batchselect-status");
    attachIndividualSelectCheckboxClickEventHandler(".nt-list-wrapper table tbody .nt-listitem .nt-list-batchselect .form-check-input");
    attachQuickSelectClickEventHandler(".nt-batchselect-filter .nt-quickselect");
});

function attachBatchSelectStatusClickEventHandler(selector) {
    $(selector).click(function (e) {
        const currentStatus = $(e.currentTarget).data("nt-batchselect-status");
        const allCheckBoxes = $(e.currentTarget).closest("table").find("tbody .nt-listitem .nt-list-batchselect .form-check-input");
        if (currentStatus !== "None") {// Some or All 
            $(allCheckBoxes).prop("checked", false); // uncheck all
            $(e.currentTarget).data("nt-batchselect-status", "None")
            $(e.currentTarget).html('<i class="fa-regular fa-square"></i>');
        }
        else {
            $(allCheckBoxes).prop("checked", true); // check all
            $(e.currentTarget).data("nt-batchselect-status", "All")
            $(e.currentTarget).html('<i class="fa-regular fa-square-check"></i>');
        }
        toggleListItemBackground($(e.currentTarget).closest("table"));
    })
}

function attachIndividualSelectCheckboxClickEventHandler(selector) {
    $(selector).click(function (e) {
        const checked = $(e.currentTarget).closest("tbody").find(".nt-listitem .nt-list-batchselect .form-check-input:checked");
        const notChecked = $(e.currentTarget).closest("tbody").find(".nt-listitem .nt-list-batchselect .form-check-input:not(:checked)");
        const batchSelectStatus = $(e.currentTarget).closest("table").find(".nt-batchselect-filter .btn-nt-batchselect-status");
        if (checked.length === 0 && notChecked.length !== 0) {
            batchSelectStatus.data("nt-batchselect-status", "None")
            batchSelectStatus.html('<i class="fa-regular fa-square"></i>');
        }
        else if (checked.length !== 0 && notChecked.length === 0) {
            batchSelectStatus.data("nt-batchselect-status", "All")
            batchSelectStatus.html('<i class="fa-regular fa-square-check"></i>');
        }
        else {
            batchSelectStatus.data("nt-batchselect-status", "Some")
            batchSelectStatus.html('<i class="fa-regular fa-square-minus"></i>');
        }
        toggleListItemBackground($(e.currentTarget).closest("table"));
    })
}

function attachQuickSelectClickEventHandler(selector) {
    $(selector).click(function (e) {
        const filterName = $(e.currentTarget).data("nt-filtername");
        const filterValue = $(e.currentTarget).data("nt-filtervalue");
        const setCheckedTo = $(e.currentTarget).data("nt-checkbox-checked");
        $(e.currentTarget).closest("table").find("tbody .nt-listitem td[data-nt-filter-" + filterName + "='" + filterValue + "']").closest(".nt-listitem").find(".nt-list-batchselect .form-check-input").prop("checked", setCheckedTo);
        toggleListItemBackground($(e.currentTarget).closest("table"));
    })
}
function toggleListItemBackground(tableSelector) {
    $(tableSelector).find("tbody .nt-listitem .nt-list-batchselect .form-check-input:checked").closest(".nt-listitem").addClass("bg-info");
    $(tableSelector).find("tbody .nt-listitem .nt-list-batchselect .form-check-input:not(:checked)").closest(".nt-listitem").removeClass("bg-info");
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 *
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * data-nt-route-id-def
 * data-nt-batchdelete-url
 * 
 * 1.3. data-nt- in .nt-listitem
 * 
 * 1.4. data-nt- in trigger button .nt-btn-action-save or .nt-btn-action-delete
 * 
 * 2. css classes
 * 2.1. only in this modal
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper
 * .nt-listitem
 * .nt-list-batchselect
 * .nt-batch-action-delete
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

function batchDelete(sourceButton) {
    // composite id when data-nt-route-id-def is null, ids is a string array, only one property name(in identifier query) is allowed in data-nt-route-id-def
    const routeIdDef = $(sourceButton).closest(".nt-list-wrapper").data("nt-route-id-def");
    const routeIds = $(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-batchselect .form-check-input:checked").closest(".nt-listitem").map((i, x) => $(x).data("nt-route-id"));
    let formData = new FormData();
    $.each(routeIds, function (index, l) {
        const formDataPropertyName = "ids[" + index + "]" + (!!routeIdDef ? "." + routeIdDef : "");
        formData.append(formDataPropertyName, l);
    });
    const postbackurl = $(sourceButton).closest(".nt-list-wrapper").data("nt-batchdelete-url");
    $.ajax({
        type: "POST",
        url: postbackurl,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            $(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-batchselect .form-check-input:checked").closest(".nt-listitem").remove();
            console.log(response);
        },
        failure: function (response) {
            // console.log(response);
            $(sourceButton).removeAttr("disabled");
        },
        error: function (response) {
            // console.log(response);
            $(sourceButton).removeAttr("disabled");
        }
    });
}

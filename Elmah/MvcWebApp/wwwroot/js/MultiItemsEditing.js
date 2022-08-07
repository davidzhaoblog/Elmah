// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 *
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * 
 * 1.3. data-nt- in .nt-listitem
 * 
 * 1.4. data-nt- in trigger button .nt-btn-action-save or .nt-btn-action-delete
 * 
 * 2. css classes
 * 2.1. only in this modal
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * 
 * 2.3. in ajax response html
 * 
 */

$(document).ready(function () {
    attachMultiItemsSubmitButtonClickEvent(".btn-nt-multiitems-editing-submit");
    attachMultiItemsCancelButtonClickEvent(".btn-nt-multiitems-editing-cancel");
})

function attachMultiItemsSubmitButtonClickEvent(selector) {
    $(selector).click(function (e) {
        let button = e.currentTarget;
        const currentListItem = $(button).closest(".nt-listitem");
        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");

        const multiitemsSubmitUrl = $(wrapper).data("nt-multiitems-submit-url");

        const form = $(wrapper).find("form");
        let formData = [];
        if (!!form) {
            formData = new FormData($(form)[0]);
        }

        $.ajax({
            type: "POST",
            url: multiitemsSubmitUrl,
            data: formData,
            async: false,
            processData: false,
            contentType: false,
            dataType: "html",
            success: function (response) {
                console.log(response);
            //    //$(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem .nt-list-bulk-select .form-check-input:checked").closest(".nt-listitem").remove();
            //    const splitResponse = response.split("===---------===");
            //    // response part #1, status html
            //    if (splitResponse.length > 0) {
            //        $(dialog).find(".modal-body").append(splitResponse[0]);
            //    }
            //    const actionSuccess = !!($(dialog).find(".text-success").length);
            //    if (actionSuccess) {
            //        if (splitResponse.length > 1) {
            //            for (i = 1; i < splitResponse.length; i++) {
            //                const responseRoutId = $(splitResponse[i]).data("nt-route-id");
            //                $(sourceButton).closest(".nt-list-wrapper").find(".nt-listitem[data-nt-route-id='" + responseRoutId + "']").html($(splitResponse[i]).html())
            //            }
            //        }
            //        attachIndividualSelectCheckboxClickEventHandler($(wrapper).find(".nt-listitem .nt-list-bulk-select .form-check-input:checked"));
            //        attachInlineEditingLaunchButtonClickEvent($(wrapper).find(".nt-listitem .nt-list-bulk-select .form-check-input:checked").closest(".nt-listitem").find(".btn-nt-inline-editing"));
            //        const modal = bootstrap.Modal.getInstance(dialog);
            //        modal.hide();
            //        showSingletonMessagePopup(splitResponse[0]);
            //    }
            },
            failure: function (response) {
                // console.log(response);
                // $(sourceButton).removeAttr("disabled");
            },
            error: function (response) {
                // console.log(response);
                // $(sourceButton).removeAttr("disabled");
            }
        });
    });
}

function attachMultiItemsCancelButtonClickEvent(selector) {
    $(selector).click(function (e) {
        let button = e.currentTarget;
        const currentListItem = $(button).closest(".nt-listitem");
        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-option-field").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");

        let routeid = ""
        if (action == "POST") { // Create
            routeid = $(button).data("nt-route-id");
        }
        else {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
        }
        const loadItemUrl = $(wrapper).data("nt-loaditem-url");

        initializeInlineEditing(button, action)
        // 3. Ajax to get htmls
        ajaxLoadItemInlineEditing(loadItemUrl + "/" + routeid, currentListItem, view, container, template, action);
    });
}

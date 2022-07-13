// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * single dialog instance with id="crudActionDialog"
 * #crudActionDialog
 *
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 * data-nt-action
 * data-nt-view // from $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-options").val();
 * data-nt-container // from $(button)
 * data-nt-template // from $(button)
 * data-nt-postbackurl // calculated
 *
 * 1.2. data-nt- in $(wrapper) .nt-list-wrapper
 * data-nt-loaditem-url
 * data-nt-createitem-url
 * data-nt-updateitem-url
 * data-nt-deleteitem-url
 * 
 * 1.3. data-nt- in .nt-listitem
 * data-nt-route-id
 * 
 * 1.4. data-nt- in trigger button .nt-btn-action-save or .nt-btn-action-delete
 * data-nt-action
 * data-nt-container
 * data-nt-template
 * 
 * 2. css classes
 * 2.1. only in this modal
 * .nt-status
 * .nt-modal-body // not in inline-editing
 * .btn-nt-pagination-previous // not in inline-editing
 * .btn-nt-pagination-next // not in inline-editing
 * .btn-nt-action
 * .btn-group-nt-action-pagination // not in inline-editing
 * .btn-group-nt-action-createanotherone // not in inline-editing
 * .btn-group-nt-action-save 
 * .btn-group-nt-action-delete // not in inline-editing??
 * .btn-group-nt-action-details
 * .nt-created, with .border-warning .border-3
 * .nt-updated, with .border-success .border-3
 * .nt-deleted, with .border-danger .border-3 // not in inline-editing
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper, shared with other .js files
 * .nt-list-container-submit, shared with other .js files
 * .nt-listitem
 * .nt-current // with on .nt-list-wrapper and .nt-list-container-submit, .nt-listitem(with .border-info .border-5)
 * .nt-paged-view-options: // read value for data-nt-view in this modal
 * 
 * 2.3. in ajax response html
 * .nt-hidden-modal-title, server side render modal title to a hidden field, will set to .modal-title // not in inline-editing
 */


$(document).ready(function () {
    $(".btn-nt-inline-editing").click(function (e) {
        let button = event.currentTarget;
        const currentListItem = $(button).closest(".nt-listitem");
        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-options").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");
        //let postbackurl = "";
        let routeid = ""
        if (action == "POST") { // Create
            routeid = $(button).data("nt-route-id");
        }
        else {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
        }
        const loadItemUrl = $(wrapper).data("nt-loaditem-url");

        initializeModal(button, action)
        // 3. Ajax to get htmls
        ajaxLoadItem(loadItemUrl + "/" + routeid, currentListItem, view, container, template, action);
    });


});

function attachFunctionButtonClickEvent() {
    $(".nt-listitem.nt-current .btn-nt-action").click(function (e) {
        const self = this;
        $(this).attr("disabled", true);
        let button = event.currentTarget;
        const currentListItem = $(button).closest(".nt-listitem");
        const wrapper = $(button).closest(".nt-list-wrapper");
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-options").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");
        // Get FormData if there are in this dialog
        const form = $(".nt-listitem.nt-current form")
        let formData = [];
        if (!!form) {
            formData = new FormData($(form)[0]);
        }
        formData.append("view", view);
        formData.append("container", container);
        formData.append("template", template);
        form.validate();
        let postbackurl = "";
        let routeid = ""
        if (action == "PUT") { // Edit
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-updateitem-url");
        }
        else if (action == "POST") { // Create
            routeid = $(button).data("nt-route-id");
            postbackurl = $(wrapper).data("nt-createitem-url");
        }
        else if (action == "DELETE") {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-deleteitem-url");
        }
        else {
            routeid = $(button).closest(".nt-listitem").data("nt-route-id");
        }
        const loadItemUrl = $(wrapper).data("nt-loaditem-url");

        ajaxPostback(postbackurl + "/" + routeid, formData, self, view, loadItemUrl + "/" + routeid, container, template);
    });
}


function ajaxLoadItem(loadItemUrl, currentListItem, view, container, template, action) {
    $.ajax({
        type: "GET",
        url: loadItemUrl,
        data: { view, container, template },
        async: false,
        contentType: "application/json",
        success: function (response) {
            // 3.1. add response html to .nt-modal-body
            if (action == "PUT" || action == "POST") // wrap with <form>..</form> Edit or Create
            {
                currentListItem.html(response);
            }
            else { // DELETE, <form>...</form> wrapped around .nt-btn-action-delete
                currentListItem.html(response);
            }
            attachFunctionButtonClickEvent();
            // console.log(response);
        },
        failure: function (response) {
            // console.log(response);
        },
        error: function (response) {
        }
    });
}

function ajaxPostback(postbackurl, formData, self, view, loadItemUrl, container, template) {
    $.ajax({
        type: "POST",
        url: postbackurl,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            // console.log(response);
            const splitResponse = response.split("===---------===");
            // response part #1, status html / how to display status message?

            // response part #1.1 TODO: should have a timer to auto hide after a few seconds.
            // response part #2, to update the item which is updated/created.
            const action = $(self).data("nt-action");
            const actionSuccess = false;// !!$("#crudActionDialog .nt-status .text-success");
            if (action === "PUT") { // EDIT
                // When EDIT, insert a new tr after ".nt-listitem.nt-current", then remove it after 3 seconds.
                if (splitResponse.length > 0) {
                    $(self).popover({
                        container: 'body',
                        html: true,
                        content: splitResponse[0]
                    });
                    setTimeout(() => {
                        $(self).popover('hide') }, 1500)
                    // $("<tr><td colspan='100'>" + splitResponse[0] + "</td></tr>").insertAfter($(".nt-listitem.nt-current"));
                }
                if (actionSuccess) {
                    // Mark as .nt-updated .border-success .border-4. will be deleted when Dialog/Modal closed
                    $(".nt-listitem.nt-current").removeClass("border-info border-5");
                    $(".nt-listitem.nt-current").addClass("nt-updated border-success border-4");
                }
                if (splitResponse.length > 1) {
                    $(".nt-listitem.nt-current").html(splitResponse[1]);
                }
                $(self).removeAttr("disabled");
            }
            else if (action === "POST") { // Create
                // .nt-created .border-warning .border-4 added in the response
                if (splitResponse.length > 1) {
                    if (view === "List") { // html table
                        let theTbody = $($("#crudActionDialog").data("nt-list-wrapper-id") + " tbody");
                        theTbody.prepend(splitResponse[1]);
                    }
                }
            }
            else if (action === "DELETE") {
                if (actionSuccess) {
                    // Mark as .nt-deleted .border-danger .border-5. will be deleted when Dialog/Modal closed
                    $(".nt-listitem.nt-current").addClass("nt-deleted border-danger border-3");
                }
            }
        },
        failure: function (response) {
            // console.log(response);
            $(self).removeAttr("disabled");
        },
        error: function (response) {
            // console.log(response);
            $(self).removeAttr("disabled");
        }
    });
}

function initializeModal(button, action) {
    // 1.1. clear .nt-current on all .nt-listitem, then set .nt-current to current item,
    $(".nt-listitem").removeClass("nt-current");
    // 1.2. set .nt-current to .nt-list-container-submit
    $(".nt-list-container-submit").removeClass("nt-current");
    // 1.3. set .nt-current to .nt-list-wrapper
    $(".nt-list-wrapper").removeClass("nt-current");
    if (action != "POST") { // set .nt-current when not Create
        $(button).closest(".nt-listitem").addClass("nt-current");
        $(button).closest(".nt-listitem").addClass("border-info border-5");

        $(button).closest(".nt-list-container-submit").addClass("nt-current");
        $(button).closest(".nt-list-wrapper").addClass("nt-current");
    }
}

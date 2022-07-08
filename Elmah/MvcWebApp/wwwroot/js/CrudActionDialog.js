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
 * data-nt-bs-modalsize // .modal-sm (NONE) .modal-lg .modal-xl this is bootstrap modal size
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
 * .nt-modal-body
 * .btn-nt-pagination-previous
 * .btn-nt-pagination-next
 * .btn-nt-action
 * .btn-group-nt-action-pagination
 * .btn-group-nt-action-createanotherone
 * .btn-group-nt-action-save
 * .btn-group-nt-action-delete
 * .btn-group-nt-action-details
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper, shared with other .js files
 * .nt-list-container-submit, shared with other .js files
 * .nt-listitem
 * .nt-editing // will be set on .nt-list-wrapper .nt-list-container-submit and .nt-listitem
 * .nt-paged-view-options: // read value for data-nt-view in this modal
 * 
 * 2.3. in ajax response html
 * .nt-hidden-modal-title, server side render modal title to a hidden field, will set to .modal-title 
 */

function attachCrudActionDialog() {
    let crudActionDialog = document.getElementById('crudActionDialog');

    // 1. Show Modal
    crudActionDialog.addEventListener('show.bs.modal', function (event) {
        let button = event.relatedTarget;

        const wrapper = $(button).closest(".nt-list-wrapper");
        let routeId = ""
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-options").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");
        let postbackurl = "";
        if (action == "PUT") { // Edit
            routeId = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-updateitem-url") + "/" + routeId;
        }
        else if (action == "POST") { // Create
            routeId = $(button).data("nt-route-id");
            postbackurl = $(wrapper).data("nt-createitem-url");
        }
        else if (action == "DELETE") {
            routeId = $(button).closest(".nt-listitem").data("nt-route-id");
            postbackurl = $(wrapper).data("nt-deleteitem-url") + "/" + routeId;
        }
        else {
            routeId = $(button).closest(".nt-listitem").data("nt-route-id");
        }
        const loadItemUrl = $(wrapper).data("nt-loaditem-url") + "/" + routeId;
        
        initializeModal(button, action, view, container, template, loadItemUrl, postbackurl)
        // 3. Ajax to get htmls
        ajaxLoadItem(loadItemUrl, view, container, template, action);
    })

    // 2. Hide Modal
    crudActionDialog.addEventListener('hide.bs.modal', function (event) {
        // 1.1. clear .nt-editing on all .nt-listitem, then set .nt-editing to current item,
        closeModal();
    })
}

$(document).ready(function () {
    attachCrudActionDialog();
    $("#crudActionDialog .btn-nt-action").click(function (e) {
        const self = this;
        $(this).attr("disabled", true); 
        const loadItemUrl = $("#crudActionDialog").data("nt-loadItemUrl");
        const postbackurl = $("#crudActionDialog").data("nt-postbackurl");
        const view = $("#crudActionDialog").data("nt-view");
        const container = $("#crudActionDialog").data("nt-container");
        const template = $("#crudActionDialog").data("nt-template");
        // Get FormData if there are in this dialog
        const form = $("#crudActionDialog form")
        let formData = [];
        if (!!form) {
            formData = new FormData($(form)[0]);
        }
        formData.append("view", view);
        formData.append("container", container);
        formData.append("template", template);

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
                // response part #1, status html
                if (splitResponse.length > 0) {
                    $("#crudActionDialog .nt-status").html(splitResponse[0]);
                    $("#crudActionDialog .nt-status").show();
                }
                // response part #1.1 TODO: should have a timer to auto hide after a few seconds.

                // response part #2, to update the item which is updated/created.
                const action = $("#crudActionDialog").data("nt-action");
                if (action === "PUT") { // EDIT 
                    if (splitResponse.length > 1) {
                        $(".nt-listitem.nt-editing").html(splitResponse[1]);
                    }
                    $(self).removeAttr("disabled");
                }
                else if (action === "POST") { // Create
                    if (splitResponse.length > 1) {
                        if (view === "List") { // html table
                            let theTbody = $($("#crudActionDialog").data("nt-list-wrapper-id") + " tbody");
                            theTbody.prepend(splitResponse[1]);
                        }
                    }

                    const createAnotherOneChecked = $("#crudActionDialog .modal-footer .btn-group-nt-action-createanotherone input").is(':checked');
                    if (createAnotherOneChecked) {
                        $(self).removeAttr("disabled");
                        ajaxLoadItem(loadItemUrl, view, container, template, action);
                    }
                }
                else if (action === "DELETE") {
                    const deleteSuccess = !!$("#crudActionDialog .nt-status .text-success");
                    if (deleteSuccess) {
                        // Mark as deleted. will be deleted when Dialog/Modal closed
                        $(".nt-listitem.nt-editing").addClass("nt-deleted");
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
    });
});

function closeModal() {
    let currentItem = $(".nt-listitem.nt-editing");
    const action = $("#crudActionDialog").data("nt-action");
    setTimeout(() => {
        if (action === "PUT" || action === "POST") { // Edit/Create, remove special border after 2 seconds
            // remove styling for Updated/Created items with .nt-editing after 2s Modal closed
            $(".nt-listitem.nt-editing").removeClass("border-warning border-5");
            $(".nt-listitem").removeClass("nt-editing");
        }
        else if (action === "DELETE") {
            // remove deleted items after 1s Modal closed
            $(".nt-listitem.nt-editing.nt-deleted").remove();
        }
    }, action === "DELETE" ? 1000 : 2000);
    // 1.2. clear .nt-editing to .nt-list-container-submit
    $(".nt-list-container-submit").removeClass("nt-editing");
    // 1.3. clear .nt-editing to .nt-list-wrapper
    $(".nt-list-wrapper").removeClass("nt-editing");

    // 2.1. hide status section
    $("#crudActionDialog .nt-result").hide();
    // 2.2. clear .nt-modal-body inner html, remove all htmls
    $("#crudActionDialog .nt-modal-body").empty();

    // 3. removeData data-nt-* attribute in this modal.
    $("#crudActionDialog").removeData("nt-action nt-view nt-container nt-template nt-postbackurl");

    // 4. hide Save/Delete button groups
    $("#crudActionDialog .modal-footer .btn-group-nt-action-save").hide();
    $("#crudActionDialog .modal-footer .btn-group-nt-action-delete").hide();
}

function ajaxLoadItem(loadItemUrl, view, container, template, action) {
    $.ajax({
        type: "GET",
        url: loadItemUrl,
        data: { view, container, template },
        async: false,
        contentType: "application/json",
        success: function(response) {
            // 3.1. add response html to .nt-modal-body
            let modalBody = $("#crudActionDialog .nt-modal-body");
            if (action == "PUT" || action == "POST") // wrap with <form>..</form> Edit or Create
            {
                modalBody.html("<form>" + response + "</form>");
            }
            else { // DELETE, <form>...</form> wrapped around .nt-btn-action-delete
                modalBody.html(response);
            }
            // console.log(response);
            // 3.2. set text for .modal-title in .nt.hidden-modal-title
            $("#crudActionDialog .modal-header .modal-title").text($("#crudActionDialog .modal-body .nt-hidden-modal-title").val());
        },
        failure: function(response) {
            // console.log(response);
        },
        error: function(response) {
        }
    });
}

function initializeModal(button, action, view, container, template, loadItemUrl, postbackurl) {
    // 1.1. clear .nt-editing on all .nt-listitem, then set .nt-editing to current item,
    $(".nt-listitem").removeClass("nt-editing");
    // 1.2. set .nt-editing to .nt-list-container-submit
    $(".nt-list-container-submit").removeClass("nt-editing");
    // 1.3. set .nt-editing to .nt-list-wrapper
    $(".nt-list-wrapper").removeClass("nt-editing");
    if (action != "POST") { // set .nt-editing when not Create
        $(button).closest(".nt-listitem").addClass("nt-editing");
        $(button).closest(".nt-listitem").addClass("border-warning border-5");
        
        $(button).closest(".nt-list-container-submit").addClass("nt-editing");
        $(button).closest(".nt-list-wrapper").addClass("nt-editing");
    }
    // 2. load the item partial view from .data-nt-partialurl
    const sizeCss = $(button).closest(".nt-list-wrapper").data("nt-bs-modalsize");
    if (!!sizeCss) {
        $("#crudActionDialog .modal-dialog").addClass(sizeCss);
    }
    $("#crudActionDialog").data("nt-action", action);
    $("#crudActionDialog").data("nt-view", view);
    $("#crudActionDialog").data("nt-container", container);
    $("#crudActionDialog").data("nt-template", template);
    $("#crudActionDialog").data("nt-loadItemUrl", loadItemUrl);
    $("#crudActionDialog").data("nt-postbackurl", postbackurl);
     
    // 2.1. Create: get id of the table wrapper .nt-list-wrapper
    if (action === "POST") { 
        $("#crudActionDialog").data("nt-list-wrapper-id", "#"+$(button).closest(".nt-list-wrapper").attr("id"));
    }
    // 3.2. clear .nt-result
    $("#crudActionDialog .nt-status").html("");
    $("#crudActionDialog .nt-status").hide();
    // 3.1. show/hide button based on CRUD Action Types
    $("#crudActionDialog div.btn-group").hide();
    if (action === "PUT") // EDIT 
    {
        $("#crudActionDialog .modal-footer .btn-group-nt-action-save").show();
        $("#crudActionDialog .modal-footer .btn-group-nt-action-pagination").show();
    }
    else if (action == "POST") { // Create
        $("#crudActionDialog .modal-footer .btn-group-nt-action-save").show();
        $("#crudActionDialog .modal-footer .btn-group-nt-action-createanotherone").show();
    }
    else if (action == "DELETE") { // Delete
        $("#crudActionDialog .modal-footer .btn-group-nt-action-delete").show();
        $("#crudActionDialog .modal-footer .btn-group-nt-action-pagination").show();
    }
    else { // Details
        $("#crudActionDialog .modal-footer .btn-group-nt-action-details").show();
        $("#crudActionDialog .modal-footer .btn-group-nt-action-pagination").show();
    }
}

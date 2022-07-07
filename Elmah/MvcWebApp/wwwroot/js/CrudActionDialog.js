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
 * .btn-nt-action-save
 * .btn-nt-action-delete
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
        const routeId = $(button).closest(".nt-listitem").data("nt-route-id");
        const loadItemUrl = $(wrapper).data("nt-loaditem-url") + "/" + routeId;
        const view = $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-options").val();
        const container = $(button).data("nt-container");
        const template = $(button).data("nt-template");
        const action = $(button).data("nt-action");
        let postbackurl = "";
        if (action == "PUT") { // Edit
            postbackurl = $(wrapper).data("nt-updateitem-url") + "/" + routeId;
        }
        else if (action == "POST") { // Create
            postbackurl = $(wrapper).data("nt-createitem-url") + "/" + routeId;
        }
        else if (action == "DELETE") {
            postbackurl = $(wrapper).data("nt-deleteitem-url") + "/" + routeId;
        }
        else {
            return;
        }
        
        initializeModal(button, action, view, container, template, postbackurl)
        // 3. Ajax to get htmls
        $.ajax({
            type: "GET",
            url: loadItemUrl,
            data: {view, container, template},
            async: false,
            contentType: "application/json",
            success: function (response) {
                // 3.1. add response html to .nt-modal-body
                let modalBody = $("#crudActionDialog .nt-modal-body");
                if (action == "PUT" || action == "POST") // wrap with <form>..</form> Edit or Create
                {
                    modalBody.html("<form>" + response +"</form>");
                }
                else { // DELETE, <form>...</form> wrapped around .nt-btn-action-delete
                    modalBody.html(response);
                }
                // console.log(response);
                // 3.2. set text for .modal-title in .nt.hidden-modal-title
                $("#crudActionDialog .modal-header .modal-title").text($("#crudActionDialog .modal-body .nt-hidden-modal-title").val());
            },
            failure: function (response) {
                // console.log(response);
            },
            error: function (response) {
                // console.log(response);
            }
        });
    })

    // 2. Hide Modal
    crudActionDialog.addEventListener('hide.bs.modal', function (event) {
        // 1.1. clear .nt-editing on all .nt-listitem, then set .nt-editing to current item,
        $(".nt-listitem").removeClass("nt-editing");
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

    })
}

$(document).ready(function () {
    attachCrudActionDialog();
    $("#crudActionDialog .btn-nt-action-save").click(function (e) {
        const self = this;
        $(this).attr("disabled", true);
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
        const action = $("#crudActionDialog").data("nt-action");
        $.ajax({
            type: "POST",
            url: postbackurl,
            data: formData,
            async: false,
            processData: false,
            contentType: false,
            dataType: "html",
            success: function (response) {
                $(self).removeAttr("disabled");
                // console.log(response);
                const splitResponse = response.split("===---------===");
                // response part #1, status html
                if (splitResponse.length > 0) {
                    $("#crudActionDialog .nt-status").html(splitResponse[0]);
                    $("#crudActionDialog .nt-status").show();
                }
                // response part #1.1 TODO: should have a timer to auto hide after a few seconds.

                // response part #2, to update the item which is updated/created.
                if (splitResponse.length > 1) {
                    $(".nt-listitem .nt-editing").html(splitResponse[1]);
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

function initializeModal(button, action, view, container, template, postbackurl) {
    // 1.1. clear .nt-editing on all .nt-listitem, then set .nt-editing to current item,
    $(".nt-listitem").removeClass("nt-editing");
    $(button).closest(".nt-listitem").addClass("nt-editing");
    // 1.2. set .nt-editing to .nt-list-container-submit
    $(".nt-list-container-submit").removeClass("nt-editing");
    $(button).closest(".nt-list-container-submit").addClass("nt-editing");
    // 1.3. set .nt-editing to .nt-list-wrapper
    $(".nt-list-wrapper").removeClass("nt-editing");
    $(button).closest(".nt-list-wrapper").addClass("nt-editing");
    // 2. load the item partial view from .data-nt-partialurl
    const sizeCss = $(button).closest(".nt-list-wrapper").data("nt-bs-modalsize");
    if (!!sizeCss) {
        $("#crudActionDialog .modal-dialog").addClass(sizeCss);
    }
    $("#crudActionDialog").data("nt-action", action);
    $("#crudActionDialog").data("nt-view", view);
    $("#crudActionDialog").data("nt-container", container);
    $("#crudActionDialog").data("nt-template", template);
    $("#crudActionDialog").data("nt-postbackurl", postbackurl);
    // 3.2. clear .nt-result
    $("#crudActionDialog .nt-status").html("");
    $("#crudActionDialog .nt-status").hide();
    // 3.1. show/hide button based on CRUD Action Types
    if (action === "PUT" || action === "POST") // EDIT || CREATE
    {
        $("#crudActionDialog .modal-footer .btn-nt-action-save").show();
        $("#crudActionDialog .modal-footer .btn-nt-action-save").removeAttr("disabled");
        $("#crudActionDialog .modal-footer .btn-nt-action-delete").hide();
    }
    else if (action == "DELETE") {
        $("#crudActionDialog .modal-footer .btn-nt-action-delete").show();
        $("#crudActionDialog .modal-footer .btn-nt-action-delete").removeAttr("disabled");
        $("#crudActionDialog .modal-footer .btn-nt-action-save").hide();
    }
    else {
        $("#crudActionDialog .modal-footer .btn-nt-action-delete").hide();
        $("#crudActionDialog .modal-footer .btn-nt-action-save").hide();
    }
}

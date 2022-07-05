// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * single dialog instance with id="crudActionDialog"
 * #crudActionDialog
 * 
 * data-nt-modelsize
 * data-nt-partialurl
 * data-nt-postbackurl
 * data-nt-action
 * 
 * .nt-result
 * .nt-modal-body
 * .nt-hidden-modal-title
 * .nt-btn-action-save
 * .nt-btn-action-delete
 * .nt-submit-text
 * .nt-list-wrapper, shared with other .js files
 * .nt-list-container-submit, shared with other .js files
 * .nt-listitem
 * .nt-editing(will set on .nt-list-container-submit and .nt-listitem)
 */

function attachCrudActionDialog() {
    var crudActionDialog = document.getElementById('crudActionDialog');
    crudActionDialog.addEventListener('hide.bs.modal', function (event) {
        // 1.1. clear .nt-editing on all .nt-listitem, then set .nt-editing to current item,
        $(".nt-listitem").removeClass("nt-editing");
        // 1.2. set .nt-editing to .nt-list-container-submit
        $(".nt-list-container-submit").removeClass("nt-editing");
        // 1.3. set .nt-editing to .nt-list-wrapper
        $(".nt-list-wrapper").removeClass("nt-editing");

        // 2.1. hide status section
        $("#crudActionDialog .nt-result").hide();
        // 2.2. clear .nt-modal-body inner html, remove all htmls
        $("#crudActionDialog .nt-modal-body").empty();

        // 3. btn-nt-* hide() and disabled

        // clear data-nt-action
        $("#crudActionDialog").data("nt-action", ""); 
    })
    crudActionDialog.addEventListener('show.bs.modal', function (event) {
        let button = event.relatedTarget;
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
        const sizeCss = $(button).data("nt-modelsize");
        if (!!sizeCss) {
            $("#crudActionDialog .modal-dialog").addClass(sizeCss);
        }
        const partialUrl = $(button).data("nt-partialurl");
        // 3. Ajax to get htmls
        $.ajax({
            type: "GET",
            url: partialUrl,
            async: false,
            contentType: "application/json",
            success: function (response) {
                const action = $(button).data("nt-action");
                // 3.1. add response html to .nt-modal-body
                let modalBody = $("#crudActionDialog .nt-modal-body");
                if (action == "PUT" || action == "POST") // wrap with <form>..</form> Edit or Create
                {
                    modalBody.html("<form>" + response +"</form>");
                }
                else {
                    modalBody.html(response);
                }
                // console.log(response);
                // 3.2. set text for .modal-title in .nt.hidden-modal-title
                $("#crudActionDialog .modal-header .modal-title").text($("#crudActionDialog .modal-body .nt-hidden-modal-title").val());
                // 3.3. clear .nt-result
                // 3.4. show/hide button based on CRUD Action Types
                const postbackurl = $(button).data("nt-postbackurl");
                initializeModal(action, postbackurl);
            },
            failure: function (response) {
                // console.log(response);
            },
            error: function (response) {
                // console.log(response);
            }
        });
    })
}

$(document).ready(function () {
    attachCrudActionDialog();
    $("#crudActionDialog .nt-btn-action-save").click(function (e) {
        const self = this;
        $(this).attr("disabled", true);
        const postbackurl = $(this).data("nt-postbackurl");
        // Get FormData if there are in this dialog
        const form = $("#crudActionDialog form")
        let formData = null;
        if (!!form) {
            formData = new FormData($(form)[0]);
        }

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
                    $("#crudActionDialog .nt-result").html(splitResponse[0]);
                    $("#crudActionDialog .nt-result").show();
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

function initializeModal(action, postbackurl) {
    $("#crudActionDialog").data("nt-action", action);
    // 3.2. clear .nt-result
    $("#crudActionDialog .nt-result").html("");
    $("#crudActionDialog .nt-result").hide();
    // 3.1. show/hide button based on CRUD Action Types
    if (action === "PUT" || action === "POST") // EDIT || CREATE
    {
        $("#crudActionDialog .modal-footer .nt-btn-action-save").data("nt-postbackurl", postbackurl);
        $("#crudActionDialog .modal-footer .nt-btn-action-save").show();
        $("#crudActionDialog .modal-footer .nt-btn-action-save").removeAttr("disabled");
        $("#crudActionDialog .modal-footer .nt-btn-action-delete").hide();
    }
    else if (action == "DELETE") {
        $("#crudActionDialog .modal-footer .nt-btn-action-delete").data("nt-postbackurl", postbackurl);
        $("#crudActionDialog .modal-footer .nt-btn-action-delete").show();
        $("#crudActionDialog .modal-footer .nt-btn-action-delete").removeAttr("disabled");
        $("#crudActionDialog .modal-footer .nt-btn-action-save").hide();
    }
    else {
        $("#crudActionDialog .modal-footer .nt-btn-action-delete").hide();
        $("#crudActionDialog .modal-footer .nt-btn-action-save").hide();
    }
}

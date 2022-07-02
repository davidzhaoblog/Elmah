// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// data-nt-modelsize
// data-nt-partialurl
// data-nt-postbackurl

// .nt-modal-body
// .nt-btn-save
// .nt-result
// .nt-btn-delete

function attachCrudActionDialog() {
    var crudActionDialog = document.getElementById('crudActionDialog');
    crudActionDialog.addEventListener('show.bs.modal', function (event) {
        // 1. load the item partial view from .data-nt-partialurl
        let button = event.relatedTarget;
        const sizeCss = $(button).data("nt-modelsize");
        if (!!sizeCss) {
            $("#crudActionDialog .modal-dialog").addClass(sizeCss);
        }
        const partialUrl = $(button).data("nt-partialurl");
        $.ajax({
            type: "GET",
            url: partialUrl,
            async: false,
            contentType: "application/json",
            success: function (response) {
                const action = $(button).data("action");
                // 1.1. add response html to .nt-modal-body
                let modalBody = $("#crudActionDialog .nt-modal-body");
                if (action == "PUT" || action == "POST") // wrap with <form>..</form> Edit or Create
                {
                    modalBody.html("<form>" + response +"</form>");
                }
                else {
                    modalBody.html(response);
                }
                // console.log(response);
                // 2. set text for .modal-title in .nt.hidden-modal-title
                $("#crudActionDialog .modal-header .modal-title").text($("#crudActionDialog .modal-body .nt-hidden-modal-title").val());
                // 3.2. clear .nt-result
                // 3.1. show/hide button based on CRUD Action Types
                const postbackurl = $(button).data("nt-postbackurl");
                initialize(action, postbackurl);
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
    $("#crudActionDialog .nt-btn-save").click(function (e) {
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
                $("#crudActionDialog .nt-result").html(response);
                $("#crudActionDialog .nt-result").show();
                // TODO: should have a timer to auto hide after a few seconds.
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

function initialize(action, postbackurl) {
    // 3.2. clear .nt-result
    $("#crudActionDialog .nt-result").html("");
    $("#crudActionDialog .nt-result").hide();
    // 3.1. show/hide button based on CRUD Action Types
    if (action === "PUT" || action === "POST") // EDIT || CREATE
    {
        $("#crudActionDialog .modal-footer .nt-btn-save").data("nt-postbackurl", postbackurl);
        $("#crudActionDialog .modal-footer .nt-btn-save").show();
        $("#crudActionDialog .modal-footer .nt-btn-save").removeAttr("disabled");
        $("#crudActionDialog .modal-footer .nt-btn-delete").hide();
    }
    else if (action == "DELETE") {
        $("#crudActionDialog .modal-footer .nt-btn-delete").data("nt-postbackurl", postbackurl);
        $("#crudActionDialog .modal-footer .nt-btn-delete").show();
        $("#crudActionDialog .modal-footer .nt-btn-delete").removeAttr("disabled");
        $("#crudActionDialog .modal-footer .nt-btn-save").hide();
    }
    else {
        $("#crudActionDialog .modal-footer .nt-btn-delete").hide();
        $("#crudActionDialog .modal-footer .nt-btn-save").hide();
    }
}

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
    });
});
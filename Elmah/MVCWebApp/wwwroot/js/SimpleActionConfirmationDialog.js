// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * single dialog instance with id="crudActionDialog"
 * simpleActionConfirmationDialog
 *
 * 1. data-nt- 
 * 1.1. data-nt- in this modal
 * data-nt-action
 * data-nt-view // from $($(wrapper).data("nt-submittarget")).children(".nt-paged-view-options").val();
 * data-nt-container // from $(button)
 * data-nt-template // from $(button)
 * data-nt-postbackurl // calculated
 * data-nt-pagination PREV/NEXT on .btn-nt-pagination
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
 * .btn-nt-pagination
 * .btn-nt-action
 * .btn-group-nt-action-pagination
 * .btn-group-nt-action-createanotherone
 * .btn-group-nt-action-save
 * .btn-group-nt-action-delete
 * .btn-group-nt-action-details
 * .nt-created, with .border-warning .border-3
 * .nt-updated, with .border-success .border-3
 * .nt-deleted, with .border-danger .border-3
 *  
 * 2.2. consumed css classes from where this modal is launched.
 * .nt-list-wrapper, shared with other .js files
 * .nt-list-container-submit, shared with other .js files
 * .nt-listitem
 * .nt-current // with on .nt-list-wrapper and .nt-list-container-submit, .nt-listitem(with .border-info .border-5)
 * .nt-paged-view-options: // read value for data-nt-view in this modal
 * 
 * 2.3. in ajax response html
 * .nt-hidden-modal-title, server side render modal title to a hidden field, will set to .modal-title 
 */
$(document).ready(function () {
    attachSimpleActionConfirmationDialog();
    attachSimpleActionConfirmationDialogEventHandler();
})

function attachSimpleActionConfirmationDialog() {
    let simpleActionConfirmationDialog = document.getElementById('simpleActionConfirmationDialog');

    // 1. Show Modal
    simpleActionConfirmationDialog.addEventListener('show.bs.modal', function (event) {
        let sourceButton = event.relatedTarget;

        $("#simpleActionConfirmationDialog .btn-nt-action-confirm").off();
        $("#simpleActionConfirmationDialog .btn-nt-action-confirm").click(function (e) {
            // 1. Batch Delete
            if ($(sourceButton).hasClass("nt-batch-action-delete")) {
                batchDelete(sourceButton);
            }
        });
    })
}

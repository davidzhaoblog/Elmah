
function attachFormDataChanged(theForm) {
    const origFormData = $(theForm).serialize();

    $(theForm).on('keyup change paste', 'input.nt-form-data, select.nt-form-data, textarea.nt-form-data', function () {
        const self = this;
        const itemStatusField = $(self).closest(".nt-listitem").find(".nt-item-status input");
        const currentItemStatus = $(itemStatusField).val(); 
        // 1. add/remove field changed css classes - nt-form-data-changed border-danger border-2
        if (currentItemStatus !== "New") { // not NoChange or Updated, see C# Framework.Models.ItemUIStatus
            const thisFieldChanged = $(self).val().toString() !== $(self).data("nt-value");
            if (thisFieldChanged) {
                $(self).closest(".form-group").addClass("nt-form-data-changed border-danger border-2");
            }
            else {
                $(self).closest(".form-group").removeClass("nt-form-data-changed border-danger border-2");
            }

            const newItemStatus = $(self).closest(".nt-listitem").find(".nt-form-data-changed").length === 0 ? "NoChange" : "Updated";
            $(itemStatusField).val(newItemStatus);
        }
        enableSaveButton(this);
    });

    $(theForm).on('keyup change paste', 'input.nt-form-check', function () {
        const self = this;
        const itemStatusField = $(self).closest(".nt-listitem").find(".nt-item-status input");
        const currentItemStatus = $(itemStatusField).val();
        // 1. add/remove field changed css classes - nt-form-data-changed border-danger border-2
        if (currentItemStatus !== "New") { // not NoChange or Updated, see C# Framework.Models.ItemUIStatus
            const thisFieldChanged = $(self).is(':checked').toString() !== $(self).data("nt-value").toLowerCase();
            if (thisFieldChanged) {
                $(self).closest(".form-group").addClass("nt-form-data-changed border-danger border-2");
            }
            else {
                $(self).closest(".form-group").removeClass("nt-form-data-changed border-danger border-2");
            }

            const newItemStatus = $(self).closest(".nt-listitem").find(".nt-form-data-changed").length === 0 ? "NoChange" : "Updated";
            $(itemStatusField).val(newItemStatus);
        }
        enableSaveButton(this);
    });
}

function enableSaveButton(changedSelf) {
    const listWrapperSelector = $(changedSelf).closest(".nt-list-wrapper");
    const formDataChanged = $(listWrapperSelector).find(".nt-form-data-changed").length > 0;
    // 2. update paged-view-option
    const submitTarget = $(listWrapperSelector).data("nt-submittarget");
    const view = $(submitTarget).find(".nt-paged-view-option-field").val();
    if (view === "EditableList") {
        $(listWrapperSelector).find(".nt-multiitem-editing-buttons .btn-nt-multiitems-editing-submit").prop("disabled", !formDataChanged);
    }
    else {

    }
}
// 4.Start. Clear all select/input inside .nt-advanced-search-field when .nt-advanced-search-button button clicked
/*
 * .nt-advanced-search-button
 * .nt-advanced-search-field
 */
$(document).ready($(function () {
    $('.nt-advanced-search-button').click(function (e) {
        $('.nt-advanced-search-field div div select').val('')
        $('.nt-advanced-search-field div div input').val('')
    });
}));
// 4.End. Clear all select/input inside .advanced-search-field when .advanced-search-button button clicked

// 5.start form ajax-submit: POST .ajax-partial-load-post
/* 
 * data-nt-partial-url
 * data-nt-updatetarget
 * data-nt-pageindex
 * 
 * .nt-ajax-partial-load-post-formdata
 * .nt-ajax-partial-load-get
 * .nt-list-container-submit
 * .nt-page-index
 * .nt-paged-view-option-field
 * .btn-nt-load-more
 *
 */
function pageLinkClicked(self) {
    const theForm = $($(self).closest(".nt-list-wrapper").data("nt-submittarget"));
    $(theForm).find(".nt-page-index").val($(self).data("nt-pageindex"));
    const url = $(theForm).data("nt-partial-url");
    const updateTarget = $($(theForm).data("nt-updatetarget")).find(".nt-list-container-submit");
    var formData = new FormData($(theForm)[0]);
    const pagedViewOption = $(theForm).children(".nt-paged-view-option-field").val();
    const pageIndex = $(theForm).children(".nt-page-index").val();
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            const toAppend = $(response);
            if (pagedViewOption !== "Tiles" || pageIndex == 1) {
                $(updateTarget).html(toAppend);
            }
            else {
                $(updateTarget).children(".btn-nt-load-more").remove()
                $(updateTarget).append(toAppend);
            }
            attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
            attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
            //console.log("success", response);
        },
        failure: function (response) {
            // console.log("failure", response);
        },
        error: function (response) {
            // console.log("error", response);
        }
    });
    return false;
}

$(document).ready($(function () {
    $('.nt-ajax-partial-load-get').submit(function (e) {
        const theForm = this;
        const url = $(theForm).data("nt-partial-url");
        const updateTarget = $($(theForm).data("nt-updatetarget")).find(".nt-list-container-submit");
        var data = $(theForm)
            //.filter(function (index, element) {
            //    console.log($(element).val());
            //    return !!$(element).val();
            //})
            .serialize();
        const pagedViewOption = $(theForm).children(".nt-paged-view-option-field").val();
        const pageIndex = $(theForm).children(".nt-page-index").val();
        //console.log(data);
        $.ajax({
            type: "GET",
            url: url,
            data: data,
            async: false,
            dataType: "html",
            success: function (response) {
                const toAppend = $(response);
                if (pagedViewOption !== "Tiles" || pageIndex == 1) {
                    $(updateTarget).html(toAppend);
                }
                else {
                    $(updateTarget).children(".btn-nt-load-more").remove()
                    $(updateTarget).append(toAppend);
                }
                attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
                attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
            },
            failure: function (response) {
                // console.log("failure", response);
            },
            error: function (response) {
                // console.log("error", response);
            }
        });
        e.preventDefault();
    });
}));

function attachIndexSearchSubmit(selector) {
    $(selector).submit(function (e) {
        indexSearchSubmit(this);
    });
}

function indexSearchSubmit(theForm) {
    const url = $(theForm).data("nt-partial-url");
    const updateTarget = $($(theForm).data("nt-updatetarget")).find(".nt-list-container-submit");
    var formData = new FormData($(theForm)[0]);
    const pagedViewOption = $(theForm).children(".nt-paged-view-option-field").val();
    const pageIndex = $(theForm).children(".nt-page-index").val();
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (response) {
            const toAppend = $(response);
            if (pagedViewOption !== "Tiles" || pageIndex == 1) {
                $(updateTarget).html(toAppend);
            }
            else {
                $(updateTarget).children(".btn-nt-load-more").remove();
                $(updateTarget).append(toAppend);
            }
            attachInlineEditingLaunchButtonClickEvent($(toAppend).find(".btn-nt-inline-editing"));
            attachIndividualSelectCheckboxClickEventHandler($(toAppend).find(".nt-list-bulk-select .form-check-input"));
            attachFormDataChanged($(toAppend).find("form"));
            attachMultiItemsDeleteCheckboxEvent($(toAppend).find("form"));
            setTimeout(() => {
                bootstrap.Modal.getOrCreateInstance(document.getElementById('fullScreenLoading')).hide();
            }, 1000);
        },
        failure: function(response) {
            // console.log("failure", response);
            setTimeout(() => {
                bootstrap.Modal.getOrCreateInstance(document.getElementById('fullScreenLoading')).hide();
            }, 1000);
        },
        error: function(response) {
            // console.log("error", response);
            setTimeout(() => {
                bootstrap.Modal.getOrCreateInstance(document.getElementById('fullScreenLoading')).hide();
            }, 1000);
        }
    });
}
// 5.end form ajax-submit
// 3.Start Pagination and OrderBy
/* 
 * data-nt-submittarget
 *
 * .nt-page-size-submit
 * .nt-order-by-submit
 * .nt-page-size
 * .nt-order-by
 * 
 */
$(document).ready($(function () {
    $(".nt-page-size-submit").change(function (e) {
        $($(this).closest(".nt-list-wrapper").data("nt-submittarget")).find(".nt-page-size").val(e.target.value);
        $($(this).closest(".nt-list-wrapper").data("nt-submittarget")).submit();
    });
}));
$(document).ready($(function () {
    $(".nt-order-by-submit").change(function (e) {
        $($(this).closest(".nt-list-wrapper").data("nt-submittarget")).find(".nt-order-by").val(e.target.value);
        $($(this).closest(".nt-list-wrapper").data("nt-submittarget")).submit();
    });
}));
// 3.End Pagination and OrderBy

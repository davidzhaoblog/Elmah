using Elmah.MvcWebApp.Models;
using Framework.Mvc.Models;
using Elmah.ServiceContracts;
using Elmah.Resx;
using Elmah.Models.Definitions;
using Elmah.Models;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Elmah.MvcWebApp.Controllers
{
    public class ElmahErrorController : Controller
    {
        private readonly IElmahErrorService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly ListSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahErrorController> _logger;

        public ElmahErrorController(
            IElmahErrorService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            ListSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<ElmahErrorController> logger)
        {
            _thisService = thisService;
            _selectListHelper = selectListHelper;
            _viewFeatureManager = viewFeatureManager;
            _dropDownListService = dropDownListService;
            _orderBysListHelper = orderBysListHelper;
            _mvcItemViewModelHelper = mvcItemViewModelHelper;
            _pagedSearchViewModelHelper = pagedSearchViewModelHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahError
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahErrorAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, ListViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultElmahErrorOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetElmahErrorPagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: ElmahError/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(ElmahErrorAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new ListViewModel<ElmahErrorDataModel.DefaultView[]>
            {
                UIListSetting = _viewFeatureManager.GetElmahErrorUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplates.Create.ToString() || uiParams.Template == ViewItemTemplates.Edit.ToString())
            {                pagedViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }

            if (uiParams.PagedViewOption == ListViewOptions.Table || uiParams.PagedViewOption == ListViewOptions.EditableTable)
            {
                return PartialView("~/Views/ElmahError/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == ListViewOptions.Tiles)
            {
                return PartialView("~/Views/ElmahError/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == ListViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/ElmahError/_SlideShow.cshtml", pagedViewModel);

        }

        // POST: ElmahError/AjaxBulkUpdateStatusCode
        [HttpPost, ActionName("AjaxBulkUpdateStatusCode")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkUpdateStatusCode(
            [FromQuery] ListViewOptions view,
            [FromForm] List<ElmahErrorIdentifier> ids,
            [Bind("StatusCode")] [FromForm] ElmahErrorDataModel.DefaultView data)
        {
            var result = await _thisService.BulkUpdate(
                new BatchActionRequest<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>
                {
                    Ids = ids,
                    ActionName = "StatusCode",
                    ActionData = data
                });
            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                    new AjaxResponseViewModel
                    {

                        Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                        PartialViews =
                            (from t in result.ResponseBody
                            select new Tuple<string, object>
                            (
                                view == ListViewOptions.Tiles
                                    ? "~/Views/ElmahError/_Tile.cshtml"
                                    : "~/Views/ElmahError/_TableItemTr.cshtml"
                                ,
                                new Framework.Mvc.Models.MvcItemViewModel<ElmahErrorDataModel.DefaultView>
                                {
                                    Template = ViewItemTemplates.Details.ToString(),
                                    Model = t,
                                    Status = System.Net.HttpStatusCode.OK,
                                    BulkSelected = true,
                                }
                            )).ToList()
                    });
            }
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


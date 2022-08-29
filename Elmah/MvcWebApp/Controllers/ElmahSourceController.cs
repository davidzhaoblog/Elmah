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
    public class ElmahSourceController : Controller
    {
        private readonly IElmahSourceService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly ListSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahSourceController> _logger;

        public ElmahSourceController(
            IElmahSourceService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            ListSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<ElmahSourceController> logger)
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

        // GET: ElmahSource
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahSourceAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, ListViewOptions.Table);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultElmahSourceOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetElmahSourcePagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: ElmahSource/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(ElmahSourceAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new ListViewModel<ElmahSourceDataModel[]>
            {
                UIListSetting = _viewFeatureManager.GetElmahSourceUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplates.Create.ToString() || uiParams.Template == ViewItemTemplates.Edit.ToString())
            {
            }

            if (uiParams.PagedViewOption == ListViewOptions.Table || uiParams.PagedViewOption == ListViewOptions.EditableTable)
            {
                return PartialView("~/Views/ElmahSource/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == ListViewOptions.Tiles)
            {
                return PartialView("~/Views/ElmahSource/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == ListViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/ElmahSource/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{Source}")] // Primary
        // GET: ElmahSource/AjaxLoadItem/{Source}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            ListViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            ElmahSourceIdentifier id)
        {
            ElmahSourceDataModel? result;
            if (template == ViewItemTemplates.Create.ToString())
            {
                result = _thisService.GetDefault();
                ViewBag.Status = System.Net.HttpStatusCode.OK;
            }
            else
            {
                var response = await _thisService.Get(id);
                result = response.ResponseBody;
            }

            var itemViewModel = new Framework.Mvc.Models.MvcItemViewModel<ElmahSourceDataModel>
            {
                UIItemFeatures = _viewFeatureManager.GetElmahSourceUIItemFeatures(),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplates.Edit.ToString() || template == ViewItemTemplates.Create.ToString())
            {

            }

            if ((view == ListViewOptions.Table || view == ListViewOptions.EditableTable) && container == CrudViewContainers.Inline)
            {
                if (template == ViewItemTemplates.Create.ToString())
                {
                    return PartialView($"_TableItemTr", itemViewModel);
                }
                else
                {
                    // By Default: _Table{template}Item.cshtml
                    // Developer can customize template name
                    return PartialView($"_Table{template}Item", itemViewModel);
                }
            }
            if (view == ListViewOptions.Tiles && container == CrudViewContainers.Inline)
            {
                // By Default: _List{template}Item.cshtml
                // Developer can customize template name
                return PartialView($"_Tile{template}Item", itemViewModel);
            }
            // By Default: _{template}.cshtml
            // Developer can customize template name
            return PartialView($"_{template}", itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: ElmahSource/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            [Bind("Source")] ElmahSourceDataModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);

                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == ListViewOptions.Table) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>> {
                                new Tuple<string, object>("~/Views/ElmahSource/_TableItemTr.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<ElmahSourceDataModel>{
                                        UIItemFeatures = _viewFeatureManager.GetElmahSourceUIItemFeatures(),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplates.Details.ToString(),
                                        IsCurrentItem = true,
                                        Model = result.ResponseBody!
                                    })
                            }});
                    }
                    //else // Tiles
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK,
                                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>>
                                {
                                    new Tuple<string, object>("~/Views/ElmahSource/_Tile.cshtml",
                                        new Framework.Mvc.Models.MvcItemViewModel<ElmahSourceDataModel>
                                        {
                                            UIItemFeatures = _viewFeatureManager.GetElmahSourceUIItemFeatures(),
                                            Status = System.Net.HttpStatusCode.OK,
                                            Template = ViewItemTemplates.Details.ToString(),
                                            IsCurrentItem = true,
                                            Model = result.ResponseBody!
                                        })
                                }
                            });
                    }
                }
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{Source}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: ElmahSource/AjaxEdit/{Source}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            ListViewOptions view,
            CrudViewContainers container,
            ViewItemTemplates template,
            ElmahSourceIdentifier id,
            [Bind("Source")] ElmahSourceDataModel input)
        {
            if (string.IsNullOrEmpty(id.Source) ||
                !string.IsNullOrEmpty(id.Source) && id.Source != input.Source)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel {
                    Status = System.Net.HttpStatusCode.NotFound, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(id, input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == ListViewOptions.Table) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK,
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>>
                            {
                                new Tuple<string, object>("~/Views/ElmahSource/_TableDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<ElmahSourceDataModel>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetElmahSourceUIItemFeatures(),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplates.Details.ToString(),
                                        IsCurrentItem = true,
                                        Model = result.ResponseBody!
                                    })
                            }
                        });
                    }
                    else // Tiles
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK,
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>>
                            {
                                new Tuple<string, object>("~/Views/ElmahSource/_TileDetailsItem.cshtml",
                                    new Framework.Mvc.Models.MvcItemViewModel<ElmahSourceDataModel>
                                    {
                                        UIItemFeatures = _viewFeatureManager.GetElmahSourceUIItemFeatures(),
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplates.Details.ToString(),
                                        IsCurrentItem = true,
                                        Model = result.ResponseBody!
                                    })
                            }
                        });
                    }
                }
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel {
                    Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ShowRequestId = false });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}


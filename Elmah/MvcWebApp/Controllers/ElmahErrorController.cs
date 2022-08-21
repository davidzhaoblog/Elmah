using Elmah.MvcWebApp.Models;
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
        private readonly Elmah.MvcWebApp.Models.OrderBysListHelper _orderBysListHelper;
        private readonly Elmah.MvcWebApp.Models.MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly Elmah.MvcWebApp.Models.PagedSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahErrorController> _logger;

        public ElmahErrorController(
            IElmahErrorService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            Elmah.MvcWebApp.Models.OrderBysListHelper orderBysListHelper,
            Elmah.MvcWebApp.Models.MvcItemViewModelHelper mvcItemViewModelHelper,
            Elmah.MvcWebApp.Models.PagedSearchViewModelHelper pagedSearchViewModelHelper,
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
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, Framework.Models.PagedViewOptions.EditableTable);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultElmahErrorOrderBys();
            }
            
            var result = await _thisService.Search(query);

            //List<NameValuePair> orderByList = _orderBysListHelper.GetElmahErrorOrderBys();

            //var textSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            //var otherDropDownLists = new Dictionary<string, List<Framework.Models.NameValuePair>>();
            //otherDropDownLists.Add(nameof(Elmah.Models.ElmahErrorAdvancedQuery.TimeUtcRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());

            //var topLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();

            //var vm = new PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorModel.DefaultView[]>
            //{
            //    Query = query,
            //    UIListSetting = _viewFeatureManager.GetDefaultEditableList(uiParams),
            //    Result = result,
            //    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
            //    OrderByList = orderByList,
            //    OtherDropDownLists = otherDropDownLists,
            //    PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
            //    TextSearchTypeList = _selectListHelper.GetTextSearchTypeList()
            //};

            var vm = await _pagedSearchViewModelHelper.GetElmahErrorPagedSearchViewModel("index", uiParams, query, result, true, true);

            return View(vm);
        }

        private List<NameValuePair> GetElmahErrorOrderBys()
        {
            return new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair{ Name = String.Format("{0} A-Z", _localizor.Get("TimeUtc")), Value = "TimeUtc~ASC" },
                new Framework.Models.NameValuePair{ Name = String.Format("{0} Z-A", _localizor.Get("TimeUtc")), Value = "TimeUtc~DESC" },
            });
        }

        // GET: ElmahError/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(ElmahErrorAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new PagedViewModel<ElmahErrorModel.DefaultView[]>
            {
                UIListSetting = _viewFeatureManager.GetElmahErrorUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplateNames.Create || uiParams.Template == ViewItemTemplateNames.Edit)
            {                pagedViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }

            if (uiParams.PagedViewOption == PagedViewOptions.Table || uiParams.PagedViewOption == PagedViewOptions.EditableTable)
            {
                return PartialView("~/Views/ElmahError/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("~/Views/ElmahError/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == PagedViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/ElmahError/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: ElmahError/Dashboard/{ErrorId}
        public async Task<IActionResult> Dashboard([FromRoute]ElmahErrorIdentifier id)
        {
            var result = await _thisService.GetCompositeModel(id);
            result.UIParamsList.Add(
                Elmah.Models.ElmahErrorCompositeModel.__DataOptions__.__Master__,
                new Framework.Models.UIParams { PagedViewOption = Framework.Models.PagedViewOptions.Card, Template = Framework.Models.ViewItemTemplateNames.Details });
            return View(result);
        }

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // GET: ElmahError/AjaxLoadItem/{ErrorId}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            ElmahErrorIdentifier id)
        {
            ElmahErrorModel.DefaultView? result;
            if (template == ViewItemTemplateNames.Create.ToString())
            {
                result = _thisService.GetDefault();
                ViewBag.Status = System.Net.HttpStatusCode.OK;
            }
            else
            {
                var response = await _thisService.Get(id);
                result = response.ResponseBody;
            }

            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                UIItemFeatures = _viewFeatureManager.GetElmahErrorUIItemFeatures(),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplateNames.Edit.ToString() || template == ViewItemTemplateNames.Create.ToString())
            {
                itemViewModel.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }

            if ((view == PagedViewOptions.Table || view == PagedViewOptions.EditableTable) && container == CrudViewContainers.Inline)
            {
                if (template == ViewItemTemplateNames.Create.ToString())
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
            if (view == PagedViewOptions.Tiles && container == CrudViewContainers.Inline)
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
        // POST: ElmahError/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,AllXml")] ElmahErrorModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);

                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == PagedViewOptions.Table) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>> {
                                new Tuple<string, object>("~/Views/ElmahError/_TableItemTr.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>{
                                        Template = ViewItemTemplateNames.Details.ToString(),
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
                                    new Tuple<string, object>("~/Views/ElmahError/_Tile.cshtml",
                                        new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
                                        {
                                            Status = System.Net.HttpStatusCode.OK,
                                            Template = ViewItemTemplateNames.Details.ToString(),
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

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        [HttpPost, ActionName("AjaxDelete")]
        // POST: ElmahError/AjaxDelete/{ErrorId}
        public async Task<IActionResult> AjaxDelete(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [FromRoute] ElmahErrorIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: ElmahError/AjaxEdit/{ErrorId}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ElmahErrorIdentifier id,
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,AllXml")] ElmahErrorModel.DefaultView input)
        {
            if (!id.ErrorId.HasValue ||
                id.ErrorId.HasValue && id.ErrorId != input.ErrorId)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel {
                    Status = System.Net.HttpStatusCode.NotFound, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(id, input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == PagedViewOptions.Table) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK,
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>>
                            {
                                new Tuple<string, object>("~/Views/ElmahError/_TableDetailsItem.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
                                    {
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplateNames.Details.ToString(),
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
                                new Tuple<string, object>("~/Views/ElmahError/_TileDetailsItem.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
                                    {
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplateNames.Details.ToString(),
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

        // POST: ElmahError//AjaxBulkDelete
        [HttpPost, ActionName("AjaxBulkDelete")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkDelete(
            [FromForm] BatchActionViewModel<ElmahErrorIdentifier> data)
        {
            var result = await _thisService.BulkDelete(data.Ids);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: ElmahError/AjaxBulkUpdateStatusCode
        [HttpPost, ActionName("AjaxBulkUpdateStatusCode")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkUpdateStatusCode(
            [FromQuery] Framework.Models.PagedViewOptions view,
            [FromForm] List<ElmahErrorIdentifier> ids,
            [Bind("StatusCode")] [FromForm] ElmahErrorModel.DefaultView data)
        {
            var result = await _thisService.BulkUpdate(
                new BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorModel.DefaultView>
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
                                view == PagedViewOptions.Tiles
                                    ? "~/Views/ElmahError/_Tile.cshtml"
                                    : "~/Views/ElmahError/_TableItemTr.cshtml"
                                ,
                                new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
                                {
                                    Template = ViewItemTemplateNames.Details.ToString(),
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

        // POST: ElmahError/AjaxMultiItemsCUDSubmit
        [HttpPost, ActionName("AjaxMultiItemsCUDSubmit")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxMultiItemsCUDSubmit(
            [FromQuery] PagedViewOptions view,
            [FromForm] List<ElmahErrorModel.DefaultView> data)
        {
            if(data == null || !data.Any(t=> t.IsDeleted______ && t.ItemUIStatus______ != ItemUIStatus.New || !t.IsDeleted______ && t.ItemUIStatus______ == ItemUIStatus.New || !t.IsDeleted______ && t.ItemUIStatus______ == ItemUIStatus.Updated))
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                    new AjaxResponseViewModel
                    {
                        Status = System.Net.HttpStatusCode.NoContent,
                        Message = "NoContent",
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    });
            }

            var multiItemsCUDModel = new MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorModel.DefaultView>
            {
                DeleteItems =
                    (from t in data
                    where t.IsDeleted______ && t.ItemUIStatus______ != ItemUIStatus.New
                    select new ElmahErrorIdentifier { ErrorId = t.ErrorId }).ToList(),
                NewItems =
                    (from t in data
                     where !t.IsDeleted______ && t.ItemUIStatus______ == ItemUIStatus.New
                     select t).ToList(),
                UpdateItems =
                    (from t in data
                     where !t.IsDeleted______ && t.ItemUIStatus______ == ItemUIStatus.Updated
                     select t).ToList(),
            };

            // although we have the NewItems and UpdatedITems in result, but we have to Mvc Core JQuery/Ajax refresh the whole list because array binding.
            var result = await _thisService.MultiItemsCUD(multiItemsCUDModel);

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                new AjaxResponseViewModel
                {
                    Status = result.Status,
                    ShowMessage = result.Status == System.Net.HttpStatusCode.OK,
                    Message = result.Status == System.Net.HttpStatusCode.OK ? _localizor.Get("Click Close To Reload this List") : result.StatusMessage,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
        }

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: ElmahError/Edit/{ErrorId}
        public async Task<IActionResult> Edit([FromRoute]ElmahErrorIdentifier id)
        {
            if (!id.ErrorId.HasValue)
            {
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();

            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                Model = result.ResponseBody
            };
            return View(itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // POST: ElmahError/Edit/{ErrorId}
        public async Task<IActionResult> Edit(
            [FromRoute]ElmahErrorIdentifier id,
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,AllXml")] ElmahErrorModel.DefaultView input)
        {
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();

            if (!id.ErrorId.HasValue ||
                id.ErrorId.HasValue && id.ErrorId != input.ErrorId)
            {
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            if (!ModelState.IsValid)
            {
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "Bad Request",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Update(id, input);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // GET: ElmahError/Details/{ErrorId}
        public async Task<IActionResult> Details([FromRoute]ElmahErrorIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: ElmahError/Create
        public async Task<IActionResult> Create()
        {
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();

            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = _thisService.GetDefault(),
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
            };
            return View(itemViewModel);
        }

        // POST: ElmahError/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,AllXml")] ElmahErrorModel.DefaultView input)
        {
            var topLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();

            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplateNames.Create.ToString(),
                    Model = result.ResponseBody,
                    TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = "Bad Request",
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = input, // should GetbyId again and merge content not in postback
                TopLevelDropDownListsFromDatabase = topLevelDropDownListsFromDatabase,
            };
            return View(itemViewModel1);
        }

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // GET: ElmahError/Delete/{ErrorId}
        public async Task<IActionResult> Delete([FromRoute]ElmahErrorIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Delete.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // POST: ElmahError/Delete/{ErrorId}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahErrorIdentifier id)
        {
            var result1 = await _thisService.Get(id);
            var result = await _thisService.Delete(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Delete.ToString(),
                Model = result1.ResponseBody,
            };
            return View(itemViewModel);
        }
    }
}


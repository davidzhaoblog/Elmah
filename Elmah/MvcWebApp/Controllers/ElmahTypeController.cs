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
    public class ElmahTypeController : Controller
    {
        private readonly IElmahTypeService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeatureManager;
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly PagedSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahTypeController> _logger;

        public ElmahTypeController(
            IElmahTypeService thisService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeatureManager,
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            PagedSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<ElmahTypeController> logger)
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

        // GET: ElmahType
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahTypeAdvancedQuery query, UIParams uiParams)
        {
            _viewFeatureManager.DefaultUIParamsIfNeeds(uiParams, PagedViewOptions.EditableTable);
            // UIParams.PagedViewOption is not null here
            query.PaginationOption = _viewFeatureManager.HardCodePaginationOption(uiParams.PagedViewOption!.Value, query.PaginationOption);
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = _orderBysListHelper.GetDefaultElmahTypeOrderBys();
            }

            var result = await _thisService.Search(query);

            var vm = await _pagedSearchViewModelHelper.GetElmahTypePagedSearchViewModel("index", uiParams, query, result, true, true);
            return View(vm);
        }

        // GET: ElmahType/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(ElmahTypeAdvancedQuery query, UIParams uiParams)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new PagedViewModel<ElmahTypeModel[]>
            {
                UIListSetting = _viewFeatureManager.GetElmahTypeUIListSetting(String.Empty, uiParams),
                Result = result,
            };

            if(uiParams.Template == ViewItemTemplateNames.Create || uiParams.Template == ViewItemTemplateNames.Edit)
            {
            }

            if (uiParams.PagedViewOption == PagedViewOptions.Table || uiParams.PagedViewOption == PagedViewOptions.EditableTable)
            {
                return PartialView("~/Views/ElmahType/_Table.cshtml", pagedViewModel);
            }
            else if (uiParams.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("~/Views/ElmahType/_Tiles.cshtml", pagedViewModel);
            }
            //else // if (uiParams.PagedViewOption == PagedViewOptions.SlideShow)
            // SlideShow
            return PartialView("~/Views/ElmahType/_SlideShow.cshtml", pagedViewModel);

        }

        [Route("[controller]/[action]/{Type}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: ElmahType/Dashboard/{Type}
        public async Task<IActionResult> Dashboard([FromRoute]ElmahTypeIdentifier id)
        {
            var result = await _thisService.GetCompositeModel(id);

            result.UIParamsList.Add(
                ElmahTypeCompositeModel.__DataOptions__.__Master__,
                new UIParams { PagedViewOption = PagedViewOptions.Card, Template = ViewItemTemplateNames.Details });

            result.UIParamsList.Add(
                ElmahTypeCompositeModel.__DataOptions__.ElmahErrors_Via_Type,
                new UIParams { PagedViewOption = PagedViewOptions.Table, Template = ViewItemTemplateNames.Details });

            return View(result);
        }

        [Route("[controller]/[action]/{Type}")] // Primary
        // GET: ElmahType/AjaxLoadItem/{Type}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            ElmahTypeIdentifier id)
        {
            ElmahTypeModel? result;
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

            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
            {
                UIItemFeatures = _viewFeatureManager.GetElmahTypeUIItemFeatures(),
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                IndexInArray = index ?? 0,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplateNames.Edit.ToString() || template == ViewItemTemplateNames.Create.ToString())
            {

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
        // POST: ElmahType/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [Bind("Type")] ElmahTypeModel input)
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
                                new Tuple<string, object>("~/Views/ElmahType/_TableItemTr.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>{
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
                                    new Tuple<string, object>("~/Views/ElmahType/_Tile.cshtml",
                                        new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
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

        [Route("[controller]/[action]/{Type}")] // Primary
        [HttpPost, ActionName("AjaxDelete")]
        // POST: ElmahType/AjaxDelete/{Type}
        public async Task<IActionResult> AjaxDelete(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [FromRoute] ElmahTypeIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{Type}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: ElmahType/AjaxEdit/{Type}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ElmahTypeIdentifier id,
            [Bind("Type")] ElmahTypeModel input)
        {
            if (string.IsNullOrEmpty(id.Type) ||
                !string.IsNullOrEmpty(id.Type) && id.Type != input.Type)
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
                                new Tuple<string, object>("~/Views/ElmahType/_TableDetailsItem.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
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
                                new Tuple<string, object>("~/Views/ElmahType/_TileDetailsItem.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
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

        // POST: ElmahType//AjaxBulkDelete
        [HttpPost, ActionName("AjaxBulkDelete")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkDelete(
            [FromForm] BatchActionViewModel<ElmahTypeIdentifier> data)
        {
            var result = await _thisService.BulkDelete(data.Ids);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: ElmahType/AjaxMultiItemsCUDSubmit
        [HttpPost, ActionName("AjaxMultiItemsCUDSubmit")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxMultiItemsCUDSubmit(
            [FromQuery] PagedViewOptions view,
            [FromForm] List<ElmahTypeModel> data)
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

            var multiItemsCUDModel = new MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeModel>
            {
                DeleteItems =
                    (from t in data
                    where t.IsDeleted______ && t.ItemUIStatus______ != ItemUIStatus.New
                    select new ElmahTypeIdentifier { Type = t.Type }).ToList(),
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

        [Route("[controller]/[action]/{Type}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: ElmahType/Edit/{Type}
        public async Task<IActionResult> Edit([FromRoute]ElmahTypeIdentifier id)
        {
            if (string.IsNullOrEmpty(id.Type))
            {
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),

                Model = result.ResponseBody
            };
            return View(itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{Type}")] // Primary
        // POST: ElmahType/Edit/{Type}
        public async Task<IActionResult> Edit(
            [FromRoute]ElmahTypeIdentifier id,
            [Bind("Type")] ElmahTypeModel input)
        {
            if (string.IsNullOrEmpty(id.Type) ||
                !string.IsNullOrEmpty(id.Type) && id.Type != input.Type)
            {
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),

                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            if (!ModelState.IsValid)
            {
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "Bad Request",
                    Template = ViewItemTemplateNames.Edit.ToString(),

                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Update(id, input);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),

                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{Type}")] // Primary
        // GET: ElmahType/Details/{Type}
        public async Task<IActionResult> Details([FromRoute]ElmahTypeIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: ElmahType/Create
        public async Task<IActionResult> Create()
        {
            var itemViewModel = await Task.FromResult(new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = _thisService.GetDefault(),

            });

            return View(itemViewModel);
        }

        // POST: ElmahType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Type")] ElmahTypeModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplateNames.Create.ToString(),
                    Model = result.ResponseBody,

                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = "Bad Request",
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = input, // should GetbyId again and merge content not in postback

            };
            return View(itemViewModel1);
        }

        [Route("[controller]/[action]/{Type}")] // Primary
        // GET: ElmahType/Delete/{Type}
        public async Task<IActionResult> Delete([FromRoute]ElmahTypeIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
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

        [Route("[controller]/[action]/{Type}")] // Primary
        // POST: ElmahType/Delete/{Type}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahTypeIdentifier id)
        {
            var result1 = await _thisService.Get(id);
            var result = await _thisService.Delete(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahTypeModel>
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


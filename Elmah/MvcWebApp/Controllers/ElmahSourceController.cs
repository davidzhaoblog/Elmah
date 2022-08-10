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
    public class ElmahSourceController : Controller
    {
        private readonly IElmahSourceService _thisService;
        private readonly SelectListHelper _selectListHelper;
        private readonly IDropDownListService _dropDownListService;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahSourceController> _logger;

        public ElmahSourceController(
            IElmahSourceService thisService,
            SelectListHelper selectListHelper,
            IDropDownListService dropDownListService,
            IUIStrings localizor,
            ILogger<ElmahSourceController> logger)
        {
            _thisService = thisService;
            _selectListHelper = selectListHelper;
            _dropDownListService = dropDownListService;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahSource
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahSourceAdvancedQuery query, MvcListSetting uiSetting)
        {
            if (uiSetting.PagedViewOption == PagedViewOptions.Tiles)
            {
                query.PaginationOption = PaginationOptions.LoadMore;
            }
            else if (uiSetting.PagedViewOption == PagedViewOptions.List || uiSetting.PagedViewOption == PagedViewOptions.EditableList)
            {
                query.PaginationOption = PaginationOptions.Paged;
            }

            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Source")), Value = "Source~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Source")), Value = "Source~DESC" },
            });
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahSourceAdvancedQuery, MvcListSetting, MvcListFeatures, ElmahSourceModel[]>
            {
                Query = query,
                UISetting = uiSetting,
                UIFeatures = IndexViewFeatures.GetElmahErrorEditableList(),

                Result = result
            });
        }

        // GET: ElmahSource/AjaxLoadItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxLoadItems(ElmahSourceAdvancedQuery query, MvcListSetting uiSetting)
        {
            var result = await _thisService.Search(query);
            var pagedViewModel = new PagedViewModel<MvcListSetting, MvcListFeatures, ElmahSourceModel[]>
            {
                UISetting = uiSetting,
                UIFeatures = uiSetting.PagedViewOption == PagedViewOptions.EditableList ? IndexViewFeatures.GetElmahErrorEditableList() : null,
                Result = result,
            };

            if(uiSetting.Template == ViewItemTemplateNames.Create || uiSetting.Template == ViewItemTemplateNames.Edit)
            {
            }

            if (uiSetting.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("_Tiles", pagedViewModel);
            }
            else if (uiSetting.PagedViewOption == PagedViewOptions.SlideShow)
            {
                return PartialView("_SlideShow", pagedViewModel);
            }

            return PartialView("_List", pagedViewModel);
        }

        [Route("[controller]/[action]/{Source}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: ElmahSource/Dashboard/{Source}
        public async Task<IActionResult> Dashboard([FromRoute]ElmahSourceIdentifier id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }

        [Route("[controller]/[action]/{Source}")] // Primary
        // GET: ElmahSource/AjaxLoadItem/{Source}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            int? index, // for EditableList
            ElmahSourceIdentifier id)
        {
            ElmahSourceModel? result;
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

            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = template,
                IsCurrentItem = true,
                ListSetting = new MvcListSetting { PagedViewOption = view, Template = Enum.Parse<ViewItemTemplateNames>(template) },
                ListFeatures = IndexViewFeatures.GetElmahErrorEditableList(),
                IndexInArray = index ?? 10,
                Model = result
            };

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplateNames.Edit.ToString() || template == ViewItemTemplateNames.Create.ToString())
            {

            }

            if ((view == PagedViewOptions.List || view == PagedViewOptions.EditableList ) && container == CrudViewContainers.Inline)
            {
                if (template == ViewItemTemplateNames.Create.ToString())
                {
                    return PartialView($"_ListItemTr", itemViewModel);
                }
                else
                {
                    // By Default: _List{template}Item.cshtml
                    // Developer can customize template name
                    return PartialView($"_List{template}Item", itemViewModel);
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
        // POST: ElmahSource/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [Bind("Source")] ElmahSourceModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);

                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    if (view == PagedViewOptions.List) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>> {
                                new Tuple<string, object>("~/Views/ElmahSource/_ListItemTr.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>{
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
                                    new Tuple<string, object>("~/Views/ElmahSource/_Tile.cshtml",
                                        new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
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

        [Route("[controller]/[action]/{Source}")] // Primary
        [HttpPost, ActionName("AjaxDelete")]
        // POST: ElmahSource/AjaxDelete/{Source}
        public async Task<IActionResult> AjaxDelete(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [FromRoute] ElmahSourceIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{Source}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: ElmahSource/AjaxEdit/{Source}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ElmahSourceIdentifier id,
            [Bind("Source")] ElmahSourceModel input)
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
                    if (view == PagedViewOptions.List) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK,
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>>
                            {
                                new Tuple<string, object>("~/Views/ElmahSource/_ListDetailsItem.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
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
                                new Tuple<string, object>("~/Views/ElmahSource/_TileDetailsItem.cshtml",
                                    new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
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

        // POST: ElmahSource//AjaxBulkDelete
        [HttpPost, ActionName("AjaxBulkDelete")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkDelete(
            [FromForm] BatchActionViewModel<ElmahSourceIdentifier> data)
        {
            var result = await _thisService.BulkDelete(data.Ids);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: ElmahSource/AjaxMultiItemsSubmit
        [HttpPost, ActionName("AjaxMultiItemsSubmit")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxMultiItemsSubmit(
            [FromQuery] PagedViewOptions view,
            [FromForm] List<ElmahSourceModel> data)
        {
            return View();
        }

        [Route("[controller]/[action]/{Source}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: ElmahSource/Edit/{Source}
        public async Task<IActionResult> Edit([FromRoute]ElmahSourceIdentifier id)
        {
            if (string.IsNullOrEmpty(id.Source))
            {
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
                {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusMessage = "Not Found",
                    Template = ViewItemTemplateNames.Edit.ToString(),
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Get(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
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

        [Route("[controller]/[action]/{Source}")] // Primary
        // POST: ElmahSource/Edit/{Source}
        public async Task<IActionResult> Edit(
            [FromRoute]ElmahSourceIdentifier id,
            [Bind("Source")] ElmahSourceModel input)
        {
            if (string.IsNullOrEmpty(id.Source) ||
                !string.IsNullOrEmpty(id.Source) && id.Source != input.Source)
            {
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
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
                var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
                {
                    Status = System.Net.HttpStatusCode.BadRequest,
                    StatusMessage = "Bad Request",
                    Template = ViewItemTemplateNames.Edit.ToString(),

                    Model = input, // should GetbyId again and merge content not in postback
                };
                return View(itemViewModel1);
            }

            var result = await _thisService.Update(id, input);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Edit.ToString(),

                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        [Route("[controller]/[action]/{Source}")] // Primary
        // GET: ElmahSource/Details/{Source}
        public async Task<IActionResult> Details([FromRoute]ElmahSourceIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
            {
                Status = result.Status,
                StatusMessage = result.StatusMessage,
                Template = ViewItemTemplateNames.Details.ToString(),
                Model = result.ResponseBody,
            };
            return View(itemViewModel);
        }

        // GET: ElmahSource/Create
        public async Task<IActionResult> Create()
        {
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
            {
                Status = System.Net.HttpStatusCode.OK,
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = _thisService.GetDefault(),

            };
            return View(itemViewModel);
        }

        // POST: ElmahSource/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Source")] ElmahSourceModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
                {
                    Status = result.Status,
                    StatusMessage = result.StatusMessage,
                    Template = ViewItemTemplateNames.Create.ToString(),
                    Model = result.ResponseBody,

                };
                return View(itemViewModel);
            }

            var itemViewModel1 = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
            {
                Status = System.Net.HttpStatusCode.BadRequest,
                StatusMessage = "Bad Request",
                Template = ViewItemTemplateNames.Create.ToString(),
                Model = input, // should GetbyId again and merge content not in postback

            };
            return View(itemViewModel1);
        }

        [Route("[controller]/[action]/{Source}")] // Primary
        // GET: ElmahSource/Delete/{Source}
        public async Task<IActionResult> Delete([FromRoute]ElmahSourceIdentifier id)
        {
            var result = await _thisService.Get(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
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

        [Route("[controller]/[action]/{Source}")] // Primary
        // POST: ElmahSource/Delete/{Source}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahSourceIdentifier id)
        {
            var result1 = await _thisService.Get(id);
            var result = await _thisService.Delete(id);
            var itemViewModel = new Elmah.MvcWebApp.Models.MvcItemViewModel<ElmahSourceModel>
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


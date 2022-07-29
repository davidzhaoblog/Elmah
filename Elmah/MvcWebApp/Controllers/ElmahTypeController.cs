using Elmah.ServiceContracts;
using Elmah.MvcWebApp.Models;
using Elmah.Resx;
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
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahTypeController> _logger;

        public ElmahTypeController(
            IElmahTypeService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahTypeController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahType
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahTypeAdvancedQuery query)
        {
            if (query.PagedViewOption == PagedViewOptions.Tiles)
            {
                query.PaginationOption = PaginationOptions.LoadMore;
            }
            else if (query.PagedViewOption == PagedViewOptions.List)
            {
                query.PaginationOption = PaginationOptions.Paged;
            }

            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Type")), Value = "Type~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Type")), Value = "Type~DESC" },
            });
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeModel[]> { Query = query, Result = result });
        }

        // GET: ElmahType/AjaxMultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxMultiItems(ElmahTypeAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            if (query.PagedViewOption == PagedViewOptions.List)
            {
                return PartialView("_List", result);
            }
            else if (query.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("_Tiles", result);
            }
            return PartialView("_SlideShow", result);
        }

        [Route("[controller]/[action]/{Type}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: ElmahType/Dashboard/{Type}
        public async Task<IActionResult> Dashboard([FromRoute]ElmahTypeIdentifier id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }

        [Route("[controller]/[action]/{Type}")] // Primary
        // GET: ElmahType/AjaxLoadItem/{Type}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
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
                ViewBag.Status = response.Status;
                ViewBag.StatusMessage = response.StatusMessage;
            }

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplateNames.Edit.ToString() || template == ViewItemTemplateNames.Create.ToString())
            {

            }

            ViewBag.Template = template;
            if (view == PagedViewOptions.List && container == CrudViewContainers.Inline)
            {
                // By Default: _List{template}Item.cshtml
                // Developer can customize template name
                ViewBag.Template = template;
                return PartialView($"_List{template}Item",
                    new ItemViewModel<ElmahTypeModel>
                    {
                        Status = System.Net.HttpStatusCode.OK,
                        Template = template,
                        IsCurrentItem = true,
                        HtmlNamePrefix = "Model.ResponseBody",
                        HtmlNameUseArrayIndex = true,
                        IndexInArray = 1,
                        Model = result
                    });
            }
            if (view == PagedViewOptions.Tiles && container == CrudViewContainers.Inline)
            {
                // By Default: _List{template}Item.cshtml
                // Developer can customize template name
                return PartialView($"_Tile{template}Item",
                    new ItemViewModel<ElmahTypeModel>
                    {
                        Status = System.Net.HttpStatusCode.OK,
                        Template = template,
                        IsCurrentItem = true,
                        HtmlNamePrefix = "Model.ResponseBody",
                        HtmlNameUseArrayIndex = true,
                        IndexInArray = 1,
                        Model = result
                    });
            }
            // By Default: _{template}.cshtml
            // Developer can customize template name
            return PartialView($"_{template}", result);
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
                    if (view == PagedViewOptions.List) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>> {
                                new Tuple<string, object>("~/Views/ElmahType/_ListItemTr.cshtml",
                                    new ItemViewModel<ElmahTypeModel>{
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
                                        new ItemViewModel<ElmahTypeModel>
                                        {
                                            Status = System.Net.HttpStatusCode.OK,
                                            Template = ViewItemTemplateNames.Details.ToString(),
                                            IsCurrentItem = true,
                                            HtmlNamePrefix = "Model.ResponseBody",
                                            HtmlNameUseArrayIndex = true,
                                            IndexInArray = 1,
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
                    if (view == PagedViewOptions.List) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK,
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>>
                            {
                                new Tuple<string, object>("~/Views/ElmahType/_ListDetailsItem.cshtml",
                                    new ItemViewModel<ElmahTypeModel>
                                    {
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplateNames.Details.ToString(),
                                        IsCurrentItem = true,
                                        HtmlNamePrefix = "Model.ResponseBody",
                                        HtmlNameUseArrayIndex = true,
                                        IndexInArray = 1,
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
                                    new ItemViewModel<ElmahTypeModel>
                                    {
                                        Status = System.Net.HttpStatusCode.OK,
                                        Template = ViewItemTemplateNames.Details.ToString(),
                                        IsCurrentItem = true,
                                        HtmlNamePrefix = "Model.ResponseBody",
                                        HtmlNameUseArrayIndex = true,
                                        IndexInArray = 1,
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

        [Route("[controller]/[action]/{Type}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: ElmahType/Edit/{Type}
        public async Task<IActionResult> Edit([FromRoute]ElmahTypeIdentifier id)
        {
            if (string.IsNullOrEmpty(id.Type))
            {
                ViewBag.Status = System.Net.HttpStatusCode.NotFound;
                ViewBag.StatusMessage = "Not Found";
                return View();
            }

            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;

            return View(result.ResponseBody);
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
                ViewBag.Status = System.Net.HttpStatusCode.NotFound;
                ViewBag.StatusMessage = "Not Found";

                return View(input);
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(id, input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
                ViewBag.Status = result.Status;
                ViewBag.StatusMessage = result.StatusMessage;
            }

            return View(input);
        }

        [Route("[controller]/[action]/{Type}")] // Primary
        // GET: ElmahType/Details/{Type}
        public async Task<IActionResult> Details([FromRoute]ElmahTypeIdentifier id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        // GET: ElmahType/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Status = System.Net.HttpStatusCode.OK;

            return View();
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
                if(result.Status == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Status = result.Status;
                ViewBag.StatusMessage = result.StatusMessage;
            }

            return View(input);
        }

        [Route("[controller]/[action]/{Type}")] // Primary
        // GET: ElmahType/Delete/{Type}
        public async Task<IActionResult> Delete([FromRoute]ElmahTypeIdentifier id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{Type}")] // Primary
        // POST: ElmahType/Delete/{Type}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahTypeIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;

            var result1 = await _thisService.Get(id);
            return View(result1.ResponseBody);
        }
    }
}


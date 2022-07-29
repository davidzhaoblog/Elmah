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
    public class ElmahUserController : Controller
    {
        private readonly IElmahUserService _thisService;

        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahUserController> _logger;

        public ElmahUserController(
            IElmahUserService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahUserController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahUser
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahUserAdvancedQuery query)
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
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("User")), Value = "User~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("User")), Value = "User~DESC" },
            });
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserModel[]> { Query = query, Result = result });
        }

        // GET: ElmahUser/AjaxMultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxMultiItems(ElmahUserAdvancedQuery query)
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

        [Route("[controller]/[action]/{User}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: ElmahUser/Dashboard/{User}
        public async Task<IActionResult> Dashboard([FromRoute]ElmahUserIdentifier id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }

        [Route("[controller]/[action]/{User}")] // Primary
        // GET: ElmahUser/AjaxLoadItem/{User}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            ElmahUserIdentifier id)
        {
            ElmahUserModel? result;
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
                    new ItemViewModel<ElmahUserModel>
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
                    new ItemViewModel<ElmahUserModel>
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
        // POST: ElmahUser/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [Bind("User")] ElmahUserModel input)
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
                                new Tuple<string, object>("~/Views/ElmahUser/_ListItemTr.cshtml",
                                    new ItemViewModel<ElmahUserModel>{
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
                                    new Tuple<string, object>("~/Views/ElmahUser/_Tile.cshtml",
                                        new ItemViewModel<ElmahUserModel>
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

        [Route("[controller]/[action]/{User}")] // Primary
        [HttpPost, ActionName("AjaxDelete")]
        // POST: ElmahUser/AjaxDelete/{User}
        public async Task<IActionResult> AjaxDelete(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [FromRoute] ElmahUserIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{User}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: ElmahUser/AjaxEdit/{User}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ElmahUserIdentifier id,
            [Bind("User")] ElmahUserModel input)
        {
            if (string.IsNullOrEmpty(id.User) ||
                !string.IsNullOrEmpty(id.User) && id.User != input.User)
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
                                new Tuple<string, object>("~/Views/ElmahUser/_ListDetailsItem.cshtml",
                                    new ItemViewModel<ElmahUserModel>
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
                                new Tuple<string, object>("~/Views/ElmahUser/_TileDetailsItem.cshtml",
                                    new ItemViewModel<ElmahUserModel>
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

        // POST: ElmahUser//AjaxBulkDelete
        [HttpPost, ActionName("AjaxBulkDelete")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxBulkDelete(
            [FromForm] BatchActionViewModel<ElmahUserIdentifier> data)
        {
            var result = await _thisService.BulkDelete(data.Ids);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("[controller]/[action]/{User}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: ElmahUser/Edit/{User}
        public async Task<IActionResult> Edit([FromRoute]ElmahUserIdentifier id)
        {
            if (string.IsNullOrEmpty(id.User))
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

        [Route("[controller]/[action]/{User}")] // Primary
        // POST: ElmahUser/Edit/{User}
        public async Task<IActionResult> Edit(
            [FromRoute]ElmahUserIdentifier id,
            [Bind("User")] ElmahUserModel input)
        {
            if (string.IsNullOrEmpty(id.User) ||
                !string.IsNullOrEmpty(id.User) && id.User != input.User)
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

        [Route("[controller]/[action]/{User}")] // Primary
        // GET: ElmahUser/Details/{User}
        public async Task<IActionResult> Details([FromRoute]ElmahUserIdentifier id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        // GET: ElmahUser/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Status = System.Net.HttpStatusCode.OK;

            return View();
        }

        // POST: ElmahUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("User")] ElmahUserModel input)
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

        [Route("[controller]/[action]/{User}")] // Primary
        // GET: ElmahUser/Delete/{User}
        public async Task<IActionResult> Delete([FromRoute]ElmahUserIdentifier id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{User}")] // Primary
        // POST: ElmahUser/Delete/{User}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahUserIdentifier id)
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


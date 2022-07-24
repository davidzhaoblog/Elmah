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
    public class ElmahErrorController : Controller
    {
        private readonly IElmahErrorService _thisService;

        private readonly IElmahApplicationService _elmahApplicationService;
        private readonly IElmahHostService _elmahHostService;
        private readonly IElmahSourceService _elmahSourceService;
        private readonly IElmahStatusCodeService _elmahStatusCodeService;
        private readonly IElmahTypeService _elmahTypeService;
        private readonly IElmahUserService _elmahUserService;
        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahErrorController> _logger;

        public ElmahErrorController(
            IElmahErrorService thisService,

            IElmahApplicationService elmahApplicationService,
            IElmahHostService elmahHostService,
            IElmahSourceService elmahSourceService,
            IElmahStatusCodeService elmahStatusCodeService,
            IElmahTypeService elmahTypeService,
            IElmahUserService elmahUserService,
            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahErrorController> logger)
        {
            _thisService = thisService;

            _elmahApplicationService = elmahApplicationService;
            _elmahHostService = elmahHostService;
            _elmahSourceService = elmahSourceService;
            _elmahStatusCodeService = elmahStatusCodeService;
            _elmahTypeService = elmahTypeService;
            _elmahUserService = elmahUserService;
            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahError
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahErrorAdvancedQuery query)
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
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("TimeUtc")), Value = "TimeUtc~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("TimeUtc")), Value = "TimeUtc~DESC" },
            });
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            ViewBag.TimeUtcRangeList = _selectListHelper.GetDefaultPredefinedDateTimeRange();

            await LoadIndexViewTopLevelSelectLists();
            return View(new PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorModel.DefaultView[]> { Query = query, Result = result });
        }

        // GET: ElmahError/AjaxMultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxMultiItems(ElmahErrorAdvancedQuery query)
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

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // GET: ElmahError/AjaxLoadItem/{ErrorId}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
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
                ViewBag.Status = response.Status;
                ViewBag.StatusMessage = response.StatusMessage;
            }

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplateNames.Edit.ToString() || template == ViewItemTemplateNames.Create.ToString())
            {
                await LoadIndexViewTopLevelSelectLists();
            }

            ViewBag.Template = template;
            if (view == PagedViewOptions.List && container == CrudViewContainers.Inline)
            {
                // By Default: _List{template}Item.cshtml
                // Developer can customize template name
                ViewBag.Template = template;
                return PartialView($"_List{template}Item", result);
            }
            if (view == PagedViewOptions.Tiles && container == CrudViewContainers.Inline)
            {
                // By Default: _List{template}Item.cshtml
                // Developer can customize template name
                return PartialView($"_Tile{template}Item",
                    new ItemViewModel<ElmahErrorModel.DefaultView>
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
                    if (view == PagedViewOptions.List) // Html Table
                    {
                        return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                            new AjaxResponseViewModel
                            {
                                Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                                PartialViews = new List<Tuple<string, object>> {
                                new Tuple<string, object>("~/Views/ElmahError/_ListItemTr.cshtml",
                                    new ItemViewModel<ElmahErrorModel.DefaultView>{
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
                                    new Tuple<string, object>("~/Views/_Tile_cshtmlElmahError.cshtml",
                                        new ItemViewModel<ElmahErrorModel.DefaultView>
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
                    return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                    {
                        Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                        PartialViews = new List<Tuple<string, object>> {
                            new Tuple<string, object>("~/Views/ElmahError/_ListDetailsItem.cshtml", result.ResponseBody!)
                        }
                    });
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

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        //[HttpGet, ActionName("Edit")]
        // GET: ElmahError/Edit/{ErrorId}
        public async Task<IActionResult> Edit([FromRoute]ElmahErrorIdentifier id)
        {
            if (!id.ErrorId.HasValue)
            {
                ViewBag.Status = System.Net.HttpStatusCode.NotFound;
                ViewBag.StatusMessage = "Not Found";
                return View();
            }

            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            await LoadIndexViewTopLevelSelectLists();
            return View(result.ResponseBody);
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
            if (!id.ErrorId.HasValue ||
                id.ErrorId.HasValue && id.ErrorId != input.ErrorId)
            {
                ViewBag.Status = System.Net.HttpStatusCode.NotFound;
                ViewBag.StatusMessage = "Not Found";
                await LoadIndexViewTopLevelSelectLists();
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

            await LoadIndexViewTopLevelSelectLists();
            return View(input);
        }

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // GET: ElmahError/Details/{ErrorId}
        public async Task<IActionResult> Details([FromRoute]ElmahErrorIdentifier id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        // GET: ElmahError/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Status = System.Net.HttpStatusCode.OK;
            await LoadIndexViewTopLevelSelectLists();
            return View();
        }

        // POST: ElmahError/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,AllXml")] ElmahErrorModel input)
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

            await LoadIndexViewTopLevelSelectLists();
            return View(input);
        }

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // GET: ElmahError/Delete/{ErrorId}
        public async Task<IActionResult> Delete([FromRoute]ElmahErrorIdentifier id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Route("[controller]/[action]/{ErrorId}")] // Primary
        // POST: ElmahError/Delete/{ErrorId}
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahErrorIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;

            var result1 = await _thisService.Get(id);
            return View(result1.ResponseBody);
        }

        private async Task LoadIndexViewTopLevelSelectLists()
        {

            var applicationList = await _elmahApplicationService.GetCodeList(new ElmahApplicationAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (applicationList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.ApplicationList = new SelectList(applicationList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var hostList = await _elmahHostService.GetCodeList(new ElmahHostAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (hostList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.HostList = new SelectList(hostList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var sourceList = await _elmahSourceService.GetCodeList(new ElmahSourceAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (sourceList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.SourceList = new SelectList(sourceList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var statusCodeList = await _elmahStatusCodeService.GetCodeList(new ElmahStatusCodeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (statusCodeList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.StatusCodeList = new SelectList(statusCodeList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var typeList = await _elmahTypeService.GetCodeList(new ElmahTypeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (typeList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.TypeList = new SelectList(typeList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var userList = await _elmahUserService.GetCodeList(new ElmahUserAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (userList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.UserList = new SelectList(userList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));
        }

        private async Task LoadSingleItemViewTopLevelSelectLists()
        {

            var applicationList = await _elmahApplicationService.GetCodeList(new ElmahApplicationAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (applicationList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.ApplicationList = new SelectList(applicationList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var hostList = await _elmahHostService.GetCodeList(new ElmahHostAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (hostList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.HostList = new SelectList(hostList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var typeList = await _elmahTypeService.GetCodeList(new ElmahTypeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (typeList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.TypeList = new SelectList(typeList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var sourceList = await _elmahSourceService.GetCodeList(new ElmahSourceAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (sourceList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.SourceList = new SelectList(sourceList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var userList = await _elmahUserService.GetCodeList(new ElmahUserAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (userList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.UserList = new SelectList(userList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));

            var statusCodeList = await _elmahStatusCodeService.GetCodeList(new ElmahStatusCodeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (statusCodeList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.StatusCodeList = new SelectList(statusCodeList.ResponseBody, nameof(NameValuePair.Value), nameof(NameValuePair.Name));
        }

    }
}


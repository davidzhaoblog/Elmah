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
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("TimeUtc")), Value = "TimeUtc~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("TimeUtc")), Value = "TimeUtc~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
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

        // GET: ElmahError/AjaxLoadItem/{ErrorId}
        [HttpGet, ActionName("AjaxLoadItem")]
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            ElmahErrorIdModel id)
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
                return PartialView($"_List{template}Item", result);
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
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence,AllXml")] ElmahErrorModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: ElmahError/AjaxDelete/{ErrorId}
        [HttpPost, ActionName("AjaxDelete")]
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> AjaxDelete(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [FromRoute] ElmahErrorIdModel id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: ElmahError/AjaxEdit/{ErrorId}
        [HttpPost, ActionName("AjaxEdit")]
        //[ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ElmahErrorIdModel id,
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence,AllXml")] ElmahErrorModel input)
        {
            if (id.ErrorId != input.ErrorId)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.NotFound, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                    return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ShowRequestId = false });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: ElmahError/Edit/{ErrorId}
        //[HttpGet, ActionName("Edit")]
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahErrorIdModel id)
        {
            if (id == null)
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

        // POST: ElmahError/Edit/{ErrorId}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> Edit(
            [FromRoute]ElmahErrorIdModel id,
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence,AllXml")] ElmahErrorModel input)
        {
            if (id.ErrorId != input.ErrorId)
            {
                ViewBag.Status = System.Net.HttpStatusCode.NotFound;
                ViewBag.StatusMessage = "Not Found";
                await LoadIndexViewTopLevelSelectLists();
                return View(input);
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
                ViewBag.Status = result.Status;
                ViewBag.StatusMessage = result.StatusMessage;
            }

            await LoadIndexViewTopLevelSelectLists();
            return View(input);
        }

        // GET: ElmahError/Details/{ErrorId}
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> Details([FromRoute]ElmahErrorIdModel id)
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
            [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence,AllXml")] ElmahErrorModel input)
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

        // GET: ElmahError/Delete/{ErrorId}
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> Delete([FromRoute]ElmahErrorIdModel id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        // POST: ElmahError/Delete/{ErrorId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahErrorIdModel id)
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


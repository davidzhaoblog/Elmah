using Elmah.MvcWebApp.Models;
using Framework.Models;
using Elmah.Models;
using Elmah.Resx;
using Elmah.ServiceContracts;
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

            ViewBag.TimeUtcRangeList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Custom"), Value = "Custom" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastFiveYears"), Value = "LastFiveYears" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastYear"), Value = "LastYear" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastSixMonths"), Value = "LastSixMonths" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastThreeMonths"), Value = "LastThreeMonths" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastMonth"), Value = "LastMonth" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastWeek"), Value = "LastWeek" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Yesterday"), Value = "Yesterday" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Today"), Value = "Today" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Tomorrow"), Value = "Tomorrow" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_ThisWeek"), Value = "ThisWeek" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_ThisMonth"), Value = "ThisMonth" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_ThisYear"), Value = "ThisYear" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextWeek"), Value = "NextWeek" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextMonth"), Value = "NextMonth" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextThreeMonths"), Value = "NextThreeMonths" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextSixMonths"), Value = "NextSixMonths" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextYear"), Value = "NextYear" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextFiveYears"), Value = "NextFiveYears" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextTenYears"), Value = "NextTenYears" },
            });

            var elmahApplicationList = await _elmahApplicationService.GetCodeList(new ElmahApplicationAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (elmahApplicationList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.ElmahApplicationList = new SelectList(elmahApplicationList.ResponseBody, nameof(NameValuePair.Name), nameof(NameValuePair.Value));

            var elmahHostList = await _elmahHostService.GetCodeList(new ElmahHostAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (elmahHostList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.ElmahHostList = new SelectList(elmahHostList.ResponseBody, nameof(NameValuePair.Name), nameof(NameValuePair.Value));

            var elmahSourceList = await _elmahSourceService.GetCodeList(new ElmahSourceAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (elmahSourceList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.ElmahSourceList = new SelectList(elmahSourceList.ResponseBody, nameof(NameValuePair.Name), nameof(NameValuePair.Value));

            var elmahStatusCodeList = await _elmahStatusCodeService.GetCodeList(new ElmahStatusCodeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (elmahStatusCodeList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.ElmahStatusCodeList = new SelectList(elmahStatusCodeList.ResponseBody, nameof(NameValuePair.Name), nameof(NameValuePair.Value));

            var elmahTypeList = await _elmahTypeService.GetCodeList(new ElmahTypeAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (elmahTypeList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.ElmahTypeList = new SelectList(elmahTypeList.ResponseBody, nameof(NameValuePair.Name), nameof(NameValuePair.Value));

            var elmahUserList = await _elmahUserService.GetCodeList(new ElmahUserAdvancedQuery { PageIndex = 1, PageSize = 10000 });
            if (elmahUserList.Status == System.Net.HttpStatusCode.OK)
                ViewBag.ElmahUserList = new SelectList(elmahUserList.ResponseBody, nameof(NameValuePair.Name), nameof(NameValuePair.Value));

            return View(new PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorModel.DefaultView[]> { Query = query, Result = result });
        }

        // GET: ElmahError/Details/{ErrorId}
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> Details([FromRoute]ElmahErrorIdModel id)
        {
            var result = await _thisService.Get(id);
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // GET: ElmahError/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElmahError/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence,AllXml")] ElmahErrorModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahError/Edit/{ErrorId}
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahErrorIdModel id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _thisService.Get(id);
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // POST: ElmahError/Edit/{ErrorId}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahErrorIdModel id, [Bind("ErrorId,Application,Host,Type,Source,Message,User,StatusCode,TimeUtc,Sequence,AllXml")] ElmahErrorModel input)
        {
            if (id.ErrorId != input.ErrorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(input);
                if(result.Status != System.Net.HttpStatusCode.OK)
                    return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahError/Delete/{ErrorId}
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> Delete([FromRoute]ElmahErrorIdModel id)
        {
            var result = await _thisService.Get(id);
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahError/Delete/{ErrorId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{ErrorId}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahErrorIdModel id)
        {
            var result = await _thisService.Delete(id);
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


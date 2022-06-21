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
    public class ElmahStatusCodeController : Controller
    {
        private readonly IElmahStatusCodeService _thisService;

        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahStatusCodeController> _logger;

        public ElmahStatusCodeController(
            IElmahStatusCodeService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahStatusCodeController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahStatusCode
        public async Task<IActionResult> Index(ElmahStatusCodeAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Name")), Value = "Name~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Name")), Value = "Name~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeModel[]> { Query = query, Result = result });
        }

        // GET: ElmahStatusCode/Dashboard/{StatusCode}
        [Route("[controller]/[action]/{StatusCode}")]
        public async Task<IActionResult> Dashboard([FromRoute]ElmahStatusCodeIdModel id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }

        // GET: ElmahStatusCode/Details/{StatusCode}
        [Route("[controller]/[action]/{StatusCode}")]
        public async Task<IActionResult> Details([FromRoute]ElmahStatusCodeIdModel id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        // GET: ElmahStatusCode/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Status = System.Net.HttpStatusCode.OK;

            return View();
        }

        // POST: ElmahStatusCode/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusCode,Name")] ElmahStatusCodeModel input)
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

        // GET: ElmahStatusCode/Edit/{StatusCode}
        [Route("[controller]/[action]/{StatusCode}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahStatusCodeIdModel id)
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

            return View(result.ResponseBody);
        }

        // POST: ElmahStatusCode/Edit/{StatusCode}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{StatusCode}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahStatusCodeIdModel id, [Bind("StatusCode,Name")] ElmahStatusCodeModel input)
        {
            if (id.StatusCode != input.StatusCode)
            {
                ViewBag.Status = System.Net.HttpStatusCode.NotFound;
                ViewBag.StatusMessage = "Not Found";

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

            return View(input);
        }

        // GET: ElmahStatusCode/Delete/{StatusCode}
        [Route("[controller]/[action]/{StatusCode}")]
        public async Task<IActionResult> Delete([FromRoute]ElmahStatusCodeIdModel id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        // POST: ElmahStatusCode/Delete/{StatusCode}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{StatusCode}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahStatusCodeIdModel id)
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


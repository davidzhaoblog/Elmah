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
    public class ElmahSourceController : Controller
    {
        private readonly IElmahSourceService _thisService;

        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahSourceController> _logger;

        public ElmahSourceController(
            IElmahSourceService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahSourceController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahSource
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(ElmahSourceAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Source")), Value = "Source~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Source")), Value = "Source~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceModel[]> { Query = query, Result = result });
        }

        // GET: ElmahSource/Dashboard/{Source}
        [Route("[controller]/[action]/{Source}")]
        public async Task<IActionResult> Dashboard([FromRoute]ElmahSourceIdModel id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }

        // GET: ElmahSource/Details/{Source}
        [Route("[controller]/[action]/{Source}")]
        public async Task<IActionResult> Details([FromRoute]ElmahSourceIdModel id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        // GET: ElmahSource/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Status = System.Net.HttpStatusCode.OK;

            return View();
        }

        // POST: ElmahSource/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Source")] ElmahSourceModel input)
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

        // GET: ElmahSource/Edit/{Source}
        [Route("[controller]/[action]/{Source}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahSourceIdModel id)
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

        // POST: ElmahSource/Edit/{Source}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{Source}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahSourceIdModel id, [Bind("Source")] ElmahSourceModel input)
        {
            if (id.Source != input.Source)
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

        // GET: ElmahSource/Delete/{Source}
        [Route("[controller]/[action]/{Source}")]
        public async Task<IActionResult> Delete([FromRoute]ElmahSourceIdModel id)
        {
            var result = await _thisService.Get(id);
            ViewBag.Status = result.Status;
            ViewBag.StatusMessage = result.StatusMessage;
            return View(result.ResponseBody);
        }

        // POST: ElmahSource/Delete/{Source}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{Source}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahSourceIdModel id)
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


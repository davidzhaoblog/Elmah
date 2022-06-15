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
    public class ElmahApplicationController : Controller
    {
        private readonly IElmahApplicationService _thisService;

        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahApplicationController> _logger;

        public ElmahApplicationController(
            IElmahApplicationService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahApplicationController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahApplication
        public async Task<IActionResult> Index(ElmahApplicationAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Application")), Value = "Application~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Application")), Value = "Application~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            return View(new PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationModel[]> { Query = query, Result = result });
        }

        // GET: ElmahApplication/Details/{Application}
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> Details([FromRoute]ElmahApplicationIdModel id)
        {
            var result = await _thisService.Get(id);
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // GET: ElmahApplication/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElmahApplication/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Application")] ElmahApplicationModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahApplication/Edit/{Application}
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahApplicationIdModel id)
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

        // POST: ElmahApplication/Edit/{Application}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahApplicationIdModel id, [Bind("Application")] ElmahApplicationModel input)
        {
            if (id.Application != input.Application)
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

        // GET: ElmahApplication/Delete/{Application}
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> Delete([FromRoute]ElmahApplicationIdModel id)
        {
            var result = await _thisService.Get(id);
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahApplication/Delete/{Application}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahApplicationIdModel id)
        {
            var result = await _thisService.Delete(id);
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


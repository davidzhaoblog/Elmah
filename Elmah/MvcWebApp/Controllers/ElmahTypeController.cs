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
        public async Task<IActionResult> Index(ElmahTypeAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Type")), Value = "Type~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Type")), Value = "Type~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            return View(new PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeModel[]> { Query = query, Result = result });
        }

        // GET: ElmahType/Details/{Type}
        [Route("[controller]/[action]/{Type}")]
        public async Task<IActionResult> Details([FromRoute]ElmahTypeIdModel id)
        {
            var result = await _thisService.Get(id);
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // GET: ElmahType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElmahType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type")] ElmahTypeModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahType/Edit/{Type}
        [Route("[controller]/[action]/{Type}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahTypeIdModel id)
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

        // POST: ElmahType/Edit/{Type}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{Type}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahTypeIdModel id, [Bind("Type")] ElmahTypeModel input)
        {
            if (id.Type != input.Type)
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

        // GET: ElmahType/Delete/{Type}
        [Route("[controller]/[action]/{Type}")]
        public async Task<IActionResult> Delete([FromRoute]ElmahTypeIdModel id)
        {
            var result = await _thisService.Get(id);
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahType/Delete/{Type}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{Type}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahTypeIdModel id)
        {
            var result = await _thisService.Delete(id);
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


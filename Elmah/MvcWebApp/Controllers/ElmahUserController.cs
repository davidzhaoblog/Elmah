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
        public async Task<IActionResult> Index(ElmahUserAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("User")), Value = "User~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("User")), Value = "User~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            return View(new PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserModel[]> { Query = query, Result = result });
        }

        // GET: ElmahUser/Details/{User}
        [Route("[controller]/[action]/{User}")]
        public async Task<IActionResult> Details([FromRoute]ElmahUserIdModel id)
        {
            var result = await _thisService.Get(id);
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // GET: ElmahUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElmahUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User")] ElmahUserModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahUser/Edit/{User}
        [Route("[controller]/[action]/{User}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahUserIdModel id)
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

        // POST: ElmahUser/Edit/{User}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{User}")]
        public async Task<IActionResult> Edit([FromRoute]ElmahUserIdModel id, [Bind("User")] ElmahUserModel input)
        {
            if (id.User != input.User)
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

        // GET: ElmahUser/Delete/{User}
        [Route("[controller]/[action]/{User}")]
        public async Task<IActionResult> Delete([FromRoute]ElmahUserIdModel id)
        {
            var result = await _thisService.Get(id);
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahUser/Delete/{User}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{User}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]ElmahUserIdModel id)
        {
            var result = await _thisService.Delete(id);
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


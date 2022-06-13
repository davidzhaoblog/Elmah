using Elmah.ServiceContracts;
using Elmah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elmah.MvcWebApp.Controllers
{
    public class ElmahTypeController : Controller
    {
        private readonly ILogger<ElmahTypeController> _logger;
        private readonly IElmahTypeService _thisService;

        public ElmahTypeController(IElmahTypeService thisService, ILogger<ElmahTypeController> logger)
        {
            _thisService = thisService;
            _logger = logger;
        }

        // GET: ElmahType
        public async Task<IActionResult> Index(ElmahTypeAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return View(result.ResponseBody);
        }

        // GET: ElmahType/Details/{type}
        public async Task<IActionResult> Details(string type)
        {
            var result = await _thisService.Get(new ElmahTypeIdModel { Type = type });
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
        public async Task<IActionResult> Create([Bind("Host,SpatialLocation____")] ElmahTypeModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahType/Edit/{type}
        public async Task<IActionResult> Edit(string type)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _thisService.Get(new ElmahTypeIdModel { Type = type });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // POST: ElmahType/Edit/{type}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string type, [Bind("Host,SpatialLocation____")] ElmahTypeModel input)
        {
            if (type != input.Type)
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

        // GET: ElmahType/Delete/{type}
        public async Task<IActionResult> Delete(string type)
        {
            var result = await _thisService.Get(new ElmahTypeIdModel { Type = type });
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahType/Delete/{type}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string type)
        {
            var result = await _thisService.Delete(new ElmahTypeIdModel { Type = type });
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


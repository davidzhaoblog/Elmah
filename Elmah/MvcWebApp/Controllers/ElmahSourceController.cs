using Elmah.ServiceContracts;
using Elmah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elmah.MvcWebApp.Controllers
{
    public class ElmahSourceController : Controller
    {
        private readonly ILogger<ElmahSourceController> _logger;
        private readonly IElmahSourceService _thisService;

        public ElmahSourceController(IElmahSourceService thisService, ILogger<ElmahSourceController> logger)
        {
            _thisService = thisService;
            _logger = logger;
        }

        // GET: ElmahSource
        public async Task<IActionResult> Index(ElmahSourceAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return View(result.ResponseBody);
        }

        // GET: ElmahSource/Details/{source}
        public async Task<IActionResult> Details(string source)
        {
            var result = await _thisService.Get(new ElmahSourceIdModel { Source = source });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // GET: ElmahSource/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElmahSource/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Host,SpatialLocation____")] ElmahSourceModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahSource/Edit/{source}
        public async Task<IActionResult> Edit(string source)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _thisService.Get(new ElmahSourceIdModel { Source = source });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // POST: ElmahSource/Edit/{source}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string source, [Bind("Host,SpatialLocation____")] ElmahSourceModel input)
        {
            if (source != input.Source)
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

        // GET: ElmahSource/Delete/{source}
        public async Task<IActionResult> Delete(string source)
        {
            var result = await _thisService.Get(new ElmahSourceIdModel { Source = source });
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahSource/Delete/{source}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string source)
        {
            var result = await _thisService.Delete(new ElmahSourceIdModel { Source = source });
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


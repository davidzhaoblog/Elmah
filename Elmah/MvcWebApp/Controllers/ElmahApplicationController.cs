using Elmah.ServiceContracts;
using Elmah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elmah.MvcWebApp.Controllers
{
    public class ElmahApplicationController : Controller
    {
        private readonly ILogger<ElmahApplicationController> _logger;
        private readonly IElmahApplicationService _thisService;

        public ElmahApplicationController(IElmahApplicationService thisService, ILogger<ElmahApplicationController> logger)
        {
            _thisService = thisService;
            _logger = logger;
        }

        // GET: ElmahApplication
        public async Task<IActionResult> Index(ElmahApplicationAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return View(result.ResponseBody);
        }

        // GET: ElmahApplication/Details/{application}
        public async Task<IActionResult> Details(string application)
        {
            var result = await _thisService.Get(new ElmahApplicationIdModel { Application = application });
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
        public async Task<IActionResult> Create([Bind("Host,SpatialLocation____")] ElmahApplicationModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahApplication/Edit/{application}
        public async Task<IActionResult> Edit(string application)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _thisService.Get(new ElmahApplicationIdModel { Application = application });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // POST: ElmahApplication/Edit/{application}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string application, [Bind("Host,SpatialLocation____")] ElmahApplicationModel input)
        {
            if (application != input.Application)
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

        // GET: ElmahApplication/Delete/{application}
        public async Task<IActionResult> Delete(string application)
        {
            var result = await _thisService.Get(new ElmahApplicationIdModel { Application = application });
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahApplication/Delete/{application}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string application)
        {
            var result = await _thisService.Delete(new ElmahApplicationIdModel { Application = application });
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


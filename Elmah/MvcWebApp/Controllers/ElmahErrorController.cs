using Elmah.ServiceContracts;
using Elmah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elmah.MvcWebApp.Controllers
{
    public class ElmahErrorController : Controller
    {
        private readonly ILogger<ElmahErrorController> _logger;
        private readonly IElmahErrorService _thisService;

        public ElmahErrorController(IElmahErrorService thisService, ILogger<ElmahErrorController> logger)
        {
            _thisService = thisService;
            _logger = logger;
        }

        // GET: ElmahError
        public async Task<IActionResult> Index(ElmahErrorAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return View(result.ResponseBody);
        }

        // GET: ElmahError/Details/{errorid}
        public async Task<IActionResult> Details(System.Guid errorid)
        {
            var result = await _thisService.Get(new ElmahErrorIdModel { ErrorId = errorid });
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
        public async Task<IActionResult> Create([Bind("Host,SpatialLocation____")] ElmahErrorModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahError/Edit/{errorid}
        public async Task<IActionResult> Edit(System.Guid errorid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _thisService.Get(new ElmahErrorIdModel { ErrorId = errorid });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // POST: ElmahError/Edit/{errorid}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(System.Guid errorid, [Bind("Host,SpatialLocation____")] ElmahErrorModel input)
        {
            if (errorid != input.ErrorId)
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

        // GET: ElmahError/Delete/{errorid}
        public async Task<IActionResult> Delete(System.Guid errorid)
        {
            var result = await _thisService.Get(new ElmahErrorIdModel { ErrorId = errorid });
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahError/Delete/{errorid}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(System.Guid errorid)
        {
            var result = await _thisService.Delete(new ElmahErrorIdModel { ErrorId = errorid });
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


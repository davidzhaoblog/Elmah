using Elmah.ServiceContracts;
using Elmah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elmah.MvcWebApp.Controllers
{
    public class ElmahHostController : Controller
    {
        private readonly ILogger<ElmahHostController> _logger;
        private readonly IElmahHostService _thisService;

        public ElmahHostController(IElmahHostService thisService, ILogger<ElmahHostController> logger)
        {
            _thisService = thisService;
            _logger = logger;
        }

        // GET: ElmahHost
        public async Task<IActionResult> Index(ElmahHostAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return View(result.ResponseBody);
        }

        // GET: ElmahHost/Details/{host}
        public async Task<IActionResult> Details(string host)
        {
            var result = await _thisService.Get(new ElmahHostIdModel { Host = host });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // GET: ElmahHost/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElmahHost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Host,SpatialLocation____")] ElmahHostModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahHost/Edit/{host}
        public async Task<IActionResult> Edit(string host)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _thisService.Get(new ElmahHostIdModel { Host = host });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // POST: ElmahHost/Edit/{host}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string host, [Bind("Host,SpatialLocation____")] ElmahHostModel input)
        {
            if (host != input.Host)
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

        // GET: ElmahHost/Delete/{host}
        public async Task<IActionResult> Delete(string host)
        {
            var result = await _thisService.Get(new ElmahHostIdModel { Host = host });
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahHost/Delete/{host}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string host)
        {
            var result = await _thisService.Delete(new ElmahHostIdModel { Host = host });
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


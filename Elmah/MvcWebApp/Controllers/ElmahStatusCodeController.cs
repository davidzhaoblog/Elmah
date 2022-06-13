using Elmah.ServiceContracts;
using Elmah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elmah.MvcWebApp.Controllers
{
    public class ElmahStatusCodeController : Controller
    {
        private readonly ILogger<ElmahStatusCodeController> _logger;
        private readonly IElmahStatusCodeService _thisService;

        public ElmahStatusCodeController(IElmahStatusCodeService thisService, ILogger<ElmahStatusCodeController> logger)
        {
            _thisService = thisService;
            _logger = logger;
        }

        // GET: ElmahStatusCode
        public async Task<IActionResult> Index(ElmahStatusCodeAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return View(result.ResponseBody);
        }

        // GET: ElmahStatusCode/Details/{statuscode}
        public async Task<IActionResult> Details(int statuscode)
        {
            var result = await _thisService.Get(new ElmahStatusCodeIdModel { StatusCode = statuscode });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // GET: ElmahStatusCode/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElmahStatusCode/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Host,SpatialLocation____")] ElmahStatusCodeModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: ElmahStatusCode/Edit/{statuscode}
        public async Task<IActionResult> Edit(int statuscode)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _thisService.Get(new ElmahStatusCodeIdModel { StatusCode = statuscode });
            if (result.Status != System.Net.HttpStatusCode.OK)
                return NotFound();
            return View(result.ResponseBody);
        }

        // POST: ElmahStatusCode/Edit/{statuscode}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int statuscode, [Bind("Host,SpatialLocation____")] ElmahStatusCodeModel input)
        {
            if (statuscode != input.StatusCode)
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

        // GET: ElmahStatusCode/Delete/{statuscode}
        public async Task<IActionResult> Delete(int statuscode)
        {
            var result = await _thisService.Get(new ElmahStatusCodeIdModel { StatusCode = statuscode });
            if (result.Status == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else if (result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);

            return View(result.ResponseBody);
        }

        // POST: ElmahStatusCode/Delete/{statuscode}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int statuscode)
        {
            var result = await _thisService.Delete(new ElmahStatusCodeIdModel { StatusCode = statuscode });
            if(result.Status != System.Net.HttpStatusCode.OK)
                return Problem(result.StatusMessage);
            return RedirectToAction(nameof(Index));
        }
    }
}


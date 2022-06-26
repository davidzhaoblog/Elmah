using Elmah.ServiceContracts;
using Elmah.MvcWebApp.Models;
using Elmah.Resx;
using Elmah.Models;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Elmah.MvcWebApp.Controllers
{
    public class ElmahHostController : Controller
    {
        private readonly IElmahHostService _thisService;

        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahHostController> _logger;

        public ElmahHostController(
            IElmahHostService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahHostController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahHost
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahHostAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Host")), Value = "Host~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Host")), Value = "Host~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostModel[]> { Query = query, Result = result });
        }

        // GET: ElmahHost/_MultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> _MultiItems(ElmahHostAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return PartialView("_List", result);
        }

        // GET: ElmahHost/Dashboard/{Host}
        [Route("[controller]/[action]/{Host}")]
        public async Task<IActionResult> Dashboard([FromRoute]ElmahHostIdModel id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }
    }
}


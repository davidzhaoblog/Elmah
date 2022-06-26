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
    public class ElmahStatusCodeController : Controller
    {
        private readonly IElmahStatusCodeService _thisService;

        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahStatusCodeController> _logger;

        public ElmahStatusCodeController(
            IElmahStatusCodeService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahStatusCodeController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahStatusCode
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahStatusCodeAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Name")), Value = "Name~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Name")), Value = "Name~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeModel[]> { Query = query, Result = result });
        }

        // GET: ElmahStatusCode/_MultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> _MultiItems(ElmahStatusCodeAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return PartialView("_List", result);
        }

        // GET: ElmahStatusCode/Dashboard/{StatusCode}
        [Route("[controller]/[action]/{StatusCode}")]
        public async Task<IActionResult> Dashboard([FromRoute]ElmahStatusCodeIdModel id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }
    }
}


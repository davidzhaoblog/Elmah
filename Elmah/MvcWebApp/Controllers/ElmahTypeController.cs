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
        [HttpGet] // from query string
        [HttpPost]// form post formdata
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

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeModel[]> { Query = query, Result = result });
        }

        // GET: ElmahType/_MultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> _MultiItems(ElmahTypeAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            return PartialView("_List", result);
        }

        // GET: ElmahType/Dashboard/{Type}
        [Route("[controller]/[action]/{Type}")]
        public async Task<IActionResult> Dashboard([FromRoute]ElmahTypeIdModel id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }    }
}


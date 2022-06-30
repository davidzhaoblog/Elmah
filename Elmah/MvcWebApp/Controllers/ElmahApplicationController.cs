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
    public class ElmahApplicationController : Controller
    {
        private readonly IElmahApplicationService _thisService;

        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahApplicationController> _logger;

        public ElmahApplicationController(
            IElmahApplicationService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahApplicationController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahApplication
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahApplicationAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Application")), Value = "Application~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Application")), Value = "Application~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationModel[]> { Query = query, Result = result });
        }

        // GET: ElmahApplication/_AjaxMultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> _AjaxMultiItems(ElmahApplicationAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            if (query.PagedViewOption == PagedViewOptions.List)
            {
                return PartialView("_List", result);
            }
            else if (query.PagedViewOption == PagedViewOptions.Tiles)
            {
                return PartialView("_Tiles", result);
            }
            return PartialView("_SlideShow", result);
        }

        // GET: ElmahApplication/_AjaxItem
        [HttpGet] // from query string or route
        [HttpPost]// form post formdata
        [Route("[controller]/[action]/{view}/{template}/{Application}")]
        public async Task<IActionResult> _AjaxItem(Framework.Models.PagedViewOptions view, Framework.Models.ViewItemTemplateNames template, Elmah.Models.ElmahApplicationIdModel id)
        {
            Elmah.Models.ElmahApplicationModel? result;
            if(template == ViewItemTemplateNames.NewItem)
            {
                result = _thisService.GetDefault();
                ViewBag.Status = System.Net.HttpStatusCode.OK;
            }
            else
            {
                var response = await _thisService.Get(id);
                result = response.ResponseBody;
                ViewBag.Status = response.Status;
                ViewBag.StatusMessage = response.StatusMessage;
            }
            ViewBag.Template = template;
            if (view == PagedViewOptions.Single)
            {
                return PartialView("_SingleItemTemplates", result);
            }
            return PartialView("_MultiItemTemplates", result);
        }

        // GET: ElmahApplication/Dashboard/{Application}
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> Dashboard([FromRoute]ElmahApplicationIdModel id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }
    }
}

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

        // GET: ElmahApplication/AjaxMultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxMultiItems(ElmahApplicationAdvancedQuery query)
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

        // GET: ElmahApplication/Dashboard/{Application}
        [HttpGet, ActionName("Dashboard")]
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> Dashboard([FromRoute]ElmahApplicationIdentifier id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }

        // GET: ElmahApplication/AjaxLoadItem/{Application}
        [HttpGet, ActionName("AjaxLoadItem")]
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            ElmahApplicationIdentifier id)
        {
            ElmahApplicationModel? result;
            if (template == ViewItemTemplateNames.Create.ToString())
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

            // TODO: Maybe some special for Edit/Create
            if (template == ViewItemTemplateNames.Edit.ToString() || template == ViewItemTemplateNames.Create.ToString())
            {

            }

            ViewBag.Template = template;
            if (view == PagedViewOptions.List && container == CrudViewContainers.Inline)
            {
                // By Default: _List{template}Item.cshtml
                // Developer can customize template name
                return PartialView($"_List{template}Item", result);
            }
            // By Default: _{template}.cshtml
            // Developer can customize template name
            return PartialView($"_{template}", result);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: ElmahApplication/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [Bind("Application")] ElmahApplicationModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);

                if (result.Status == System.Net.HttpStatusCode.OK)
                    return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: ElmahApplication/AjaxDelete/{Application}
        [HttpPost, ActionName("AjaxDelete")]
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> AjaxDelete(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [FromRoute] ElmahApplicationIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: ElmahApplication/AjaxEdit/{Application}
        [HttpPost, ActionName("AjaxEdit")]
        //[ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{Application}")]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ElmahApplicationIdentifier id,
            [Bind("Application")] ElmahApplicationModel input)
        {
            if (id.Application != input.Application)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.NotFound, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                    return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ShowRequestId = false });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}


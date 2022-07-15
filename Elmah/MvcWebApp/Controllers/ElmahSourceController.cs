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
    public class ElmahSourceController : Controller
    {
        private readonly IElmahSourceService _thisService;

        private readonly SelectListHelper _selectListHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ElmahSourceController> _logger;

        public ElmahSourceController(
            IElmahSourceService thisService,

            SelectListHelper selectListHelper,
            IUIStrings localizor,
            ILogger<ElmahSourceController> logger)
        {
            _thisService = thisService;

            _selectListHelper = selectListHelper;
            _localizor = localizor;
            _logger = logger;
        }

        // GET: ElmahSource
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> Index(ElmahSourceAdvancedQuery query)
        {
            var result = await _thisService.Search(query);
            ViewBag.PageSizeList = _selectListHelper.GetDefaultPageSizeList();

            ViewBag.OrderByList = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = String.Format("{0} A-Z", _localizor.Get("Source")), Value = "Source~ASC" },
                new SelectListItem{ Text = String.Format("{0} Z-A", _localizor.Get("Source")), Value = "Source~DESC" },
            });
            if(string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = ((List<SelectListItem>)ViewBag.OrderByList).First().Value;
            }

            ViewBag.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

            return View(new PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceModel[]> { Query = query, Result = result });
        }

        // GET: ElmahSource/AjaxMultiItems
        [HttpGet] // from query string
        [HttpPost]// form post formdata
        public async Task<IActionResult> AjaxMultiItems(ElmahSourceAdvancedQuery query)
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

        [Route("[controller]/[action]/{Source}")] // Primary
        [HttpGet, ActionName("Dashboard")]
        // GET: ElmahSource/Dashboard/{Source}
        public async Task<IActionResult> Dashboard([FromRoute]ElmahSourceIdentifier id)
        {
            var result = await _thisService.GetCompositeModel(id);
            return View(result);
        }

        [Route("[controller]/[action]/{Source}")] // Primary
        // GET: ElmahSource/AjaxLoadItem/{Source}
        [HttpGet, ActionName("AjaxLoadItem")]
        public async Task<IActionResult> AjaxLoadItem(
            PagedViewOptions view,
            CrudViewContainers container,
            string template,
            ElmahSourceIdentifier id)
        {
            ElmahSourceModel? result;
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
        // POST: ElmahSource/AjaxCreate
        [HttpPost, ActionName("AjaxCreate")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AjaxCreate(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [Bind("Source")] ElmahSourceModel input)
        {
            if (ModelState.IsValid)
            {
                var result = await _thisService.Create(input);

                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    return PartialView("~/Views/Shared/_AjaxResponse.cshtml",
                        new AjaxResponseViewModel
                        {
                            Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            PartialViews = new List<Tuple<string, object>> {
                            new Tuple<string, object>("~/Views/ElmahSource/_ListItemTr.cshtml",
                                new ListItemTrViewModal<ElmahSourceModel>{
                                    Template = ViewItemTemplateNames.Details.ToString(),
                                    IsCurrentItem = true,
                                    Model = result.ResponseBody!
                                })
                        }});
                }
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("[controller]/[action]/{Source}")] // Primary
        [HttpPost, ActionName("AjaxDelete")]
        // POST: ElmahSource/AjaxDelete/{Source}
        public async Task<IActionResult> AjaxDelete(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            [FromRoute] ElmahSourceIdentifier id)
        {
            var result = await _thisService.Delete(id);
            if (result.Status == System.Net.HttpStatusCode.OK)
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("[controller]/[action]/{Source}")] // Primary
        [HttpPost, ActionName("AjaxEdit")]
        // POST: ElmahSource/AjaxEdit/{Source}
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxEdit(
            PagedViewOptions view,
            CrudViewContainers container,
            ViewItemTemplateNames template,
            ElmahSourceIdentifier id,
            [Bind("Source")] ElmahSourceModel input)
        {
            if (string.IsNullOrEmpty(id.Source) ||
                !string.IsNullOrEmpty(id.Source) && id.Source != input.Source)
            {
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel {
                    Status = System.Net.HttpStatusCode.NotFound, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            if (ModelState.IsValid)
            {
                var result = await _thisService.Update(id, input);
                if (result.Status == System.Net.HttpStatusCode.OK)
                    return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel
                    {
                        Status = System.Net.HttpStatusCode.OK, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                        PartialViews = new List<Tuple<string, object>> {
                            new Tuple<string, object>("~/Views/ElmahSource/_ListDetailsItem.cshtml", result.ResponseBody!)
                        }
                    });
                return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel {
                    Status = result.Status, Message = result.StatusMessage, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ShowRequestId = false });
            }

            return PartialView("~/Views/Shared/_AjaxResponse.cshtml", new AjaxResponseViewModel { Status = System.Net.HttpStatusCode.BadRequest, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}


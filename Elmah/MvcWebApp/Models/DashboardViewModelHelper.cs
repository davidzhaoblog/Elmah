using Elmah.ServiceContracts;
using Framework.Models;
using Elmah.Models;
using Elmah.Resx;
namespace Elmah.MvcWebApp.Models
{
    public class DashboardViewModelHelper
    {
        private readonly IDropDownListService _dropDownListService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<DashboardViewModelHelper> _logger;

        public DashboardViewModelHelper(
            IDropDownListService dropDownListService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeaturesManager,
            IUIStrings localizor,
            ILogger<DashboardViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }
    }
}


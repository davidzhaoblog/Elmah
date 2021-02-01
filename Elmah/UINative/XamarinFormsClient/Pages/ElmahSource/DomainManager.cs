using System.Windows.Input;
using Xamarin.Forms;

namespace Elmah.XamarinForms.Pages.ElmahSource
{
    public class DomainManager : Framework.Xamariner.Interfaces.IDomainManager
    {
        public const string DomainKey = "ElmahSource";
        /// <summary>
        /// will be called in AppVM.Initialize(), Step #6. Register Domains
        /// </summary>
        public void RegisterViewModels()
        {
            //Register Extra View Models
            //DependencyService.Register< ViewModelTypeFullName >();
        }

        /// <summary>
        /// will be called in AppVM.Initialize(), Step #6. Register Domains,
        /// 1. add DomainRegistrationModel to AppVM.DomainRegistrationModels
        /// 2. then DomainRegistrationModel.Routes will be added AppShell.Routes
        /// </summary>
        /// <returns></returns>
        public Framework.Xaml.DomainModel CreateDomainModel()
        {
            var domainModel = Framework.Xaml.DomainModel.Create(DomainKey, null, null, null);

            //domainModel.AddRouteWithDomanKey(RouteKey.Demo.ToString(), typeof( DemoPage TypeFullName ), false);

            // 1. Create/Delete/Details/Edit UIViews
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.Create.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahSource.Create), false);
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.Delete.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahSource.Delete), false);
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.Details.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahSource.Details), false);
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.Edit.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahSource.Edit), false);

            // 2. SingleItemForm/SingleItemAggregateForm

            // 3. TabSearchResult/Results/Search/SearchResult

            //// 03.01. CommonSearchView -> Elmah.XamarinForms.Pages.ElmahSource.CommonSearchView
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.CommonSearchView.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahSource.CommonSearchView), false);

            // 03.02. CommonResultView -> Elmah.XamarinForms.Pages.ElmahSource.CommonResultView
            domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.CommonResultView.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahSource.CommonResultView), false);

            // 4. TabFullDetails/FullDetails/FunctionDashboard

            // 5. MasterViewAsideKeyInformation

            return domainModel;
        }
    }
}


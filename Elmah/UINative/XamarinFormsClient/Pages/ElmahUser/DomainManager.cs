using System.Windows.Input;
using Xamarin.Forms;

namespace Elmah.XamarinForms.Pages.ElmahUser
{
    public class DomainManager : Framework.Xamariner.Interfaces.IDomainManager
    {
        public const string DomainKey = "ElmahUser";
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
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.Create.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahUser.Create), false);
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.Delete.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahUser.Delete), false);
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.Details.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahUser.Details), false);
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.Edit.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahUser.Edit), false);

            // 2. SingleItemForm/SingleItemAggregateForm

            // 3. TabSearchResult/Results/Search/SearchResult

            //// 03.01. CommonSearchView -> Elmah.XamarinForms.Pages.ElmahUser.CommonSearchView
            //domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.CommonSearchView.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahUser.CommonSearchView), false);

            // 03.02. CommonResultView -> Elmah.XamarinForms.Pages.ElmahUser.CommonResultView
            domainModel.AddRelativeRoute(Framework.Xaml.StandardRouteRelativeKey.CommonResultView.ToString(), typeof(Elmah.XamarinForms.Pages.ElmahUser.CommonResultView), false);

            // 4. TabFullDetails/FullDetails/FunctionDashboard

            // 5. MasterViewAsideKeyInformation

            return domainModel;
        }
    }
}


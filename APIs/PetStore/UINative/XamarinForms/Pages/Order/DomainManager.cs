using System.Windows.Input;
using Xamarin.Forms;

namespace Elmah.PetStore.XamarinForms.Pages.Order
{
    public class DomainManager : Framework.Xamariner.Interfaces.IDomainManager
    {
        public const string DomainKey = "PetStore_Order";

        /// <summary>
        /// will be called in AppVM.Initialize(), Step #6. Register Domains,
        /// 1. add DomainRegistrationModel to AppVM.DomainRegistrationModels
        /// 2. then DomainRegistrationModel.Routes will be added AppShell.Routes
        /// </summary>
        /// <returns></returns>
        public Framework.Xaml.DomainModel CreateDomainModel()
        {
            var domainModel = Framework.Xaml.DomainModel.Create(DomainKey, null, null, null);

            // 03.02. CommonResultView -> Elmah.XamarinForms.Pages.ELMAH_Error.CommonResultView
            domainModel.AddRelativeRoute(Elmah.PetStore.ViewModels.NavigationVM.OrderActions.AddPet.ToString(), typeof(Elmah.PetStore.XamarinForms.Pages.Order.ListPage), false);

            return domainModel;
        }

        public void RegisterViewModels()
        {
        }
    }
}


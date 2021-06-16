using System.Windows.Input;
using Xamarin.Forms;

namespace Elmah.PetStore.XamarinForms.Pages.Pet
{
    public class DomainManager : Framework.Xamariner.Interfaces.IDomainManager
    {
        public const string DomainKey = "PetStore_Pet";

        /// <summary>
        /// will be called in AppVM.Initialize(), Step #6. Register Domains,
        /// 1. add DomainRegistrationModel to AppVM.DomainRegistrationModels
        /// 2. then DomainRegistrationModel.Routes will be added AppShell.Routes
        /// </summary>
        /// <returns></returns>
        public Framework.Xaml.DomainModel CreateDomainModel()
        {
            var domainModel = Framework.Xaml.DomainModel.Create(DomainKey, null, null, null);

            // Get.1. FindPetsByStatus - /pet/findByStatus
            domainModel.AddRelativeRoute(Elmah.PetStore.ViewModels.NavigationVM.PetActions.FindPetsByStatus.ToString(), typeof(Elmah.PetStore.XamarinForms.Pages.FindPetsByStatusPage), false);

            // Get.2. FindPetsByTags - /pet/findByTags
            domainModel.AddRelativeRoute(Elmah.PetStore.ViewModels.NavigationVM.PetActions.FindPetsByTags.ToString(), typeof(Elmah.PetStore.XamarinForms.Pages.FindPetsByTagsPage), false);

            return domainModel;
        }

        public void RegisterViewModels()
        {
        }
    }
}


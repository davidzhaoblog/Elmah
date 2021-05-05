using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {

        public UserVM UserVM
        {
            get
            {
                return DependencyService.Resolve<UserVM>();
            }
        }

        static partial void _RegisterViewModelsOfUser()
        {
            DependencyService.Register<UserVM>();
            DependencyService.Register<NavigationVM.UserContainer>();
        }
    }
}


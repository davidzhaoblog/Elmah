using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {

        static partial void _RegisterViewModelsOfUser()
        {

            DependencyService.Register<NavigationVM.UserContainer>();
        }
    }
}

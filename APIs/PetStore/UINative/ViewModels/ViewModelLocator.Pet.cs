using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {
        static partial void _RegisterViewModelsOfPet()
        {
            DependencyService.Register<Elmah.PetStore.ViewModels.PetVM>();
            DependencyService.Register<Elmah.PetStore.ViewModels.NavigationVM.PetContainer>();
        }
    }
}

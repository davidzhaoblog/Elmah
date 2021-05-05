using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {
        public PetVM PetVM
        {
            get
            {
                return DependencyService.Resolve<PetVM>();
            }
        }

        static partial void _RegisterViewModelsOfPet()
        {
            DependencyService.Register<Elmah.PetStore.ViewModels.PetVM>();
            DependencyService.Register<Elmah.PetStore.ViewModels.NavigationVM.PetContainer>();
        }
    }
}

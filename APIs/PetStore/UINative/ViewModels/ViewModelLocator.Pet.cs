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
            DependencyService.Register<PetVM>();
            DependencyService.Register<NavigationVM.PetContainer>();
        }
    }
}


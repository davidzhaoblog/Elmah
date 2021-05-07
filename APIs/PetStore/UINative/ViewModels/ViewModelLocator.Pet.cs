using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {

        public PetListVM PetListVM
        {
            get
            {
                return DependencyService.Resolve<PetListVM>();
            }
        }

        public PetItemVM PetItemVM
        {
            get
            {
                return DependencyService.Resolve<PetItemVM>();
            }
        }

        static partial void _RegisterViewModelsOfPet()
        {
            DependencyService.Register<PetListVM>();
            DependencyService.Register<PetItemVM>();
            DependencyService.Register<NavigationVM.PetContainer>();
        }
    }
}


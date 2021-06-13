using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {

        public FindPetsByStatusVM FindPetsByStatusVM
        {
            get
            {
                return DependencyService.Resolve<FindPetsByStatusVM>();
            }
        }

        public FindPetsByTagsVM FindPetsByTagsVM
        {
            get
            {
                return DependencyService.Resolve<FindPetsByTagsVM>();
            }
        }

        static partial void _RegisterViewModelsOfPet()
        {

            DependencyService.Register<FindPetsByStatusVM>();

            DependencyService.Register<FindPetsByTagsVM>();

            DependencyService.Register<NavigationVM.PetContainer>();
        }
    }
}


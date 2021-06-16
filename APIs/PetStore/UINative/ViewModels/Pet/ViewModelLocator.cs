using Elmah.PetStore.ViewModels.Pet;
using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {

        public Elmah.PetStore.ViewModels.Pet.FindPetsByStatusVM FindPetsByStatusVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.PetStore.ViewModels.Pet.FindPetsByStatusVM>();
            }
        }

        public Elmah.PetStore.ViewModels.Pet.FindPetsByTagsVM FindPetsByTagsVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.PetStore.ViewModels.Pet.FindPetsByTagsVM>();
            }
        }

        static partial void _RegisterViewModelsOfPet()
        {

            DependencyService.Register<Elmah.PetStore.ViewModels.Pet.FindPetsByStatusVM>();

            DependencyService.Register<Elmah.PetStore.ViewModels.Pet.FindPetsByTagsVM>();

            DependencyService.Register<NavigationVM.PetContainer>();
        }
    }
}


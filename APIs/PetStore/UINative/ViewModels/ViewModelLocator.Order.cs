using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {

        public OrderVM OrderVM
        {
            get
            {
                return DependencyService.Resolve<OrderVM>();
            }
        }

        static partial void _RegisterViewModelsOfOrder()
        {
            DependencyService.Register<OrderVM>();
            DependencyService.Register<NavigationVM.OrderContainer>();
        }
    }
}


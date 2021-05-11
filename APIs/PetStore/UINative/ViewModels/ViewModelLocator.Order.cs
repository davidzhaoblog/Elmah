using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {

        public OrderListVM OrderListVM
        {
            get
            {
                return DependencyService.Resolve<OrderListVM>();
            }
        }

        //public OrderItemVM OrderItemVM
        //{
        //    get
        //    {
        //        return DependencyService.Resolve<OrderItemVM>();
        //    }
        //}

        static partial void _RegisterViewModelsOfOrder()
        {
            DependencyService.Register<OrderListVM>();
            //DependencyService.Register<OrderItemVM>();
            DependencyService.Register<NavigationVM.OrderContainer>();
        }
    }
}


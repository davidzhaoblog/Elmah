using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class ViewModelLocator
    {

        public UserListVM UserListVM
        {
            get
            {
                return DependencyService.Resolve<UserListVM>();
            }
        }

        //public UserItemVM UserItemVM
        //{
        //    get
        //    {
        //        return DependencyService.Resolve<UserItemVM>();
        //    }
        //}

        static partial void _RegisterViewModelsOfUser()
        {
            DependencyService.Register<UserListVM>();
            //DependencyService.Register<UserItemVM>();
            DependencyService.Register<NavigationVM.UserContainer>();
        }
    }
}


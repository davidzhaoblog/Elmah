//using Elmah.MVVMLightViewModels.ElmahUser.Design;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class ViewModelLocator
    {

        // #1.2.1: Elmah.MVVMLightViewModels.ElmahUser.IndexVM
        public ElmahUser.IndexVM ElmahUser_IndexVM
        {
            get
            {
                return DependencyService.Resolve<ElmahUser.IndexVM>();
            }
        }

        // #1.2.: Elmah.MVVMLightViewModels.ElmahUser.ItemVM
        public ElmahUser.ItemVM ElmahUser_ItemVM
        {
            get
            {
                return DependencyService.Resolve<ElmahUser.ItemVM>();
            }
        }

        static partial void _RegisterViewModelsOfElmahUser()
        {

            // #1.1.1: Elmah.MVVMLightViewModels.ElmahUser.IndexVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahUser.IndexVM>();

            // #1.2. Elmah.MVVMLightViewModels.ElmahUser.ItemVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahUser.ItemVM>();

            DependencyService.Register<Elmah.MVVMLightViewModels.NavigationVM.ElmahUserContainer>();
        }
    }
}


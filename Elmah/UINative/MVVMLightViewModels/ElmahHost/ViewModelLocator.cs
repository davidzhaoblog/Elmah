//using Elmah.MVVMLightViewModels.ElmahHost.Design;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class ViewModelLocator
    {

        // #1.2.1: Elmah.MVVMLightViewModels.ElmahHost.IndexVM
        public ElmahHost.IndexVM ElmahHost_IndexVM
        {
            get
            {
                return DependencyService.Resolve<ElmahHost.IndexVM>();
            }
        }

        // #1.2.: Elmah.MVVMLightViewModels.ElmahHost.ItemVM
        public ElmahHost.ItemVM ElmahHost_ItemVM
        {
            get
            {
                return DependencyService.Resolve<ElmahHost.ItemVM>();
            }
        }

        static partial void _RegisterViewModelsOfElmahHost()
        {

            // #1.1.1: Elmah.MVVMLightViewModels.ElmahHost.IndexVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahHost.IndexVM>();

            // #1.2. Elmah.MVVMLightViewModels.ElmahHost.ItemVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahHost.ItemVM>();

            DependencyService.Register<Elmah.MVVMLightViewModels.NavigationVM.ElmahHostContainer>();
        }
    }
}


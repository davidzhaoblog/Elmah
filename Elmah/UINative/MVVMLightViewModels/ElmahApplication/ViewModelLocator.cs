//using Elmah.MVVMLightViewModels.ElmahApplication.Design;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class ViewModelLocator
    {

        // #1.2.1: Elmah.MVVMLightViewModels.ElmahApplication.IndexVM
        public ElmahApplication.IndexVM ElmahApplication_IndexVM
        {
            get
            {
                return DependencyService.Resolve<ElmahApplication.IndexVM>();
            }
        }

        // #1.2.: Elmah.MVVMLightViewModels.ElmahApplication.ItemVM
        public ElmahApplication.ItemVM ElmahApplication_ItemVM
        {
            get
            {
                return DependencyService.Resolve<ElmahApplication.ItemVM>();
            }
        }

        static partial void _RegisterViewModelsOfElmahApplication()
        {

            // #1.1.1: Elmah.MVVMLightViewModels.ElmahApplication.IndexVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahApplication.IndexVM>();

            // #1.2. Elmah.MVVMLightViewModels.ElmahApplication.ItemVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM>();

            DependencyService.Register<Elmah.MVVMLightViewModels.NavigationVM.ElmahApplicationContainer>();
        }
    }
}


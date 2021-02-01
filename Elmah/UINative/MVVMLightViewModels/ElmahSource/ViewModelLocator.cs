//using Elmah.MVVMLightViewModels.ElmahSource.Design;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class ViewModelLocator
    {

        // #1.2.1: Elmah.MVVMLightViewModels.ElmahSource.IndexVM
        public ElmahSource.IndexVM ElmahSource_IndexVM
        {
            get
            {
                return DependencyService.Resolve<ElmahSource.IndexVM>();
            }
        }

        // #1.2.: Elmah.MVVMLightViewModels.ElmahSource.ItemVM
        public ElmahSource.ItemVM ElmahSource_ItemVM
        {
            get
            {
                return DependencyService.Resolve<ElmahSource.ItemVM>();
            }
        }

        static partial void _RegisterViewModelsOfElmahSource()
        {

            // #1.1.1: Elmah.MVVMLightViewModels.ElmahSource.IndexVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahSource.IndexVM>();

            // #1.2. Elmah.MVVMLightViewModels.ElmahSource.ItemVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahSource.ItemVM>();

            DependencyService.Register<Elmah.MVVMLightViewModels.NavigationVM.ElmahSourceContainer>();
        }
    }
}


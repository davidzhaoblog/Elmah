//using Elmah.MVVMLightViewModels.ElmahStatusCode.Design;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class ViewModelLocator
    {

        // #1.2.1: Elmah.MVVMLightViewModels.ElmahStatusCode.IndexVM
        public ElmahStatusCode.IndexVM ElmahStatusCode_IndexVM
        {
            get
            {
                return DependencyService.Resolve<ElmahStatusCode.IndexVM>();
            }
        }

        // #1.2.: Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM
        public ElmahStatusCode.ItemVM ElmahStatusCode_ItemVM
        {
            get
            {
                return DependencyService.Resolve<ElmahStatusCode.ItemVM>();
            }
        }

        static partial void _RegisterViewModelsOfElmahStatusCode()
        {

            // #1.1.1: Elmah.MVVMLightViewModels.ElmahStatusCode.IndexVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahStatusCode.IndexVM>();

            // #1.2. Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM>();

            DependencyService.Register<Elmah.MVVMLightViewModels.NavigationVM.ElmahStatusCodeContainer>();
        }
    }
}


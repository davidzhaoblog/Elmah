//using Elmah.MVVMLightViewModels.ElmahType.Design;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class ViewModelLocator
    {

        // #1.2.1: Elmah.MVVMLightViewModels.ElmahType.IndexVM
        public ElmahType.IndexVM ElmahType_IndexVM
        {
            get
            {
                return DependencyService.Resolve<ElmahType.IndexVM>();
            }
        }

        // #1.2.: Elmah.MVVMLightViewModels.ElmahType.ItemVM
        public ElmahType.ItemVM ElmahType_ItemVM
        {
            get
            {
                return DependencyService.Resolve<ElmahType.ItemVM>();
            }
        }

        static partial void _RegisterViewModelsOfElmahType()
        {

            // #1.1.1: Elmah.MVVMLightViewModels.ElmahType.IndexVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahType.IndexVM>();

            // #1.2. Elmah.MVVMLightViewModels.ElmahType.ItemVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ElmahType.ItemVM>();

            DependencyService.Register<Elmah.MVVMLightViewModels.NavigationVM.ElmahTypeContainer>();
        }
    }
}


//using Elmah.MVVMLightViewModels.ELMAH_Error.Design;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class ViewModelLocator
    {

        // #1.2.1: Elmah.MVVMLightViewModels.ELMAH_Error.IndexVM
        public ELMAH_Error.IndexVM ELMAH_Error_IndexVM
        {
            get
            {
                return DependencyService.Resolve<ELMAH_Error.IndexVM>();
            }
        }

        // #1.2.: Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM
        public ELMAH_Error.ItemVM ELMAH_Error_ItemVM
        {
            get
            {
                return DependencyService.Resolve<ELMAH_Error.ItemVM>();
            }
        }

        // #3.1: Elmah.MVVMLightViewModels.ELMAH_Error.ExtendedVM
        public ELMAH_Error.ExtendedVM ELMAH_Error_ExtendedVM
        {
            get
            {
                return DependencyService.Resolve<ELMAH_Error.ExtendedVM>();
            }
        }

        static partial void _RegisterViewModelsOfELMAH_Error()
        {

            // #1.1.1: Elmah.MVVMLightViewModels.ELMAH_Error.IndexVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ELMAH_Error.IndexVM>();

            // #1.2. Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM>();

            // #3.1: Elmah.MVVMLightViewModels.ELMAH_Error.ExtendedVM
            DependencyService.Register<Elmah.MVVMLightViewModels.ELMAH_Error.ExtendedVM>();

            DependencyService.Register<Elmah.MVVMLightViewModels.NavigationVM.ELMAH_ErrorContainer>();
        }
    }
}


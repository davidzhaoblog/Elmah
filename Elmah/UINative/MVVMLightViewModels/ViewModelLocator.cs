/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Elmah.MVVMLightViewModels"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  OR (WPF only):

  xmlns:vm="clr-namespace:Elmah.MVVMLightViewModels"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/
//using Elmah.MVVMLightViewModels.Design;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Elmah.MVVMLightViewModels"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the Main property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:Elmah.MVVMLightViewModels"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public partial class ViewModelLocator
    {
        private const bool ForceDesignData = false;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view models
            ////}
            ////else
            ////{
            ////    // Create run time view models
            ////}
        }

        public MainViewModel Main
        {
            get
            {
                return DependencyService.Resolve<MainViewModel>();
            }
        }

        public SystemDashboardVM SystemDashboardVM
        {
            get
            {
                return DependencyService.Resolve<SystemDashboardVM>();
            }
        }

        public Framework.Xaml.PopupVM PopupVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.PopupVM>();
            }
        }

        public Framework.Xaml.ProgressBarVM ProgressBarVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.ProgressBarVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        public static void _RegisterViewModels()
        {
            DependencyService.Register<MainViewModel>();
            DependencyService.Register<SystemDashboardVM>();
            DependencyService.Register<Framework.Xaml.PopupVM>();
            DependencyService.Register<Framework.Xaml.ProgressBarVM>();

            // 1. ELMAH_Error
            _RegisterViewModelsOfELMAH_Error();

            // 2. ElmahApplication
            _RegisterViewModelsOfElmahApplication();

            // 3. ElmahHost
            _RegisterViewModelsOfElmahHost();

            // 4. ElmahSource
            _RegisterViewModelsOfElmahSource();

            // 5. ElmahStatusCode
            _RegisterViewModelsOfElmahStatusCode();

            // 6. ElmahType
            _RegisterViewModelsOfElmahType();

            // 7. ElmahUser
            _RegisterViewModelsOfElmahUser();

        }

        // 1. ELMAH_Error
        static partial void _RegisterViewModelsOfELMAH_Error();

        // 2. ElmahApplication
        static partial void _RegisterViewModelsOfElmahApplication();

        // 3. ElmahHost
        static partial void _RegisterViewModelsOfElmahHost();

        // 4. ElmahSource
        static partial void _RegisterViewModelsOfElmahSource();

        // 5. ElmahStatusCode
        static partial void _RegisterViewModelsOfElmahStatusCode();

        // 6. ElmahType
        static partial void _RegisterViewModelsOfElmahType();

        // 7. ElmahUser
        static partial void _RegisterViewModelsOfElmahUser();

    }
}


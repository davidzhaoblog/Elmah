using Xamarin.Forms;

namespace Elmah.XamarinForms.ViewModels
{
    public class ViewModelLocator
    {
        public Elmah.XamarinForms.ViewModels.AppVM AppVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        public Elmah.XamarinForms.ViewModels.AppLoadingVM AppLoadingVM
        {
            get { return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppLoadingVM>(DependencyFetchTarget.GlobalInstance); }
        }

        public Framework.Xaml.AppShellVM AppShellVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.AppShellVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        public Elmah.MVVMLightViewModels.NavigationVM NavigationVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        public Elmah.XamarinForms.ViewModels.DashboardVM DashboardVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.DashboardVM>();
            }
        }

        public Elmah.XamarinForms.ViewModels.CalendarVM CalendarVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.CalendarVM>();
            }
        }

        public Framework.Xaml.ImageCaptureVM ImageCropVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.ImageCaptureVM>();
            }
        }

        public ViewModelLocator()
        {
        }

        public static void RegisterViewModels()
        {
            DependencyService.Register<Elmah.MVVMLightViewModels.ListsHelper>();

            // 1.
            DependencyService.Register<Elmah.MVVMLightViewModels.NavigationVM, Elmah.XamarinForms.ViewModels.NavigationVM>();
            DependencyService.Register<Elmah.XamarinForms.ViewModels.AppLoadingVM>();
            DependencyService.Register<Framework.Xaml.AppShellVM>();
            DependencyService.Register<Elmah.XamarinForms.ViewModels.AppShellVM>();
            // Already registered in {SolutionName}.XamarinForms.App.xaml.cs
            //DependencyService.Register<Framework.Xaml.Themes.ThemeSelectorVM>();
            DependencyService.Register<Elmah.XamarinForms.ViewModels.LogInViewModel>();
            DependencyService.Register<Elmah.XamarinForms.ViewModels.RegisterViewModel>();
            DependencyService.Register<Elmah.XamarinForms.ViewModels.CalendarVM>();

            DependencyService.Register<Framework.Xaml.ImageCaptureVM>();

            // 2. Dashboard
            DependencyService.Register<Elmah.XamarinForms.ViewModels.DashboardVM>();
        }
    }
}


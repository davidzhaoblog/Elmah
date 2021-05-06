using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Elmah.XamarinForms
{
    /// <summary>
    /// Icons:
    /// Android: https://romannurik.github.io/AndroidAssetStudio/index.html
    /// Ios: https://appicon.co/
    /// </summary>
    public partial class App : Application {

        public Elmah.XamarinForms.ViewModels.AppVM AppVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppVM>();
            }
        }

        public Framework.Xaml.Themes.ThemeSelectorVM ThemeSelectorVM
        {
            get { return DependencyService.Resolve<Framework.Xaml.Themes.ThemeSelectorVM>(); }
        }

        public App()
        {
            Device.SetFlags(new string[] { "StateTriggers_Experimental", "SwipeView_Experimental" });

            // 1. Initial Theme -> LightTheme
            DependencyService.Register<Framework.Xaml.Themes.ThemeSelectorVM>();
            ThemeSelectorVM.Initialize(new Elmah.XamarinForms.Themes.LightTheme(), new Elmah.XamarinForms.Themes.DarkTheme());
            ThemeSelectorVM.SwitchTheme(Framework.Themes.Theme.Light);

            // 2. AppVM Initialize
            DependencyService.Register<Elmah.XamarinForms.ViewModels.AppVM>();

            // TODO: change hasAuthentication to true if you need login page
            const bool hasAuthentication = false;
            AppVM.Initialize(hasAuthentication);

            InitializeComponent();

            MainPage = new Elmah.XamarinForms.AppShell();
        }

        protected override async void OnStart()
        {
            await AppVM.OnStart();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}


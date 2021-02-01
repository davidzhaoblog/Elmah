using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Xamarin.Forms;

namespace Elmah.XamarinForms.ViewModels
{
    public class RegisterViewModel : Framework.Xaml.ViewModelBase
    {
        public Elmah.XamarinForms.ViewModels.AppLoadingVM AppLoadingVM
        {
            get { return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppLoadingVM>(DependencyFetchTarget.GlobalInstance); }
        }

        public Elmah.MVVMLightViewModels.NavigationVM NavigationVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>();
            }
        }

        protected Elmah.XamarinForms.ViewModels.DashboardVM DashboardVM
        {
            get
            {
                return DependencyService.Resolve< Elmah.XamarinForms.ViewModels.DashboardVM >();
            }
        }

        #region Properties
        string m_Email;
        [Required(ErrorMessageResourceName = "Common_EmailRequiredErrorMessage", ErrorMessageResourceType = typeof(Framework.Resx.UIStringResource))]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "Common_EmailFormatErrorMessage", ErrorMessageResourceType = typeof(Framework.Resx.UIStringResource))]
        public string Email
        {
            get
            {
                return m_Email;
            }
            set
            {
                ValidateProperty(value);
                Set(nameof(Email), ref m_Email, value);
            }
        }

        string m_Password;
        [Required(ErrorMessageResourceType = typeof(Framework.Resx.UIStringResource), ErrorMessageResourceName = "Common_PasswordRequiredErrorMessage")]
        [RegularExpression(@"^(?!.*([A-Za-z0-9])\1{1})(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessageResourceName = "Common_PasswordErrorMessage", ErrorMessageResourceType = typeof(Framework.Resx.UIStringResource))]
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                ValidateProperty(value);
                Set(nameof(Password), ref m_Password, value);
                if (!string.IsNullOrEmpty(ConfirmPassword))
                {
                    RaisePropertyChanged(nameof(ConfirmPassword));
                    ValidateProperty(ConfirmPassword, nameof(ConfirmPassword));
                }
            }
        }

        private string _ConfirmPassword;
        [Required(ErrorMessageResourceType = typeof(Framework.Resx.UIStringResource), ErrorMessageResourceName = "Common_PasswordRequiredErrorMessage")]
        [Compare(nameof(Password), ErrorMessageResourceName = "Common_ConfirmPasswordNotMatchErrorMessage", ErrorMessageResourceType = typeof(Framework.Resx.UIStringResource))]
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set {
                ValidateProperty(value);
                Set(nameof(ConfirmPassword), ref _ConfirmPassword, value);
                RaisePropertyChanged(nameof(Password));
                ValidateProperty(Password, nameof(Password));
            }
        }

        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { Set(nameof(Message), ref _Message, value); }
        }

        #endregion Properties

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var client = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateAuthenticationApiClient();

                    var loginResponse = await client.RegisterUserAsync(Email, Password, ConfirmPassword);

                    if (loginResponse.Succeeded)
                    {
                        loginResponse.LoggedInSource = Framework.WebApi.LoggedInSource.RegisterPage;

                        await Framework.Xaml.ApplicationPropertiesHelper.ClearSignInData();

                        App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.AppLoadingPage());
                        MessagingCenter.Send<Elmah.XamarinForms.ViewModels.AppLoadingVM, Framework.WebApi.AuthenticationResponse>(AppLoadingVM, Elmah.XamarinForms.ViewModels.AppLoadingVM.MessageTitle_LoadData, loginResponse);

                        Message = "Success :)";
                    }
                    else
                    {
                        Message = "Please try again :(";
                    }
                });
            }
        }
    }
}


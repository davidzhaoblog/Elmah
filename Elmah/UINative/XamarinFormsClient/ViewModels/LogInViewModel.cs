using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Elmah.XamarinForms.ViewModels
{
    public class LogInViewModel: Framework.Xaml.ViewModelBase
    {
        public Elmah.XamarinForms.ViewModels.AppVM AppVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppVM>();
            }
        }

        public Elmah.XamarinForms.ViewModels.AppLoadingVM AppLoadingVM
        {
            get { return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppLoadingVM>(DependencyFetchTarget.GlobalInstance); }
        }

        public Framework.Xaml.ProgressBarVM ProgressBarVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.ProgressBarVM>();
            }
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
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.DashboardVM>();
            }
        }

        #region properties

        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set { Set(nameof(IsVisible), ref _IsVisible, value); }
        }

        public bool FromLogInFromLogInPage { get; set; } = false;

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
            }
        }

        public bool EnableLogInButton
        {
            get
            {
                return !HasErrors;
            }
        }

        string m_ErrorMessage;
        public string ErrorMessage
        {
            get
            {
                return m_ErrorMessage;
            }
            set
            {
                m_ErrorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        private bool _RememberMe;
        public bool RememberMe
        {
            get { return _RememberMe; }
            set { Set(nameof(RememberMe), ref _RememberMe, value); }
        }

        private bool _AutoSignIn;
        public bool AutoSignIn
        {
            get { return _AutoSignIn; }
            set { Set(nameof(AutoSignIn), ref _AutoSignIn, value); }
        }

        Framework.WebApi.AuthenticationResponse m_LoginResponse = new Framework.WebApi.AuthenticationResponse();

        public Framework.WebApi.AuthenticationResponse LoginResponse
        {
            get { return m_LoginResponse; }
            set { Set(nameof(LoginResponse), ref m_LoginResponse, value); }
        }

        #endregion properties

        #region 1. Login Asp.Net Identity Command

        /// <summary>
        /// 1. Login Asp.Net Identity
        /// </summary>
        public ICommand LogInCommand => new Command(OnLogIn);

        private async void OnLogIn()
        {
            PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            this.FromLogInFromLogInPage = true;
            var client = new Elmah.WebApiClient.AuthenticationApiClient(Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl);

            LoginResponse = await client.LogInAsync(this.Email, this.Password);

            if (LoginResponse.Succeeded)
            {
                LoginResponse.LoggedInSource = Framework.WebApi.LoggedInSource.LogInPage;
                PopupVM.HidePopup();
                this.ErrorMessage = string.Empty;

                await Framework.Xaml.ApplicationPropertiesHelper.SetSignInData(
                    this.Email
                    , this.RememberMe ? this.Password : null
                    , this.RememberMe, this.AutoSignIn, LoginResponse.Token, LoginResponse.EntityID ?? 0, !LoginResponse.EntityID.HasValue, null);
                AppVM.SignInData = Framework.Xaml.ApplicationPropertiesHelper.GetSignInData();
                this.IsVisible = false;

                App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.AppLoadingPage());
                MessagingCenter.Send<Elmah.XamarinForms.ViewModels.AppLoadingVM, Framework.WebApi.AuthenticationResponse>(AppLoadingVM, Elmah.XamarinForms.ViewModels.AppLoadingVM.MessageTitle_LoadData, LoginResponse);
            }
            else
            {
                this.ErrorMessage = Framework.Resx.UIStringResource.Account_LogIn_FailureText;
                //PopupVM.HidePopup();
            }
        }

        #endregion 1. Login Asp.Net Identity Command

        #region 2. Google OAuth2 Login Command

        public ICommand GoogleLogInCommand => new Command(OnGoogleLogin);

        private async void OnGoogleLogin()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.iOSClientId;
                    redirectUri = Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AndroidClientId;
                    redirectUri = Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AndroidRedirectUrl;
                    break;
            }

            var account = await Framework.Xamariner.Helpers.SecureStorageAccountStoreHelper.FindAccountsForServiceAsync(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AppName);

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.Scope,
                new Uri(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted_Google;
            authenticator.Error += OnAuthError;

            Elmah.XamarinForms.Authentication.AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        #endregion 2. Google OAuth2 Login Command

        public LogInViewModel()
        {
            if (AppVM.SignInData.RememberMe)
            {
                this.Email = AppVM.SignInData.UserName;
                this.Password = AppVM.SignInData.Password;
            }
            else
            {
                this.Email = string.Empty;
                this.Password = string.Empty;
            }

            this.RememberMe = AppVM.SignInData.RememberMe;
            this.AutoSignIn = AppVM.SignInData.AutoSignIn;
            RaisePropertyChanged(nameof(EnableLogInButton));
        }

        protected override void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            base.ValidateProperty(value, propertyName);

            RaisePropertyChanged(nameof(EnableLogInButton));
        }

        #region 2. Google OAuth2 Login OnAuthCompleted/OnAuthError

        async void OnAuthCompleted_Google(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;

            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted_Google;
                authenticator.Error -= OnAuthError;
            }

            Elmah.XamarinForms.Authentication.GoogleUser googleUser = null;
            if (e.IsAuthenticated)
            {
                var account = await Framework.Xamariner.Helpers.SecureStorageAccountStoreHelper.FindAccountsForServiceAsync(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AppName);

                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    googleUser = JsonConvert.DeserializeObject<Elmah.XamarinForms.Authentication.GoogleUser>(userJson);
                    var userTokenIdModel = new Framework.WebApi.UserTokenIdModel
                    {
                        AuthenticationProvider = Framework.Models.AuthenticationProvider.Google,
                        Name = googleUser.Name,
                        FamilyName = googleUser.FamilyName,
                        Email = googleUser.Email,
                        Gender = googleUser.Gender,
                        GivenName = googleUser.GivenName,
                        Id = googleUser.Id,
                        JwtToken = e.Account.Properties["id_token"],
                        Picture = googleUser.Picture,
                        VerifiedEmail = googleUser.VerifiedEmail
                    };

                    var authenticationApiClient = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateAuthenticationApiClient();
                    var apiResponse = await authenticationApiClient.SignInWithOAuth2(userTokenIdModel, Framework.Models.AuthenticationProvider.Google);
                }

                if (account != null)
                {
                    await Framework.Xamariner.Helpers.SecureStorageAccountStoreHelper.DeleteAsync(account.FirstOrDefault(), Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AppName);
                }

                await Framework.Xamariner.Helpers.SecureStorageAccountStoreHelper.SaveAsync(e.Account, Elmah.XamarinForms.Authentication.GoogleAuthenticationConstants.AppName);
            }
        }

        #endregion 2. Google OAuth2 Login OnAuthCompleted/OnAuthError

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted_Google;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}


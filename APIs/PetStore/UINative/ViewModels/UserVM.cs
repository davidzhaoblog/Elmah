using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

//using Framework.Xaml.SQLite;

namespace Elmah.PetStore.ViewModels
{
    public partial class UserVM
        : Framework.Xaml.ViewModelBaseWithResultAndUIElement<Elmah.PetStore.Models.User>
    {
        public override string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.User;
        public const string MessageTitle_LoadData = "Load_PetStore_User_VM";

        #region 1. Properties

        // User.Get.01 LoginUser /user/login
        protected LoginUserCriteria m_LoginUserCriteria;
        public LoginUserCriteria LoginUserCriteria
        {
            get { return m_LoginUserCriteria; }
            set
            {
                Set(nameof(LoginUserCriteria), ref m_LoginUserCriteria, value);
            }
        }

        // User.Get.21 GetUserByName /user/{username}
        protected GetUserByNameCriteria m_GetUserByNameCriteria;
        public GetUserByNameCriteria GetUserByNameCriteria
        {
            get { return m_GetUserByNameCriteria; }
            set
            {
                Set(nameof(GetUserByNameCriteria), ref m_GetUserByNameCriteria, value);
            }
        }

        #endregion 1. Properties

        #region 2. Commands

        // User.Delete.01 DeleteUser /user/{username}
        public ICommand DeleteUserCommand { get; protected set; }

        // User.Get.01 LoginUser /user/login
        public ICommand LoginUserCommand { get; protected set; }

        // User.Get.11 LogoutUser /user/logout
        public ICommand LogoutUserCommand { get; protected set; }

        // User.Get.21 GetUserByName /user/{username}
        public ICommand GetUserByNameCommand { get; protected set; }

        // User.Post.01 CreateUser /user
        public ICommand CreateUserCommand { get; protected set; }

        // User.Post.11 CreateUsersWithListInput /user/createWithList
        public ICommand CreateUsersWithListInputCommand { get; protected set; }

        // User.Put.01 UpdateUser /user/{username}
        public ICommand UpdateUserCommand { get; protected set; }

        #endregion 2. Commands

        public UserVM()
            : base()
        {

            // User.Delete.01 DeleteUser /user/{username}
            DeleteUserCommand = new Command(OnDeleteUser, CanDeleteUser);

            // User.Get.01 LoginUser /user/login
            LoginUserCommand = new Command(OnLoginUser, CanLoginUser);

            // User.Get.11 LogoutUser /user/logout
            LogoutUserCommand = new Command(OnLogoutUser, CanLogoutUser);

            // User.Get.21 GetUserByName /user/{username}
            GetUserByNameCommand = new Command(OnGetUserByName, CanGetUserByName);

            // User.Post.01 CreateUser /user
            CreateUserCommand = new Command(OnCreateUser, CanCreateUser);

            // User.Post.11 CreateUsersWithListInput /user/createWithList
            CreateUsersWithListInputCommand = new Command(OnCreateUsersWithListInput, CanCreateUsersWithListInput);

            // User.Put.01 UpdateUser /user/{username}
            UpdateUserCommand = new Command(OnUpdateUser, CanUpdateUser);

        }

        // User.Delete.01 DeleteUser /user/{username}
        public async void OnDeleteUser()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreateUserApiClient();

            // TODO: you may add more code here to get proper parameter values.
            var result = await client.DeleteUserAsync(SelectedItem.Username);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Result.Any(t => t.Id == SelectedItem.Id))
                {
                    Result.Remove(SelectedItem);
                }
                SelectedItem = new Elmah.PetStore.Models.User();

                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullydeleted, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.FailedToSave, GetThisItemDisplayString(), "!");
            }
        }

        protected virtual bool CanDeleteUser()
        {
            return this.SelectedItem != null;
        }

        // User.Get.01 LoginUser /user/login
        public async void OnLoginUser()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            var client = WebApiClientFactory.CreateUserApiClient();

            var result = await client.LoginUserAsync(LoginUserCriteria.Username, LoginUserCriteria.Password);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Result.Any(t => t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullydeleted, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.FailedToSave, GetThisItemDisplayString(), "!");
            }
        }

        protected virtual bool CanLoginUser()
        {
            return true;
        }

        // User.Get.11 LogoutUser /user/logout
        public async void OnLogoutUser()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            var client = WebApiClientFactory.CreateUserApiClient();

            var result = await client.LogoutUserAsync();

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Result.Any(t => t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullydeleted, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.FailedToSave, GetThisItemDisplayString(), "!");
            }
        }

        protected virtual bool CanLogoutUser()
        {
            return true;
        }

        // User.Get.21 GetUserByName /user/{username}
        public async void OnGetUserByName()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            var client = WebApiClientFactory.CreateUserApiClient();

            var result = await client.GetUserByNameAsync(GetUserByNameCriteria.Username);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Result.Any(t => t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullydeleted, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.FailedToSave, GetThisItemDisplayString(), "!");
            }
        }

        protected virtual bool CanGetUserByName()
        {
            return true;
        }

        // User.Post.01 CreateUser /user
        public async void OnCreateUser()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreateUserApiClient();

            var result = await client.CreateUserAsync(SelectedItem);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                SelectedItem = result.Message;
                if(!Result.Any(t=>t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanCreateUser()
        {
            return this.SelectedItem != null;
        }

        // User.Post.11 CreateUsersWithListInput /user/createWithList
        public async void OnCreateUsersWithListInput()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreateUserApiClient();

            var result = await client.CreateUsersWithListInputAsync(SelectedItem);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                SelectedItem = result.Message;
                if(!Result.Any(t=>t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanCreateUsersWithListInput()
        {
            return this.SelectedItem != null;
        }

        // User.Put.01 UpdateUser /user/{username}
        public async void OnUpdateUser()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreateUserApiClient();

            var result = await client.UpdateUserAsync(SelectedItem.Username, SelectedItem);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                SelectedItem = result.Message;
                if(!Result.Any(t=>t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanUpdateUser()
        {
            return this.SelectedItem != null;
        }

        public override Task DoSearch(bool isToClearExistingResult, bool isToLoadFromCache = false, bool enablePopup = true)
        {
            throw new NotImplementedException();
        }

        public override List<Framework.Queries.QueryOrderBySetting> GetDefaultQueryOrderBySettingCollection()
        {
            return new List<Framework.Queries.QueryOrderBySetting> {
                new Framework.Queries.QueryOrderBySetting { IsSelected = true, DisplayName = Elmah.PetStore.Resx.UIStringResource.Name, PropertyName = nameof(Elmah.PetStore.Models.User.Name), Direction = Framework.Queries.QueryOrderDirections.Ascending, FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                        ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { FirstLetter = !string.IsNullOrEmpty(t.Name) && Char.IsLetter(t.Name.First()) ? t.Name.Substring(0, 1) : "?!#1-9" } into tg
                                select new GroupedResult(tg.Key.FirstLetter, tg.Key.FirstLetter, tg.Select(t => t.GetAClone()).ToList());
                            return groupedResult.ToList();
                         },
                         //GetSQLiteSortTableQuery = (tableQuery, direction) => {
                         //   tableQuery = tableQuery.Sort(t => t.Type, direction);
                         //    return tableQuery;
                         //}
                }}
            };
        }
    }

    // User.Get.01 LoginUser /user/login
    public class LoginUserCriteria: Framework.Models.PropertyChangedNotifier
    {

        private string m_Username;

        [Display(Name = "Username", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public string Username
        {
            get
            {
                return m_Username;
            }
            set
            {
                Set(nameof(Username), ref m_Username, value);
            }
        }

        private string m_Password;

        [Display(Name = "Password", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                Set(nameof(Password), ref m_Password, value);
            }
        }

    }

    // User.Get.21 GetUserByName /user/{username}
    public class GetUserByNameCriteria: Framework.Models.PropertyChangedNotifier
    {

        private string m_Username;

        [Display(Name = "Username", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public string Username
        {
            get
            {
                return m_Username;
            }
            set
            {
                Set(nameof(Username), ref m_Username, value);
            }
        }

    }

}


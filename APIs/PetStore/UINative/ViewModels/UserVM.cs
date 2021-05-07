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

        public const string MessageTitle_LoadData_LoginUser = "Load_PetStore_User_LoginUser";

        public const string MessageTitle_LoadData_LogoutUser = "Load_PetStore_User_LogoutUser";

        public const string MessageTitle_LoadData_GetUserByName = "Load_PetStore_User_GetUserByName";

        #region 1. Properties

        protected NavigationVM.UserActions m_CurrentGetAction;
        public NavigationVM.UserActions CurrentGetAction
        {
            get { return m_CurrentGetAction; }
            set
            {
                Set(nameof(CurrentGetAction), ref m_CurrentGetAction, value);
            }
        }

        public NavigationVM.UserContainer NavigationContainer
        {
            get
            {
                return DependencyService.Resolve<NavigationVM.UserContainer>();
            }
        }

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
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            Framework.WebApi.Response<Elmah.PetStore.Models.User[]> result;
            if(false)
            {}

            else
            {
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.FailedToSave, GetThisItemDisplayString(), "!");
                return;
            }

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (!Result.Any(t => t.Id == SelectedItem.Id))
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

        /*
        // TODO: you can customize Search()/CanSearch()/LoadMore()
        protected override async void Search()
        {
        }

        protected override bool CanSearch()
        {
        }

        protected override async void LoadMore()
        {
        }
        */

        public override List<Framework.Queries.QueryOrderBySetting> GetDefaultQueryOrderBySettingCollection()
        {
            return new List<Framework.Queries.QueryOrderBySetting> {
                new Framework.Queries.QueryOrderBySetting { IsSelected = true, DisplayName = Elmah.PetStore.Resx.UIStringResource.Name, PropertyName = nameof(Elmah.PetStore.Models.User.Username), Direction = Framework.Queries.QueryOrderDirections.Ascending, FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                        ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { FirstLetter = !string.IsNullOrEmpty(t.Username) && Char.IsLetter(t.Username.First()) ? t.Username.Substring(0, 1) : "?!#1-9" } into tg
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

        protected override void Search()
        {
            throw new NotImplementedException();
        }

        protected override bool CanSearch()
        {
            throw new NotImplementedException();
        }

        protected override void LoadMore()
        {
            throw new NotImplementedException();
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


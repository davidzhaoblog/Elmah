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
    public partial class UserListVM
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
        protected LoginUserCriteria m_LoginUserCriteria = new LoginUserCriteria();
        public LoginUserCriteria LoginUserCriteria
        {
            get { return m_LoginUserCriteria; }
            set
            {
                Set(nameof(LoginUserCriteria), ref m_LoginUserCriteria, value);
            }
        }

        // User.Get.21 GetUserByName /user/{username}
        protected GetUserByNameCriteria m_GetUserByNameCriteria = new GetUserByNameCriteria();
        public GetUserByNameCriteria GetUserByNameCriteria
        {
            get { return m_GetUserByNameCriteria; }
            set
            {
                Set(nameof(GetUserByNameCriteria), ref m_GetUserByNameCriteria, value);
            }
        }

        #endregion 1. Properties

        public UserListVM()
            : base()
        {

        }

        public override async Task DoSearch(bool isToClearExistingResult, bool isToLoadFromCache = false, bool enablePopup = true)
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            try
            {
                Framework.WebApi.Response<Elmah.PetStore.Models.User[]> result;
                if(false)
                {}

                else
                {
                    // Do something, developer coding error: parameter is wrong
                    PopupVM.HidePopup();
                    return;
                }

                if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                // success, will close Item Popup and popup message box
                {
                    BindResult(result.Message.ToList(), isToClearExistingResult);
                }
                else
                // failed
                {
                    // TODO: should display error message, no change to binding?
                    this.StatusMessageOfResult = result.ErrorMessage.FirstOrDefault().Value;
                    this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                }
            }
            catch //(Exception ex)
            {
            }

            if (enablePopup)
                PopupVM.HidePopup();
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
                                select new GroupedResult(tg.Key.FirstLetter, tg.Key.FirstLetter, tg.Select(t => t.GetAClone<Models.User>()).ToList());
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


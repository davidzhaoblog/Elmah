using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

//using Framework.Xaml.SQLite;

namespace Elmah.PetStore.ViewModels.Pet
{
    public partial class FindPetsByStatusVM
        : Framework.Xaml.ViewModelBaseWithResultAndUIElement<Elmah.PetStore.Models.Pet>
    {
        public override string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Pet;

        public const string MessageTitle_LoadData_FindPetsByStatus = "Load_PetStore_Pet_FindPetsByStatus";

        #region 1. Properties

        protected NavigationVM.PetActions m_CurrentGetAction;
        public NavigationVM.PetActions CurrentGetAction
        {
            get { return m_CurrentGetAction; }
            set
            {
                Set(nameof(CurrentGetAction), ref m_CurrentGetAction, value);
            }
        }

        public NavigationVM.PetContainer NavigationContainer
        {
            get
            {
                return DependencyService.Resolve<NavigationVM.PetContainer>();
            }
        }

        // Pet.Get FindPetsByStatus /pet/findByStatus
        protected FindPetsByStatusCriteria m_FindPetsByStatusCriteria = new FindPetsByStatusCriteria();
        public FindPetsByStatusCriteria FindPetsByStatusCriteria
        {
            get { return m_FindPetsByStatusCriteria; }
            set
            {
                Set(nameof(FindPetsByStatusCriteria), ref m_FindPetsByStatusCriteria, value);
            }
        }

        #endregion 1. Properties

        public FindPetsByStatusVM()
            : base()
        {
            MessagingCenter.Subscribe<FindPetsByStatusVM, Framework.Xaml.LoadListDataRequest>(this, MessageTitle_LoadData_FindPetsByStatus, async (sender, request) =>
            {
                CurrentGetAction = NavigationVM.PetActions.FindPetsByStatus;
                ListItemViewMode = request.ListItemViewMode;
                if(request.BindToGroupedResults.HasValue)
                {
                    if (!request.BindToGroupedResults.Value)
                        BindToGroupedResults = request.BindToGroupedResults.Value;
                    else
                        SetBindToGroupedResults(request.OrderByPropertyName, request.OrderByDirection);
                }
                // Set Critieria
                if(request.Parameters != null)
                {
                    if (request.Parameters.ContainsKey(nameof(Elmah.PetStore.Models.Pet.onecondition)) && request.Parameters[nameof(Elmah.PetStore.Models.Pet.onecondition)] != null)
                        FindPetsByStatusCriteria.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.PetStore.Models.Pet.onecondition)];
                    // can be more
                    //if (request.Parameters.ContainsKey(nameof(Elmah.PetStore.Models.Pet.onecondition)) && request.Parameters[nameof(Elmah.PetStore.Models.Pet.onecondition)] != null)
                        //FindPetsByStatusCriteria.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.PetStore.Models.Pet.onecondition)];
                }
                CachingOption = Framework.Xaml.CachingOptions.NoCaching  ?;
                QueryPagingSetting = GetDefaultQueryPagingSetting();
                QueryPagingSetting.CurrentPage = 1;
                await DoSearch(true, true);
                if(request.ActionWhenLaunch != null)
                    request.ActionWhenLaunch();
            });
        }

        public override async Task DoSearch(bool isToClearExistingResult, bool isToLoadFromCache = false, bool enablePopup = true)
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            try
            {
                Framework.WebApi.Response<Elmah.PetStore.Models.Pet[]> result;
                var client = WebApiClientFactory.CreatePetApiClient();
                result = await client.FindPetsByStatusAsync(FindPetsByStatusCriteria.Status);

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
                new Framework.Queries.QueryOrderBySetting { IsSelected = true, DisplayName = Elmah.PetStore.Resx.UIStringResource.Name, PropertyName = nameof(Elmah.PetStore.Models.Pet.Name), Direction = Framework.Queries.QueryOrderDirections.Ascending, FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                        ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { FirstLetter = !string.IsNullOrEmpty(t.Name) && Char.IsLetter(t.Name.First()) ? t.Name.Substring(0, 1) : "?!#1-9" } into tg
                                select new GroupedResult(tg.Key.FirstLetter, tg.Key.FirstLetter, tg.Select(t => t.GetAClone<Elmah.PetStore.Models.Pet>()).ToList());
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

    // Pet.Get. FindPetsByStatus /pet/findByStatus
    public class FindPetsByStatusCriteria: Framework.Models.PropertyChangedNotifier
    {

        private string m_Status;

        [Display(Name = "Status", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public string Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                Set(nameof(Status), ref m_Status, value);
            }
        }

    }
}


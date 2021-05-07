using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class NavigationVM
    {
        public PetContainer Pet
        {
            get
            {
                return DependencyService.Resolve<PetContainer>();
            }
        }

        public enum PetActions
        {
            DeletePet,
            FindPetsByStatus,
            FindPetsByTags,
            GetPetById,
            AddPet,
            UpdatePetWithForm,
            UploadFile,
            UpdatePet
        }

        public partial class PetContainer: Framework.Xaml.NavigationVMEntityContainer<Elmah.PetStore.Models.Pet>
        {
            public const string DomainKey = "PetStore_Pet";

            public PetContainer(): base()
            {
            }

            public Framework.Xaml.ActionForm.ActionParameter GetNavigateToCommandParam_ListPage(
                long oneCondition // can be more
                , Framework.Xaml.ListItemViewModes listItemViewMode
                , bool? bindToGroupedResults
                , string orderByPropertyName
                , Framework.Queries.QueryOrderDirections? orderByDirection)
            {
                return new Framework.Xaml.ActionForm.ActionParameter
                {
                    Domain = DomainKey
                    ,
                    Page = PetActions.??.ToString()
                    ,
                    SendMessage = () => {
                        SendMessage_Init_ListPage(oneCondition, listItemViewMode, bindToGroupedResults, orderByPropertyName, orderByDirection);
                    }
                };
            }

            /// <summary>
            /// this method can be called in other view models or anywhere to initialize CommonResultView if referenced in other places.
            /// </summary>
            public void SendMessage_Init_ListPage(
                long oneCondition // can be more
                , Framework.Xaml.ListItemViewModes listItemViewMode
                , bool? bindToGroupedResults
                , string orderByPropertyName
                , Framework.Queries.QueryOrderDirections? orderByDirection)
            {
                var vm = DependencyService.Resolve<PetListVM>();
                MessagingCenter.Send<PetListVM, Framework.Xaml.LoadListDataRequest>(vm, PetListVM.MessageTitle_LoadData,
                    new Framework.Xaml.LoadListDataRequest
                    {
                        ListItemViewMode = listItemViewMode
                        ,
                        BindToGroupedResults = bindToGroupedResults
                        ,
                        OrderByPropertyName = orderByPropertyName
                        ,
                        OrderByDirection = orderByDirection
                        ,
                        Parameters = new Dictionary<string, object>
                        {
                            // { nameof(Elmah.DataSourceEntities.ELMAH_Error.Default.oneCondition), oneCondition },
                            // { nameof(Elmah.DataSourceEntities.ELMAH_Error.Default.BusinessEntityID), businessEntityID }, // can be more
                        }
                        ,
                        ActionWhenLaunch = () => { DefaultItem(oneCondition); ListFooterActionSheet = GetListFooterActionSheet(vm); }
                    });
            }

            protected virtual Framework.Xaml.ActionForm.ActionSheetVM GetListFooterActionSheet(PetListVM vm)
            {
                List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                //list.Add(GetActionItemModel_LaunchCommonSearchView());
                //list.Add(GetActionItemModel(ItemVM.DefaultItem, Framework.Xaml.StandardRouteRelativeKey.Create, Framework.Xaml.ControlParentOptions.InPopup));
                //list.Add(GetShowListFullScreenActionSheetActionItemModel(ShowListFullScreenActionSheetCommand));
                ShowListFullScreenActionSheetActionItemModel = GetShowListFullScreenActionSheetActionItemModel_Sort(ShowListFullScreenActionSheetCommand, vm.QueryOrderBySettingCollection.FirstOrDefault(t => t.IsSelected));
                list.Add(ShowListFullScreenActionSheetActionItemModel);
                return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = true, ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position)) };
            }

            protected override void OnShowListFullScreenActionSheetCommand()
            {
                var actionItems = new List<Framework.Xaml.ActionForm.ActionItemModel>();

                var vm = DependencyService.Resolve<PetListVM>();
                vm.PostOnSortCommand = UpdateShowListFullScreenActionSheetActionItemModel;

                actionItems.Add(vm.GetToggleGroupedResultsViewSearchActionItemModel());

                actionItems.AddRange(GetSortActionItems(vm.QueryOrderBySettingCollection, vm.SortCommand));

                actionItems.Add(PopupVM.CloseActionItemModel);

                PopupVM.ShowVerticalActionSheet(new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = true, ActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(actionItems) });
            }
            private void UpdateShowListFullScreenActionSheetActionItemModel()
            {
                var vm = DependencyService.Resolve<PetListVM>();
                UpdateShowListFullScreenActionSheetActionItemModel(vm.QueryOrderBySettingCollection);
            }

            public void DefaultItem(long entityID)
            {
            }
        }
    }
}


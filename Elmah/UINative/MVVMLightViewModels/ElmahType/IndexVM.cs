using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Windows.Input;

using Framework.Xaml.SQLite;

namespace Elmah.MVVMLightViewModels.ElmahType
{
    public partial class IndexVM
        : Framework.Xaml.ViewModelBaseWithResultAndUIElement<Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ElmahType>, Elmah.DataSourceEntities.ElmahType, Elmah.ViewModelData.ElmahType.IndexVM, Elmah.MVVMLightViewModels.ElmahType.ItemVM, Elmah.DataSourceEntities.ElmahTypeIdentifier, Elmah.MVVMLightViewModels.NavigationVM.ElmahTypeContainer, Elmah.SQLite.ElmahTypeRepository, Elmah.SQLite.TableModels.ElmahType, Elmah.EntityContracts.IElmahTypeIdentifier>
    {
        public const string MessageTitle_LoadData = "Load_ElmahType_IndexVM";
        public override string SearchBarPlaceHolder => Elmah.Resx.UIStringResourcePerApp.ElmahType;

        #region 1. Global Variables

        public override Elmah.SQLite.ElmahTypeRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ElmahTypeRepository;
            }
        }

        public override Elmah.MVVMLightViewModels.NavigationVM.ElmahTypeContainer NavigationContainer
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM.ElmahTypeContainer>();
            }
        }

        public override ItemVM ItemVM
        {
            get
            {
                return DependencyService.Resolve<ItemVM>();
            }
        }

        #endregion 1. Global Variables

        /// <summary>
        /// Initializes a new instance of the IndexVM class.
        /// </summary>
        public IndexVM()
            : base()
        {
            MessagingCenter.Subscribe<IndexVM, Framework.Xaml.LoadListDataRequest>(this, MessageTitle_LoadData, async (sender, request) =>
            {
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
                    if (request.Parameters.ContainsKey(nameof(Elmah.DataSourceEntities.ElmahType.onecondition)) && request.Parameters[nameof(Elmah.DataSourceEntities.ElmahType.onecondition)] != null)
                        this.Criteria.Common.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.DataSourceEntities.ElmahType.onecondition)];
                    // can be more
                    //if (request.Parameters.ContainsKey(nameof(Elmah.DataSourceEntities.ElmahType.onecondition)) && request.Parameters[nameof(Elmah.DataSourceEntities.ElmahType.onecondition)] != null)
                        //this.Criteria.Common.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.DataSourceEntities.ElmahType.onecondition)];
                }
                CachingOption = Framework.Xaml.CachingOptions.NoCaching  ?;
                QueryPagingSetting = GetDefaultQueryPagingSetting();
                QueryPagingSetting.CurrentPage = 1;
                await DoSearch(true, true);
                if(request.ActionWhenLaunch != null)
                    request.ActionWhenLaunch();
            });
        }

        protected override void CopyToItemInList(Elmah.DataSourceEntities.ElmahType itemInList, Elmah.DataSourceEntities.ElmahType newItem)
        {
            itemInList.CopyFrom<Elmah.DataSourceEntities.ElmahType>(newItem);
        }

        protected override Func<Elmah.DataSourceEntities.ElmahType, bool> GetPredicateToGetAnExistingItem(Elmah.DataSourceEntities.ElmahType item)
        {
            return t => t.Type == ItemVM.Item.Type;
        }

        protected override async void OnTextFilterCommand(string filter)
        {
            this.Criteria.Common.StringContains_AllColumns.NullableValueToBeContained = filter;

            Criteria.Common.Type.NullableValueToBeContained = filter;

            await DoSearch(true, false, false);
        }

        protected override async Task<Elmah.ViewModelData.ElmahType.IndexVM> GetFromServer()
        {
            var vmData = new Elmah.ViewModelData.ElmahType.IndexVM
            {
                Criteria = this.Criteria,
                QueryPagingSetting = CachingOption == Framework.Xaml.CachingOptions.NoCaching ? this.QueryPagingSetting : new Framework.Queries.QueryPagingSetting(1, 10000),
                QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection(QueryOrderBySettingCollection.Where(t => t.IsSelected)),
            };
            vmData.Criteria.CanQueryWhenNoQuery = true;

            /*
            // Add extra QueryOrderBySetting, eg CategoryName -> Name of this class.
            if (QueryOrderBySettingCollection.Any(t => t.IsSelected && t.PropertyName == nameof(Elmah.DataSourceEntities.ElmahType.??)))
            {
                vmData.QueryOrderBySettingCollection.Add(new Framework.Queries.QueryOrderBySetting { PropertyName = nameof(Elmah.DataSourceEntities.ElmahType.??), Direction = QueryOrderBySettingCollection.First(t => t.IsSelected && t.PropertyName == nameof(Elmah.DataSourceEntities.ElmahType.??)).Direction } );
            }
            */

            var client = WebApiClientFactory.CreateElmahTypeApiClient();
            var result = await client.GetIndexVMAsync(vmData);
            return result;
        }

        public override List<Framework.Queries.QueryOrderBySetting> GetDefaultQueryOrderBySettingCollection()
        {
            return new List<Framework.Queries.QueryOrderBySetting> {
                new Framework.Queries.QueryOrderBySetting{ DisplayName = Elmah.Resx.UIStringResourcePerEntity., PropertyName = nameof(Elmah.DataSourceEntities.ElmahType.?), Direction = Framework.Queries.QueryOrderDirections.Descending, FontIcon = Framework.Xaml.FontAwesomeIcons.ThList, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                    ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { t., t. } into tg
                                select new GroupedResult(tg.Key., tg.Key., tg.Select(t => t.GetAClone<Elmah.DataSourceEntities.ElmahType>()).ToList());
                            return groupedResult.ToList();
                         },
                         GetSQLiteSortTableQuery = (tableQuery, direction) => {
                             tableQuery = tableQuery.Sort(t => t., direction).ThenSort(t => t., direction);
                             return tableQuery;
                         }
                }},
                new Framework.Queries.QueryOrderBySetting{ IsSelected = true, DisplayName = Elmah.Resx.UIStringResourcePerEntity.Name, PropertyName = nameof(Elmah.DataSourceEntities.ElmahType.Name), Direction = Framework.Queries.QueryOrderDirections.Ascending, FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                        ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { FirstLetter = !string.IsNullOrEmpty(t.Name) && Char.IsLetter(t.Name.First()) ? t.Name.Substring(0, 1) : "?!#1-9" } into tg
                                select new GroupedResult(tg.Key.FirstLetter, tg.Key.FirstLetter, tg.Select(t => t.GetAClone<Elmah.DataSourceEntities.ElmahType>()).ToList());
                            return groupedResult.ToList();
                         },
                         GetSQLiteSortTableQuery = (tableQuery, direction) => {
                            tableQuery = tableQuery.Sort(t => t.Name, direction);
                             return tableQuery;
                         }
                }},
                new Framework.Queries.QueryOrderBySetting{ DisplayName = Framework.Resx.UIStringResource.Recent, PropertyName = nameof(Elmah.DataSourceEntities.ElmahType.ModifiedDate), Direction = Framework.Queries.QueryOrderDirections.Descending, FontIcon = Framework.Xaml.FontAwesomeIcons.Clock, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                    ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { t.ModifiedDate?.Year } into tg
                                select new GroupedResult(tg.Key.Year, tg.Key.Year?.ToString(), tg.Select(t => t.GetAClone<Elmah.DataSourceEntities.ElmahType>()).ToList());
                            return groupedResult.ToList();
                         },
                         GetSQLiteSortTableQuery = (tableQuery, direction) => {
                             tableQuery = tableQuery.Sort(t => t.ModifiedDate, direction);
                             return tableQuery;
                         }
                }},
                new Framework.Queries.QueryOrderBySetting{ DisplayName = Framework.Resx.UIStringResource.Age, PropertyName = nameof(Elmah.DataSourceEntities.ElmahType.CreatedDate), Direction = Framework.Queries.QueryOrderDirections.Descending, FontIcon = Framework.Xaml.FontAwesomeIcons.History, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                    ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { t.CreatedDate.Year } into tg
                                select new GroupedResult(tg.Key.Year, tg.Key.Year.ToString(), tg.Select(t => t.GetAClone<Elmah.DataSourceEntities.ElmahType>()).ToList());
                            return groupedResult.ToList();
                         },
                         GetSQLiteSortTableQuery = (tableQuery, direction) => {
                             tableQuery = tableQuery.Sort(t => t.CreatedDate, direction);
                             return tableQuery;
                         }
                }},
            };
        }
    }
}


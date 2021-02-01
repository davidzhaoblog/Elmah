using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Windows.Input;

using Framework.Xaml.SQLite;

namespace Elmah.MVVMLightViewModels.ElmahSource
{
    public partial class IndexVM
        : Framework.Xaml.ViewModelBaseWithResultAndUIElement<Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ElmahSource>, Elmah.DataSourceEntities.ElmahSource, Elmah.ViewModelData.ElmahSource.IndexVM, Elmah.MVVMLightViewModels.ElmahSource.ItemVM, Elmah.DataSourceEntities.ElmahSourceIdentifier, Elmah.MVVMLightViewModels.NavigationVM.ElmahSourceContainer, Elmah.SQLite.ElmahSourceRepository, Elmah.SQLite.TableModels.ElmahSource, Elmah.EntityContracts.IElmahSourceIdentifier>
    {
        public const string MessageTitle_LoadData = "Load_ElmahSource_IndexVM";
        public override string SearchBarPlaceHolder => Elmah.Resx.UIStringResourcePerApp.ElmahSource;

        #region 1. Global Variables

        public override Elmah.SQLite.ElmahSourceRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ElmahSourceRepository;
            }
        }

        public override Elmah.MVVMLightViewModels.NavigationVM.ElmahSourceContainer NavigationContainer
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM.ElmahSourceContainer>();
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
                    //if (request.Parameters.ContainsKey(nameof(Elmah.DataSourceEntities.ElmahSource.onecondition)) && request.Parameters[nameof(Elmah.DataSourceEntities.ElmahSource.onecondition)] != null)
                    //    this.Criteria.Common.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.DataSourceEntities.ElmahSource.onecondition)];
                    // can be more
                    //if (request.Parameters.ContainsKey(nameof(Elmah.DataSourceEntities.ElmahSource.onecondition)) && request.Parameters[nameof(Elmah.DataSourceEntities.ElmahSource.onecondition)] != null)
                        //this.Criteria.Common.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.DataSourceEntities.ElmahSource.onecondition)];
                }
                CachingOption = Framework.Xaml.CachingOptions.NoCaching;
                QueryPagingSetting = GetDefaultQueryPagingSetting();
                QueryPagingSetting.CurrentPage = 1;
                await DoSearch(true, true);
                if(request.ActionWhenLaunch != null)
                    request.ActionWhenLaunch();
            });
        }

        protected override void CopyToItemInList(Elmah.DataSourceEntities.ElmahSource itemInList, Elmah.DataSourceEntities.ElmahSource newItem)
        {
            itemInList.CopyFrom<Elmah.DataSourceEntities.ElmahSource>(newItem);
        }

        protected override Func<Elmah.DataSourceEntities.ElmahSource, bool> GetPredicateToGetAnExistingItem(Elmah.DataSourceEntities.ElmahSource item)
        {
            return t => t.Source == ItemVM.Item.Source;
        }

        protected override async void OnTextFilterCommand(string filter)
        {
            this.Criteria.Common.StringContains_AllColumns.NullableValueToBeContained = filter;

            Criteria.Common.Source.NullableValueToBeContained = filter;

            await DoSearch(true, false, false);
        }

        protected override async Task<Elmah.ViewModelData.ElmahSource.IndexVM> GetFromServer()
        {
            var vmData = new Elmah.ViewModelData.ElmahSource.IndexVM
            {
                Criteria = this.Criteria,
                QueryPagingSetting = CachingOption == Framework.Xaml.CachingOptions.NoCaching ? this.QueryPagingSetting : new Framework.Queries.QueryPagingSetting(1, 10000),
                QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection(QueryOrderBySettingCollection.Where(t => t.IsSelected)),
            };
            vmData.Criteria.CanQueryWhenNoQuery = true;

            /*
            // Add extra QueryOrderBySetting, eg CategoryName -> Name of this class.
            if (QueryOrderBySettingCollection.Any(t => t.IsSelected && t.PropertyName == nameof(Elmah.DataSourceEntities.ElmahSource.??)))
            {
                vmData.QueryOrderBySettingCollection.Add(new Framework.Queries.QueryOrderBySetting { PropertyName = nameof(Elmah.DataSourceEntities.ElmahSource.??), Direction = QueryOrderBySettingCollection.First(t => t.IsSelected && t.PropertyName == nameof(Elmah.DataSourceEntities.ElmahSource.??)).Direction } );
            }
            */

            var client = WebApiClientFactory.CreateElmahSourceApiClient();
            var result = await client.GetIndexVMAsync(vmData);
            return result;
        }

        public override List<Framework.Queries.QueryOrderBySetting> GetDefaultQueryOrderBySettingCollection()
        {
            return new List<Framework.Queries.QueryOrderBySetting> {

                new Framework.Queries.QueryOrderBySetting{ IsSelected = true, DisplayName = Elmah.Resx.UIStringResourcePerEntity.Source, PropertyName = nameof(Elmah.DataSourceEntities.ElmahSource.Source), Direction = Framework.Queries.QueryOrderDirections.Ascending, FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                        ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { FirstLetter = !string.IsNullOrEmpty(t.Source) && Char.IsLetter(t.Source.First()) ? t.Source.Substring(0, 1) : "?!#1-9" } into tg
                                select new GroupedResult(tg.Key.FirstLetter, tg.Key.FirstLetter, tg.Select(t => t.GetAClone<Elmah.DataSourceEntities.ElmahSource>()).ToList());
                            return groupedResult.ToList();
                         },
                         GetSQLiteSortTableQuery = (tableQuery, direction) => {
                            tableQuery = tableQuery.Sort(t => t.Source, direction);
                             return tableQuery;
                         }
                }}
            };
        }
    }
}


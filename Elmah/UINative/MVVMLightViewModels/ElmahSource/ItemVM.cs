using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels.ElmahSource
{
    /// <summary>
    /// This class contains single item view model
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </summary>
    public partial class ItemVM
        : Framework.Xaml.ViewModelItemBase<Elmah.DataSourceEntities.ElmahSourceIdentifier, Elmah.EntityContracts.IElmahSourceIdentifier, Elmah.DataSourceEntities.ElmahSource, Elmah.ViewModelData.ElmahSource.ItemVM, Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn, Elmah.SQLite.ElmahSourceRepository, Elmah.SQLite.TableModels.ElmahSource>
    {
        public const string MessageTitle_LoadItem = "Load_ElmahSource_ItemVM";
        public const string MessageTitle_AssignItem = "Assign_ElmahSource_ItemVM";

        public override Elmah.SQLite.ElmahSourceRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ElmahSourceRepository;
            }
        }

        public ItemVM()
            : base()
        {
            // Keep MessagingCenter.Subscribe here
            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahSource.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahSourceIdentifier>>(
                this
                , MessageTitle_LoadItem
                , async (sender, request) =>{
                    await ProcessLoadItemRequest(request);
                });

            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahSource.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahSource>>(
                this
                , MessageTitle_AssignItem
                , async (sender, request) =>{
                    await ProcessAssignItemRequest(request);
                });
        }

        protected override async Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> UpsertToServer()
        {
            var client = WebApiClientFactory.CreateElmahSourceApiClient();
            var item = Item;

            var result = await client.UpsertEntityAsync(item);
            return result;
        }

        /*
        protected override async Task DeleteFromServer()
        {
            var client = WebApiClientFactory.CreateElmahSourceApiClient();
            var item = Item;

            var result = await client.DeleteEntityAsync(item);
            this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
            this.StatusMessageOfResult = result.GetStatusMessage();
        }
        */

        protected override async Task<Elmah.ViewModelData.ElmahSource.ItemVM> GetFromServer(Elmah.EntityContracts.IElmahSourceIdentifier identifier)
        {
            var client = WebApiClientFactory.CreateElmahSourceApiClient();
            var result = await client.GetItemVMAsync(identifier.Source);
            return result;
        }

        public override async Task LoadDropDownListContents()
        {

        }

        protected override void GetValueFromDropDownListSelectedItems()
        {

        }

        public override void SetDropDownListSelectedItems(Elmah.DataSourceEntities.ElmahSource o)
        {

        }

        protected override Elmah.DataSourceEntities.ElmahSource GetAClone(Elmah.DataSourceEntities.ElmahSource item)
        {
            return item?.GetAClone<Elmah.DataSourceEntities.ElmahSource>();
        }

        /*
        public override string GetThisItemDisplayString()
        {
            return Item != null ? string.Format("{0}({1})", Item. ?? ): string.Empty;
        }
        */

        public override string GetDisplayString(Elmah.DataSourceEntities.ElmahSource item)
        {
            return item != null ? string.Format("{0}", item.Source ): string.Empty;
        }

        /*
        public override void Cleanup()
        {
            base.Cleanup();
        }
        */
    }
}


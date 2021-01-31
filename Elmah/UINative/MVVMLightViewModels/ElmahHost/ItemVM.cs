using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels.ElmahHost
{
    /// <summary>
    /// This class contains single item view model
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </summary>
    public partial class ItemVM
        : Framework.Xaml.ViewModelItemBase<Elmah.DataSourceEntities.ElmahHostIdentifier, Elmah.EntityContracts.IElmahHostIdentifier, Elmah.DataSourceEntities.ElmahHost, Elmah.ViewModelData.ElmahHost.ItemVM, Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn, Elmah.SQLite.ElmahHostRepository, Elmah.SQLite.TableModels.ElmahHost>
    {
        public const string MessageTitle_LoadItem = "Load_ElmahHost_ItemVM";
        public const string MessageTitle_AssignItem = "Assign_ElmahHost_ItemVM";

        public override Elmah.SQLite.ElmahHostRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ElmahHostRepository;
            }
        }

        public ItemVM()
            : base()
        {
            // Keep MessagingCenter.Subscribe here
            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahHost.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahHostIdentifier>>(
                this
                , MessageTitle_LoadItem
                , async (sender, request) =>{
                    await ProcessLoadItemRequest(request);
                });

            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahHost.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahHost>>(
                this
                , MessageTitle_AssignItem
                , async (sender, request) =>{
                    await ProcessAssignItemRequest(request);
                });
        }

        protected override async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> UpsertToServer()
        {
            var client = WebApiClientFactory.CreateElmahHostApiClient();
            var item = Item;

            var result = await client.UpsertEntityAsync(item);
            return result;
        }

        /*
        protected override async Task DeleteFromServer()
        {
            var client = WebApiClientFactory.CreateElmahHostApiClient();
            var item = Item;

            var result = await client.DeleteEntityAsync(item);
            this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
            this.StatusMessageOfResult = result.GetStatusMessage();
        }
        */

        protected override async Task<Elmah.ViewModelData.ElmahHost.ItemVM> GetFromServer(Elmah.EntityContracts.IElmahHostIdentifier identifier)
        {
            var client = WebApiClientFactory.CreateElmahHostApiClient();
            var result = await client.GetItemVMAsync(identifier.Host);
            return result;
        }

        public override async Task LoadDropDownListContents()
        {

        }

        protected override void GetValueFromDropDownListSelectedItems()
        {

        }

        public override void SetDropDownListSelectedItems(Elmah.DataSourceEntities.ElmahHost o)
        {

        }

        protected override Elmah.DataSourceEntities.ElmahHost GetAClone(Elmah.DataSourceEntities.ElmahHost item)
        {
            return item?.GetAClone<Elmah.DataSourceEntities.ElmahHost>();
        }

        /*
        public override string GetThisItemDisplayString()
        {
            return Item != null ? string.Format("{0}({1})", Item. ?? ): string.Empty;
        }
        */

        public override string GetDisplayString(Elmah.DataSourceEntities.ElmahHost item)
        {
            return item != null ? string.Format("{0}({1})", item. ?? ): string.Empty;
        }

        /*
        public override void Cleanup()
        {
            base.Cleanup();
        }
        */
    }
}


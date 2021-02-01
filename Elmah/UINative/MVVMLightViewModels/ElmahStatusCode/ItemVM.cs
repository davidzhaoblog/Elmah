using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels.ElmahStatusCode
{
    /// <summary>
    /// This class contains single item view model
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </summary>
    public partial class ItemVM
        : Framework.Xaml.ViewModelItemBase<Elmah.DataSourceEntities.ElmahStatusCodeIdentifier, Elmah.EntityContracts.IElmahStatusCodeIdentifier, Elmah.DataSourceEntities.ElmahStatusCode, Elmah.ViewModelData.ElmahStatusCode.ItemVM, Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn, Elmah.SQLite.ElmahStatusCodeRepository, Elmah.SQLite.TableModels.ElmahStatusCode>
    {
        public const string MessageTitle_LoadItem = "Load_ElmahStatusCode_ItemVM";
        public const string MessageTitle_AssignItem = "Assign_ElmahStatusCode_ItemVM";

        public override Elmah.SQLite.ElmahStatusCodeRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ElmahStatusCodeRepository;
            }
        }

        public ItemVM()
            : base()
        {
            // Keep MessagingCenter.Subscribe here
            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahStatusCodeIdentifier>>(
                this
                , MessageTitle_LoadItem
                , async (sender, request) =>{
                    await ProcessLoadItemRequest(request);
                });

            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahStatusCode>>(
                this
                , MessageTitle_AssignItem
                , async (sender, request) =>{
                    await ProcessAssignItemRequest(request);
                });
        }

        protected override async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> UpsertToServer()
        {
            var client = WebApiClientFactory.CreateElmahStatusCodeApiClient();
            var item = Item;

            var result = await client.UpsertEntityAsync(item);
            return result;
        }

        /*
        protected override async Task DeleteFromServer()
        {
            var client = WebApiClientFactory.CreateElmahStatusCodeApiClient();
            var item = Item;

            var result = await client.DeleteEntityAsync(item);
            this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
            this.StatusMessageOfResult = result.GetStatusMessage();
        }
        */

        protected override async Task<Elmah.ViewModelData.ElmahStatusCode.ItemVM> GetFromServer(Elmah.EntityContracts.IElmahStatusCodeIdentifier identifier)
        {
            var client = WebApiClientFactory.CreateElmahStatusCodeApiClient();
            var result = await client.GetItemVMAsync(identifier.StatusCode);
            return result;
        }

        public override async Task LoadDropDownListContents()
        {

        }

        protected override void GetValueFromDropDownListSelectedItems()
        {

        }

        public override void SetDropDownListSelectedItems(Elmah.DataSourceEntities.ElmahStatusCode o)
        {

        }

        protected override Elmah.DataSourceEntities.ElmahStatusCode GetAClone(Elmah.DataSourceEntities.ElmahStatusCode item)
        {
            return item?.GetAClone<Elmah.DataSourceEntities.ElmahStatusCode>();
        }

        /*
        public override string GetThisItemDisplayString()
        {
            return Item != null ? string.Format("{0}({1})", Item. ?? ): string.Empty;
        }
        */

        public override string GetDisplayString(Elmah.DataSourceEntities.ElmahStatusCode item)
        {
            return item != null ? string.Format("{0}({1})", item.StatusCode, item.Name ): string.Empty;
        }

        /*
        public override void Cleanup()
        {
            base.Cleanup();
        }
        */
    }
}


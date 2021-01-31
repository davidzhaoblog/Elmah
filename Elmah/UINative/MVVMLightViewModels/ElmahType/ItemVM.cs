using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels.ElmahType
{
    /// <summary>
    /// This class contains single item view model
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </summary>
    public partial class ItemVM
        : Framework.Xaml.ViewModelItemBase<Elmah.DataSourceEntities.ElmahTypeIdentifier, Elmah.EntityContracts.IElmahTypeIdentifier, Elmah.DataSourceEntities.ElmahType, Elmah.ViewModelData.ElmahType.ItemVM, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn, Elmah.SQLite.ElmahTypeRepository, Elmah.SQLite.TableModels.ElmahType>
    {
        public const string MessageTitle_LoadItem = "Load_ElmahType_ItemVM";
        public const string MessageTitle_AssignItem = "Assign_ElmahType_ItemVM";

        public override Elmah.SQLite.ElmahTypeRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ElmahTypeRepository;
            }
        }

        public ItemVM()
            : base()
        {
            // Keep MessagingCenter.Subscribe here
            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahType.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahTypeIdentifier>>(
                this
                , MessageTitle_LoadItem
                , async (sender, request) =>{
                    await ProcessLoadItemRequest(request);
                });

            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahType.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahType>>(
                this
                , MessageTitle_AssignItem
                , async (sender, request) =>{
                    await ProcessAssignItemRequest(request);
                });
        }

        protected override async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> UpsertToServer()
        {
            var client = WebApiClientFactory.CreateElmahTypeApiClient();
            var item = Item;

            var result = await client.UpsertEntityAsync(item);
            return result;
        }

        /*
        protected override async Task DeleteFromServer()
        {
            var client = WebApiClientFactory.CreateElmahTypeApiClient();
            var item = Item;

            var result = await client.DeleteEntityAsync(item);
            this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
            this.StatusMessageOfResult = result.GetStatusMessage();
        }
        */

        protected override async Task<Elmah.ViewModelData.ElmahType.ItemVM> GetFromServer(Elmah.EntityContracts.IElmahTypeIdentifier identifier)
        {
            var client = WebApiClientFactory.CreateElmahTypeApiClient();
            var result = await client.GetItemVMAsync(identifier.Type);
            return result;
        }

        public override async Task LoadDropDownListContents()
        {

        }

        protected override void GetValueFromDropDownListSelectedItems()
        {

        }

        public override void SetDropDownListSelectedItems(Elmah.DataSourceEntities.ElmahType o)
        {

        }

        protected override Elmah.DataSourceEntities.ElmahType GetAClone(Elmah.DataSourceEntities.ElmahType item)
        {
            return item?.GetAClone<Elmah.DataSourceEntities.ElmahType>();
        }

        /*
        public override string GetThisItemDisplayString()
        {
            return Item != null ? string.Format("{0}({1})", Item. ?? ): string.Empty;
        }
        */

        public override string GetDisplayString(Elmah.DataSourceEntities.ElmahType item)
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


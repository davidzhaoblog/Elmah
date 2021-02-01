using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels.ElmahUser
{
    /// <summary>
    /// This class contains single item view model
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </summary>
    public partial class ItemVM
        : Framework.Xaml.ViewModelItemBase<Elmah.DataSourceEntities.ElmahUserIdentifier, Elmah.EntityContracts.IElmahUserIdentifier, Elmah.DataSourceEntities.ElmahUser, Elmah.ViewModelData.ElmahUser.ItemVM, Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn, Elmah.SQLite.ElmahUserRepository, Elmah.SQLite.TableModels.ElmahUser>
    {
        public const string MessageTitle_LoadItem = "Load_ElmahUser_ItemVM";
        public const string MessageTitle_AssignItem = "Assign_ElmahUser_ItemVM";

        public override Elmah.SQLite.ElmahUserRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ElmahUserRepository;
            }
        }

        public ItemVM()
            : base()
        {
            // Keep MessagingCenter.Subscribe here
            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahUser.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahUserIdentifier>>(
                this
                , MessageTitle_LoadItem
                , async (sender, request) =>{
                    await ProcessLoadItemRequest(request);
                });

            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahUser.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahUser>>(
                this
                , MessageTitle_AssignItem
                , async (sender, request) =>{
                    await ProcessAssignItemRequest(request);
                });
        }

        protected override async Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> UpsertToServer()
        {
            var client = WebApiClientFactory.CreateElmahUserApiClient();
            var item = Item;

            var result = await client.UpsertEntityAsync(item);
            return result;
        }

        /*
        protected override async Task DeleteFromServer()
        {
            var client = WebApiClientFactory.CreateElmahUserApiClient();
            var item = Item;

            var result = await client.DeleteEntityAsync(item);
            this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
            this.StatusMessageOfResult = result.GetStatusMessage();
        }
        */

        protected override async Task<Elmah.ViewModelData.ElmahUser.ItemVM> GetFromServer(Elmah.EntityContracts.IElmahUserIdentifier identifier)
        {
            var client = WebApiClientFactory.CreateElmahUserApiClient();
            var result = await client.GetItemVMAsync(identifier.User);
            return result;
        }

        public override async Task LoadDropDownListContents()
        {

        }

        protected override void GetValueFromDropDownListSelectedItems()
        {

        }

        public override void SetDropDownListSelectedItems(Elmah.DataSourceEntities.ElmahUser o)
        {

        }

        protected override Elmah.DataSourceEntities.ElmahUser GetAClone(Elmah.DataSourceEntities.ElmahUser item)
        {
            return item?.GetAClone<Elmah.DataSourceEntities.ElmahUser>();
        }

        /*
        public override string GetThisItemDisplayString()
        {
            return Item != null ? string.Format("{0}({1})", Item. ?? ): string.Empty;
        }
        */

        public override string GetDisplayString(Elmah.DataSourceEntities.ElmahUser item)
        {
            return item != null ? string.Format("{0}", item.User ): string.Empty;
        }

        /*
        public override void Cleanup()
        {
            base.Cleanup();
        }
        */
    }
}


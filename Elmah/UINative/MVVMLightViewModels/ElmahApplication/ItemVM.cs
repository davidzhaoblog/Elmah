using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels.ElmahApplication
{
    /// <summary>
    /// This class contains single item view model
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </summary>
    public partial class ItemVM
        : Framework.Xaml.ViewModelItemBase<Elmah.DataSourceEntities.ElmahApplicationIdentifier, Elmah.EntityContracts.IElmahApplicationIdentifier, Elmah.DataSourceEntities.ElmahApplication, Elmah.ViewModelData.ElmahApplication.ItemVM, Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn, Elmah.SQLite.ElmahApplicationRepository, Elmah.SQLite.TableModels.ElmahApplication>
    {
        public const string MessageTitle_LoadItem = "Load_ElmahApplication_ItemVM";
        public const string MessageTitle_AssignItem = "Assign_ElmahApplication_ItemVM";

        public override Elmah.SQLite.ElmahApplicationRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ElmahApplicationRepository;
            }
        }

        public ItemVM()
            : base()
        {
            // Keep MessagingCenter.Subscribe here
            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier>>(
                this
                , MessageTitle_LoadItem
                , async (sender, request) =>{
                    await ProcessLoadItemRequest(request);
                });

            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplication>>(
                this
                , MessageTitle_AssignItem
                , async (sender, request) =>{
                    await ProcessAssignItemRequest(request);
                });
        }

        protected override async Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> UpsertToServer()
        {
            var client = WebApiClientFactory.CreateElmahApplicationApiClient();
            var item = Item;

            var result = await client.UpsertEntityAsync(item);
            return result;
        }

        /*
        protected override async Task DeleteFromServer()
        {
            var client = WebApiClientFactory.CreateElmahApplicationApiClient();
            var item = Item;

            var result = await client.DeleteEntityAsync(item);
            this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
            this.StatusMessageOfResult = result.GetStatusMessage();
        }
        */

        protected override async Task<Elmah.ViewModelData.ElmahApplication.ItemVM> GetFromServer(Elmah.EntityContracts.IElmahApplicationIdentifier identifier)
        {
            var client = WebApiClientFactory.CreateElmahApplicationApiClient();
            var result = await client.GetItemVMAsync(identifier.Application);
            return result;
        }

        public override async Task LoadDropDownListContents()
        {

        }

        protected override void GetValueFromDropDownListSelectedItems()
        {

        }

        public override void SetDropDownListSelectedItems(Elmah.DataSourceEntities.ElmahApplication o)
        {

        }

        protected override Elmah.DataSourceEntities.ElmahApplication GetAClone(Elmah.DataSourceEntities.ElmahApplication item)
        {
            return item?.GetAClone<Elmah.DataSourceEntities.ElmahApplication>();
        }

        /*
        public override string GetThisItemDisplayString()
        {
            return Item != null ? string.Format("{0}({1})", Item. ?? ): string.Empty;
        }
        */

        public override string GetDisplayString(Elmah.DataSourceEntities.ElmahApplication item)
        {
            return item != null ? string.Format("{0}", item.Application ): string.Empty;
        }

        /*
        public override void Cleanup()
        {
            base.Cleanup();
        }
        */
    }
}


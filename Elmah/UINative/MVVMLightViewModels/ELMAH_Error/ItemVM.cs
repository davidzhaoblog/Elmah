using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels.ELMAH_Error
{
    /// <summary>
    /// This class contains single item view model
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </summary>
    public partial class ItemVM
        : Framework.Xaml.ViewModelItemBase<Elmah.DataSourceEntities.ELMAH_ErrorIdentifier, Elmah.EntityContracts.IELMAH_ErrorIdentifier, Elmah.DataSourceEntities.ELMAH_Error.Default, Elmah.ViewModelData.ELMAH_Error.ItemVM, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default, Elmah.SQLite.ELMAH_ErrorRepository, Elmah.SQLite.TableModels.ELMAH_Error>
    {
        public const string MessageTitle_LoadItem = "Load_ELMAH_Error_ItemVM";
        public const string MessageTitle_AssignItem = "Assign_ELMAH_Error_ItemVM";

        public override Elmah.SQLite.ELMAH_ErrorRepository CacheInstance
        {
            get
            {
                return DependencyService.Resolve<SQLiteFactory>().ELMAH_ErrorRepository;
            }
        }

        public ExtendedVM ExtendedVM
        {
            get
            {
                return DependencyService.Resolve<ExtendedVM>();
            }
        }

        public ItemVM()
            : base()
        {
            // Keep MessagingCenter.Subscribe here
            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ELMAH_ErrorIdentifier>>(
                this
                , MessageTitle_LoadItem
                , async (sender, request) =>{
                    await ProcessLoadItemRequest(request);
                });

            MessagingCenter.Subscribe<Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ELMAH_Error.Default>>(
                this
                , MessageTitle_AssignItem
                , async (sender, request) =>{
                    await ProcessAssignItemRequest(request);
                });
        }

        protected override async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> UpsertToServer()
        {
            var client = WebApiClientFactory.CreateELMAH_ErrorApiClient();
            var item = new Elmah.DataSourceEntities.ELMAH_Error();
                item.CopyFrom<Elmah.EntityContracts.IELMAH_Error>(Item);

            var result = await client.UpsertEntityAsync(item);
            return result;
        }

        /*
        protected override async Task DeleteFromServer()
        {
            var client = WebApiClientFactory.CreateELMAH_ErrorApiClient();
            var item = new Elmah.DataSourceEntities.ELMAH_Error();
                item.CopyFrom<Elmah.EntityContracts.IELMAH_Error>(Item);

            var result = await client.DeleteEntityAsync(item);
            this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
            this.StatusMessageOfResult = result.GetStatusMessage();
        }
        */

        protected override async Task<Elmah.ViewModelData.ELMAH_Error.ItemVM> GetFromServer(Elmah.EntityContracts.IELMAH_ErrorIdentifier identifier)
        {
            var client = WebApiClientFactory.CreateELMAH_ErrorApiClient();
            var result = await client.GetItemVMAsync(identifier.ErrorId);
            return result;
        }

        public override async Task LoadDropDownListContents()
        {

            await this.ExtendedVM.GetDropDownContentsOfElmahApplication();

            await this.ExtendedVM.GetDropDownContentsOfElmahHost();

            await this.ExtendedVM.GetDropDownContentsOfElmahSource();

            await this.ExtendedVM.GetDropDownContentsOfElmahStatusCode();

            await this.ExtendedVM.GetDropDownContentsOfElmahType();

            await this.ExtendedVM.GetDropDownContentsOfElmahUser();

        }

        protected override void GetValueFromDropDownListSelectedItems()
        {

            this.Item.User = this.ExtendedVM.DropDownContentsOfElmahUserSelectedItem.Value;

            this.Item.Type = this.ExtendedVM.DropDownContentsOfElmahTypeSelectedItem.Value;

            this.Item.StatusCode = this.ExtendedVM.DropDownContentsOfElmahStatusCodeSelectedItem.Value;

            this.Item.Source = this.ExtendedVM.DropDownContentsOfElmahSourceSelectedItem.Value;

            this.Item.Host = this.ExtendedVM.DropDownContentsOfElmahHostSelectedItem.Value;

            this.Item.Application = this.ExtendedVM.DropDownContentsOfElmahApplicationSelectedItem.Value;

        }

        public override void SetDropDownListSelectedItems(Elmah.DataSourceEntities.ELMAH_Error.Default o)
        {

            this.ExtendedVM.DropDownContentsOfElmahUserSelectedItem = new Framework.Models.NameValuePair<string>(o.User, o.ElmahUser_Name);

            this.ExtendedVM.DropDownContentsOfElmahTypeSelectedItem = new Framework.Models.NameValuePair<string>(o.Type, o.ElmahType_Name);

            this.ExtendedVM.DropDownContentsOfElmahStatusCodeSelectedItem = new Framework.Models.NameValuePair<int>(o.StatusCode, o.ElmahStatusCode_Name);

            this.ExtendedVM.DropDownContentsOfElmahSourceSelectedItem = new Framework.Models.NameValuePair<string>(o.Source, o.ElmahSource_Name);

            this.ExtendedVM.DropDownContentsOfElmahHostSelectedItem = new Framework.Models.NameValuePair<string>(o.Host, o.ElmahHost_Name);

            this.ExtendedVM.DropDownContentsOfElmahApplicationSelectedItem = new Framework.Models.NameValuePair<string>(o.Application, o.ElmahApplication_Name);

        }

        protected override Elmah.DataSourceEntities.ELMAH_Error.Default GetAClone(Elmah.DataSourceEntities.ELMAH_Error.Default item)
        {
            return item?.GetAClone<Elmah.DataSourceEntities.ELMAH_Error.Default>();
        }

        /*
        public override string GetThisItemDisplayString()
        {
            return Item != null ? string.Format("{0}({1})", Item. ?? ): string.Empty;
        }
        */

        public override string GetDisplayString(Elmah.DataSourceEntities.ELMAH_Error.Default item)
        {
            return item != null ? string.Format("{0}", item.Message ): string.Empty;
        }

        /*
        public override void Cleanup()
        {
            base.Cleanup();
        }
        */
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Elmah.AspNetMvcCoreViewModel.ELMAH_Error
{
    public partial class IndexVM : Elmah.ViewModelData.ELMAH_Error.IndexVM
    {
        //private readonly ILogger _logger;

        public IndexVM()
        {
        }

        private IServiceProvider ServiceProvider { get; set; }
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public Elmah.AspNetMvcCoreViewModel.UISharedViewModel UISharedViewModel { get; set; }
        public List<SelectListItem> SelectListOfElmahModel_ElmahApplication { get; set; }

        public List<SelectListItem> SelectListOfElmahModel_ElmahHost { get; set; }

        public List<SelectListItem> SelectListOfElmahModel_ElmahSource { get; set; }

        public List<SelectListItem> SelectListOfElmahModel_ElmahStatusCode { get; set; }

        public List<SelectListItem> SelectListOfElmahModel_ElmahType { get; set; }

        public List<SelectListItem> SelectListOfElmahModel_ElmahUser { get; set; }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ELMAH_Error_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }

        public override async Task GetDefaultPerViewModel()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                    var service_ElmahApplication = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahApplicationService>();
                    var responseOfElmahModel_ElmahApplication = await service_ElmahApplication.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahApplication = responseOfElmahModel_ElmahApplication.Message;
                    this.SelectListOfElmahModel_ElmahApplication = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahApplication);

                    var service_ElmahHost = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahHostService>();
                    var responseOfElmahModel_ElmahHost = await service_ElmahHost.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahHost = responseOfElmahModel_ElmahHost.Message;
                    this.SelectListOfElmahModel_ElmahHost = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahHost);

                    var service_ElmahSource = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahSourceService>();
                    var responseOfElmahModel_ElmahSource = await service_ElmahSource.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahSource = responseOfElmahModel_ElmahSource.Message;
                    this.SelectListOfElmahModel_ElmahSource = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahSource);

                    var service_ElmahStatusCode = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahStatusCodeService>();
                    var responseOfElmahModel_ElmahStatusCode = await service_ElmahStatusCode.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahStatusCode = responseOfElmahModel_ElmahStatusCode.Message;
                    this.SelectListOfElmahModel_ElmahStatusCode = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahStatusCode);

                    var service_ElmahType = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahTypeService>();
                    var responseOfElmahModel_ElmahType = await service_ElmahType.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahType = responseOfElmahModel_ElmahType.Message;
                    this.SelectListOfElmahModel_ElmahType = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahType);

                    var service_ElmahUser = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahUserService>();
                    var responseOfElmahModel_ElmahUser = await service_ElmahUser.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahUser = responseOfElmahModel_ElmahUser.Message;
                    this.SelectListOfElmahModel_ElmahUser = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahUser);

            }

            this.UISharedViewModel = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.GetUISharedViewModel(this.ListOfQueryOrderBySettingCollecionInString, this.QueryPagingSetting.PageSizeSelectionList, this.ListOfDataExport);
        }

        //public override void LoadData()
        /// <summary>
        /// Loads the data.
        /// </summary>
        public async Task LoadData(bool isToLoadDropDownlistContent)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                if (isToLoadDropDownlistContent)
                {
                    var service_ElmahApplication = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahApplicationService>();
                    var responseOfElmahModel_ElmahApplication = await service_ElmahApplication.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahApplication = responseOfElmahModel_ElmahApplication.Message;
                    this.SelectListOfElmahModel_ElmahApplication = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahApplication);

                    var service_ElmahHost = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahHostService>();
                    var responseOfElmahModel_ElmahHost = await service_ElmahHost.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahHost = responseOfElmahModel_ElmahHost.Message;
                    this.SelectListOfElmahModel_ElmahHost = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahHost);

                    var service_ElmahSource = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahSourceService>();
                    var responseOfElmahModel_ElmahSource = await service_ElmahSource.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahSource = responseOfElmahModel_ElmahSource.Message;
                    this.SelectListOfElmahModel_ElmahSource = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahSource);

                    var service_ElmahStatusCode = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahStatusCodeService>();
                    var responseOfElmahModel_ElmahStatusCode = await service_ElmahStatusCode.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahStatusCode = responseOfElmahModel_ElmahStatusCode.Message;
                    this.SelectListOfElmahModel_ElmahStatusCode = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahStatusCode);

                    var service_ElmahType = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahTypeService>();
                    var responseOfElmahModel_ElmahType = await service_ElmahType.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahType = responseOfElmahModel_ElmahType.Message;
                    this.SelectListOfElmahModel_ElmahType = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahType);

                    var service_ElmahUser = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahUserService>();
                    var responseOfElmahModel_ElmahUser = await service_ElmahUser.GetMessageOfNameValuePairByCommon(new Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaCommon() { CanQueryWhenNoQuery = true }, new Framework.Queries.QueryPagingSetting(-1, -1), null);
                    this.NameValueCollectionOfElmahModel_ElmahUser = responseOfElmahModel_ElmahUser.Message;
                    this.SelectListOfElmahModel_ElmahUser = Elmah.AspNetMvcCoreViewModel.UISharedViewModel.BuildListOfSelectListItem(this.NameValueCollectionOfElmahModel_ElmahUser);

                }
                this.Criteria.CanQueryWhenNoQuery = true;

                this.Criteria.Common.UpdateStringContainsCriteria();

                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IELMAH_ErrorService>();
                var searchResult = await thisService.GetMessageOfDefaultByCommon(
                    this.Criteria
                    , this.QueryPagingSetting
                    , this.QueryOrderBySettingCollection);

                this.StatusOfResult = searchResult.BusinessLogicLayerResponseStatus;

                if (this.StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                {
                    this.Result = searchResult.Message;

                    if (searchResult.QueryPagingResult != null)
                    {
                        this.QueryPagingSetting.CountOfRecords = searchResult.QueryPagingResult.CountOfRecords;
                        this.QueryPagingSetting.RecordCountOfCurrentPage = searchResult.QueryPagingResult.RecordCountOfCurrentPage;
                    }
                }
                else
                {
                    this.StatusMessageOfResult = searchResult.GetStatusMessage();
#if DEBUG
                    this.StatusMessageOfResult = string.Format("{0} {1}", this.StatusMessageOfResult, searchResult.ServerErrorMessage);
#endif
                }
            }
        }
    }
}


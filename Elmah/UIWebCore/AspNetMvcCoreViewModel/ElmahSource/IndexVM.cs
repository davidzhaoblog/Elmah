using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Elmah.AspNetMvcCoreViewModel.ElmahSource
{
    public partial class IndexVM : Elmah.ViewModelData.ElmahSource.IndexVM
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

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ElmahSource_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }

        public override async Task GetDefaultPerViewModel()
        {
            using (var scope = ServiceProvider.CreateScope())
            {

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

                }
                this.Criteria.CanQueryWhenNoQuery = true;

                this.Criteria.Common.UpdateStringContainsCriteria();

                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahSourceService>();
                var searchResult = await thisService.GetMessageOfEntityByCommon(
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


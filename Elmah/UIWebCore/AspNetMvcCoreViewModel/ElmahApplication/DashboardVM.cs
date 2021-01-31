using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Elmah.AspNetMvcCoreViewModel.ElmahApplication
{
    public partial class DashboardVM
        : Elmah.ViewModelData.ElmahApplication.DashboardVM
    {
        //private readonly ILogger _logger;

        public DashboardVM()
            : base()
        {
        }

        private IServiceProvider ServiceProvider { get; set; }
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public async Task LoadData(Elmah.ViewModelData.ElmahApplication.DashboardVMDataOptions[] dataOptions)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                // 1. master on accessory part - Aside UIWorkspaceItemSetting
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahApplicationService>();
                var masterEntityResult = await thisService.GetMessageOfEntityByIdentifier(this.CriteriaOfMasterEntity, this.QueryPagingSettingOneRecord, null);
                MasterEntityResponseStatus = new Framework.ViewModels.ResponseStatus
                {
                    RequestType = Framework.Services.BusinessLogicLayerRequestTypes.Search,
                    Status = masterEntityResult.BusinessLogicLayerResponseStatus,
                    StatusMessage = masterEntityResult.GetStatusMessage()
                };

                if (masterEntityResult.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                {
                    this.MasterEntity = masterEntityResult.Message[0];

                    var tasks = new List<Task>();

                    // 2. accessory part - Aside UIWorkspaceItemSetting -- RelatedEntityWhenMasterViewIsFKEntity

                    // 3. Major part - Article UIWorkspaceItemSetting - EntityReference/FK downtree -- RelatedEntityWhenMasterViewIsPKEntity

                    // ElmahModel.ELMAH_Error
                    if(dataOptions == null || dataOptions.Contains(Elmah.ViewModelData.ElmahApplication.DashboardVMDataOptions.ELMAH_Error))
                    {
                        tasks.Add(Task.Run(async () => {
                        this.CriteriaOfELMAH_Error.Common.Application = new Framework.Queries.QuerySystemStringEqualsCriteria(this.MasterEntity.Application);
                            var service_ELMAH_Error = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IELMAH_ErrorService>();
                            var resultELMAH_Error = await service_ELMAH_Error.GetMessageOfDefaultByCommon(this.CriteriaOfELMAH_Error, this.QueryPagingSetting, null);
                            this.ResponseStatuses.Add(resultELMAH_Error.GetStatusMessage<Elmah.ViewModelData.ElmahApplication.DashboardVMDataOptions>(
                                Elmah.ViewModelData.ElmahApplication.DashboardVMDataOptions.ELMAH_Error, Framework.Services.BusinessLogicLayerRequestTypes.Search));

                            // TODO: Keep them for now, before asp.net mvc core code fixed.
                            this.StatusOfELMAH_Error = resultELMAH_Error.BusinessLogicLayerResponseStatus;
                            if (resultELMAH_Error.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                            {
                                this.ELMAH_Error = resultELMAH_Error.Message;
                            }
                            else
                            {
                                this.StatusMessageOfELMAH_Error = resultELMAH_Error.GetStatusMessage();
#if DEBUG
                                this.StatusMessageOfELMAH_Error = string.Format("{0} {1}", this.StatusMessageOfELMAH_Error, resultELMAH_Error.ServerErrorMessage);
#endif
                            }
                        }));
                    }

                    if (tasks.Count > 0)
                    {
                        Task t = Task.WhenAll(tasks.ToArray());
                        try
                        {
                            await t;
                        }
                        catch { }
                    }
                }
                else
                {
                    this.StatusMessageOfMasterEntity = masterEntityResult.GetStatusMessage();
#if DEBUG
                    this.StatusMessageOfMasterEntity = string.Format("{0} {1}", this.StatusMessageOfMasterEntity, masterEntityResult.ServerErrorMessage);
#endif
                }
            }
        }
    }
}


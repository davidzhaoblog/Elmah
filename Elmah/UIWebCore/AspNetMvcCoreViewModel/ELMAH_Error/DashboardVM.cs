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
    public partial class DashboardVM
        : Elmah.ViewModelData.ELMAH_Error.DashboardVM
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

        public async Task LoadData(Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions[] dataOptions)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                // 1. master on accessory part - Aside UIWorkspaceItemSetting
                var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IELMAH_ErrorService>();
                var masterEntityResult = await thisService.GetMessageOfDefaultByIdentifier(this.CriteriaOfMasterEntity, this.QueryPagingSettingOneRecord, null);
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

                    // ElmahModel.ElmahApplication
                    if(dataOptions == null || dataOptions.Contains(Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahApplication))
                    {
                        tasks.Add(Task.Run(async () => {
                        this.CriteriaOfElmahApplication.Identifier.Application = new Framework.Queries.QuerySystemStringEqualsCriteria(this.MasterEntity.Application);
                            var service_ElmahApplication = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahApplicationService>();
                            var resultElmahApplication = await service_ElmahApplication.GetMessageOfEntityByIdentifier(this.CriteriaOfElmahApplication, this.QueryPagingSettingOneRecord, null);

                            this.ResponseStatuses.Add(resultElmahApplication.GetStatusMessage<Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions>(
                                Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahApplication, Framework.Services.BusinessLogicLayerRequestTypes.Search));

                            // TODO: Keep them for now, before asp.net mvc core code fixed.
                            this.StatusOfElmahApplication = resultElmahApplication.BusinessLogicLayerResponseStatus;
                            if (resultElmahApplication.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                            {
                                this.ElmahApplication = resultElmahApplication.Message[0];
                            }
                            else
                            {
                                this.StatusMessageOfElmahApplication = resultElmahApplication.GetStatusMessage();
#if DEBUG
                                this.StatusMessageOfElmahApplication = string.Format("this.ThisService GetMessageOfEntityByIdentifier", this.StatusMessageOfElmahApplication, resultElmahApplication.ServerErrorMessage);
#endif
                            }
                        }));
                    }

                    // ElmahModel.ElmahHost
                    if(dataOptions == null || dataOptions.Contains(Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahHost))
                    {
                        tasks.Add(Task.Run(async () => {
                        this.CriteriaOfElmahHost.Identifier.Host = new Framework.Queries.QuerySystemStringEqualsCriteria(this.MasterEntity.Host);
                            var service_ElmahHost = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahHostService>();
                            var resultElmahHost = await service_ElmahHost.GetMessageOfEntityByIdentifier(this.CriteriaOfElmahHost, this.QueryPagingSettingOneRecord, null);

                            this.ResponseStatuses.Add(resultElmahHost.GetStatusMessage<Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions>(
                                Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahHost, Framework.Services.BusinessLogicLayerRequestTypes.Search));

                            // TODO: Keep them for now, before asp.net mvc core code fixed.
                            this.StatusOfElmahHost = resultElmahHost.BusinessLogicLayerResponseStatus;
                            if (resultElmahHost.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                            {
                                this.ElmahHost = resultElmahHost.Message[0];
                            }
                            else
                            {
                                this.StatusMessageOfElmahHost = resultElmahHost.GetStatusMessage();
#if DEBUG
                                this.StatusMessageOfElmahHost = string.Format("this.ThisService GetMessageOfEntityByIdentifier", this.StatusMessageOfElmahHost, resultElmahHost.ServerErrorMessage);
#endif
                            }
                        }));
                    }

                    // ElmahModel.ElmahSource
                    if(dataOptions == null || dataOptions.Contains(Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahSource))
                    {
                        tasks.Add(Task.Run(async () => {
                        this.CriteriaOfElmahSource.Identifier.Source = new Framework.Queries.QuerySystemStringEqualsCriteria(this.MasterEntity.Source);
                            var service_ElmahSource = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahSourceService>();
                            var resultElmahSource = await service_ElmahSource.GetMessageOfEntityByIdentifier(this.CriteriaOfElmahSource, this.QueryPagingSettingOneRecord, null);

                            this.ResponseStatuses.Add(resultElmahSource.GetStatusMessage<Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions>(
                                Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahSource, Framework.Services.BusinessLogicLayerRequestTypes.Search));

                            // TODO: Keep them for now, before asp.net mvc core code fixed.
                            this.StatusOfElmahSource = resultElmahSource.BusinessLogicLayerResponseStatus;
                            if (resultElmahSource.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                            {
                                this.ElmahSource = resultElmahSource.Message[0];
                            }
                            else
                            {
                                this.StatusMessageOfElmahSource = resultElmahSource.GetStatusMessage();
#if DEBUG
                                this.StatusMessageOfElmahSource = string.Format("this.ThisService GetMessageOfEntityByIdentifier", this.StatusMessageOfElmahSource, resultElmahSource.ServerErrorMessage);
#endif
                            }
                        }));
                    }

                    // ElmahModel.ElmahStatusCode
                    if(dataOptions == null || dataOptions.Contains(Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahStatusCode))
                    {
                        tasks.Add(Task.Run(async () => {
                        this.CriteriaOfElmahStatusCode.Identifier.StatusCode = new Framework.Queries.QuerySystemInt32EqualsCriteria(this.MasterEntity.StatusCode);
                            var service_ElmahStatusCode = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahStatusCodeService>();
                            var resultElmahStatusCode = await service_ElmahStatusCode.GetMessageOfEntityByIdentifier(this.CriteriaOfElmahStatusCode, this.QueryPagingSettingOneRecord, null);

                            this.ResponseStatuses.Add(resultElmahStatusCode.GetStatusMessage<Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions>(
                                Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahStatusCode, Framework.Services.BusinessLogicLayerRequestTypes.Search));

                            // TODO: Keep them for now, before asp.net mvc core code fixed.
                            this.StatusOfElmahStatusCode = resultElmahStatusCode.BusinessLogicLayerResponseStatus;
                            if (resultElmahStatusCode.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                            {
                                this.ElmahStatusCode = resultElmahStatusCode.Message[0];
                            }
                            else
                            {
                                this.StatusMessageOfElmahStatusCode = resultElmahStatusCode.GetStatusMessage();
#if DEBUG
                                this.StatusMessageOfElmahStatusCode = string.Format("this.ThisService GetMessageOfEntityByIdentifier", this.StatusMessageOfElmahStatusCode, resultElmahStatusCode.ServerErrorMessage);
#endif
                            }
                        }));
                    }

                    // ElmahModel.ElmahType
                    if(dataOptions == null || dataOptions.Contains(Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahType))
                    {
                        tasks.Add(Task.Run(async () => {
                        this.CriteriaOfElmahType.Identifier.Type = new Framework.Queries.QuerySystemStringEqualsCriteria(this.MasterEntity.Type);
                            var service_ElmahType = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahTypeService>();
                            var resultElmahType = await service_ElmahType.GetMessageOfEntityByIdentifier(this.CriteriaOfElmahType, this.QueryPagingSettingOneRecord, null);

                            this.ResponseStatuses.Add(resultElmahType.GetStatusMessage<Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions>(
                                Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahType, Framework.Services.BusinessLogicLayerRequestTypes.Search));

                            // TODO: Keep them for now, before asp.net mvc core code fixed.
                            this.StatusOfElmahType = resultElmahType.BusinessLogicLayerResponseStatus;
                            if (resultElmahType.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                            {
                                this.ElmahType = resultElmahType.Message[0];
                            }
                            else
                            {
                                this.StatusMessageOfElmahType = resultElmahType.GetStatusMessage();
#if DEBUG
                                this.StatusMessageOfElmahType = string.Format("this.ThisService GetMessageOfEntityByIdentifier", this.StatusMessageOfElmahType, resultElmahType.ServerErrorMessage);
#endif
                            }
                        }));
                    }

                    // ElmahModel.ElmahUser
                    if(dataOptions == null || dataOptions.Contains(Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahUser))
                    {
                        tasks.Add(Task.Run(async () => {
                        this.CriteriaOfElmahUser.Identifier.User = new Framework.Queries.QuerySystemStringEqualsCriteria(this.MasterEntity.User);
                            var service_ElmahUser = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahUserService>();
                            var resultElmahUser = await service_ElmahUser.GetMessageOfEntityByIdentifier(this.CriteriaOfElmahUser, this.QueryPagingSettingOneRecord, null);

                            this.ResponseStatuses.Add(resultElmahUser.GetStatusMessage<Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions>(
                                Elmah.ViewModelData.ELMAH_Error.DashboardVMDataOptions.ElmahUser, Framework.Services.BusinessLogicLayerRequestTypes.Search));

                            // TODO: Keep them for now, before asp.net mvc core code fixed.
                            this.StatusOfElmahUser = resultElmahUser.BusinessLogicLayerResponseStatus;
                            if (resultElmahUser.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                            {
                                this.ElmahUser = resultElmahUser.Message[0];
                            }
                            else
                            {
                                this.StatusMessageOfElmahUser = resultElmahUser.GetStatusMessage();
#if DEBUG
                                this.StatusMessageOfElmahUser = string.Format("this.ThisService GetMessageOfEntityByIdentifier", this.StatusMessageOfElmahUser, resultElmahUser.ServerErrorMessage);
#endif
                            }
                        }));
                    }

                    // 3. Major part - Article UIWorkspaceItemSetting - EntityReference/FK downtree -- RelatedEntityWhenMasterViewIsPKEntity

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


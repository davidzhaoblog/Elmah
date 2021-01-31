using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Elmah.AspNetMvcCoreViewModel.ElmahUser
{
    public partial class ItemVM : Elmah.ViewModelData.ElmahUser.ItemVM
    {
        private readonly ILogger _logger;

        public ItemVM()
        {
        }

        public ItemVM(ILogger<ItemVM> logger)
        {
            this._logger = logger;
        }

        private IServiceProvider ServiceProvider { get; set; }
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public virtual async Task Load(bool isToCompareUser, string user
            , Framework.ViewModels.UIAction uiAction)
        {
            using (var scope = this.ServiceProvider.CreateScope())
            {
                try
                {
                    //log.Info(string.Format("{0}: Details", Framework.LoggingOptions.UI_Process_Started.ToString()));

                    var thisService = scope.ServiceProvider.GetRequiredService<Elmah.WcfContracts.IElmahUserService>();
                    var _Response = await thisService.GetMessageOfEntityByIdentifier(isToCompareUser, user, -1, -1, null);

                    if (_Response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || _Response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                    {
                        this.Item = _Response.Message[0];
                        this.UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage(typeof(ItemVM).FullName, uiAction.ToString(), uiAction, Framework.ViewModels.UIActionStatus.Launch);
                        //log.Info(string.Format("{0}: {1}", uiAction, Framework.Models.LoggingOptions.UI_Process_Succeeded.ToString()));
                        await this.LoadExtraData(uiAction);
                    }
                    else
                    {
                        this.StatusOfResult = _Response.BusinessLogicLayerResponseStatus;
                        this.StatusMessageOfResult = _Response.ServerErrorMessage;
                        this.UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage(typeof(ItemVM).FullName, uiAction.ToString(), uiAction, Framework.ViewModels.UIActionStatus.Failed);
                        //log.Error(string.Format("{0}: {1}, {2}, {3}", uiAction, Framework.Models.LoggingOptions.UI_Process_Failed.ToString(), this.StatusOfResult, this.StatusMessageOfResult));
                    }
                }
                catch (Exception ex)
                {
                    this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    this.StatusMessageOfResult = ex.Message;
                    this.UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage(typeof(ItemVM).FullName, uiAction.ToString(), uiAction, Framework.ViewModels.UIActionStatus.Failed);
                    //log.Error(string.Format("{0}: {1}, {2}, {3}", uiAction, Framework.Models.LoggingOptions.UI_Process_Failed.ToString(), this.StatusOfResult, this.StatusMessageOfResult));
                }
            }
        }

        public static async Task<ItemVM> CreateNewViewModel(IServiceProvider serviceProvider, Elmah.DataSourceEntities.ElmahUser entity)
        {
            return await CreateNewViewModel<ItemVM>(serviceProvider, entity);
        }

        public static async Task<T> CreateNewViewModel<T>(IServiceProvider serviceProvider, Elmah.DataSourceEntities.ElmahUser entity)
            where T: ItemVM, new()
        {
            var uiAction = Framework.ViewModels.UIAction.Create;
            T vm = new T();
            vm.ContentData.Title = string.Format("{0} {1}", Framework.Resx.UIStringResource.AddNew, Elmah.Resx.UIStringResourcePerApp.ElmahUser);
            //TODO: create a new new entry in xxx.UIStringResourceExt.resx file if you need, and uncomment next line, reference resource key here, then add in .cshtml file.
            //vm.ContentData.Summary = string.Format("{0} {1}", Framework.Resx.UIStringResource.AddNew, Elmah.Resx.UIStringResourcePerApp.ElmahUser);
            vm.ServiceProvider = serviceProvider;
            await vm.LoadExtraData(uiAction);

            vm.Item = entity;
            vm.UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage(typeof(ItemVM).FullName, uiAction.ToString(), uiAction, Framework.ViewModels.UIActionStatus.Launch);

            return vm;
        }

        public async Task LoadExtraData(Framework.ViewModels.UIAction uiAction)
        {
            if (uiAction != Framework.ViewModels.UIAction.ViewDetails)
            {
                using (var scope = this.ServiceProvider.CreateScope())
                {

                }
            }
        }
    }
}


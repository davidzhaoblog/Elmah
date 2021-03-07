using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using X.PagedList;

namespace Elmah.MvcCore.Controllers
{
    /// <summary>
    /// Mvc Controller of  <see cref="ElmahModel.ElmahApplication"/>
    /// </summary>
    //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
    public partial class ElmahApplicationController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public ElmahApplicationController(IServiceProvider serviceProvider, ILogger<ElmahApplicationController> logger)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }

        public const string TempDataKey_Index = "TempDataKey_ElmahModel.ElmahApplication_Index";
        /// <summary>
        /// Controller Method of View Index
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> Index(int currentPage = 1, Elmah.AspNetMvcCoreViewModel.ElmahApplication.IndexVM viewModel = null)
        {
            //log.Info(string.Format("{0}: Index", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));

            Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon> vmFromTempData;
            bool httpPost = Request.Method == "POST";
            if (!TempData.ContainsKey(TempDataKey_Index))
            {
                vmFromTempData = null;
            }
            else
            {
                vmFromTempData = (Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon>)JsonConvert.DeserializeObject<Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon>>(TempData[TempDataKey_Index].ToString());
            }

            viewModel.SetServiceProvider(this._serviceProvider);

            viewModel.PopulateAllUIElements(vmFromTempData, currentPage, !httpPost);
            await viewModel.LoadData(true);

            TempData[TempDataKey_Index] = JsonConvert.SerializeObject(viewModel.GetPrimaryInformationEntity());
            TempData.Keep(TempDataKey_Index);

            if (viewModel.Result != null)
            {
                ViewBag.StaticPagedResult = new StaticPagedList<Elmah.DataSourceEntities.ElmahApplication>(viewModel.Result, viewModel.QueryPagingSetting.CurrentPage, viewModel.QueryPagingSetting.PageSize, viewModel.QueryPagingSetting.CountOfRecords);
            }

            return View(viewModel);
        }

        /// <summary>
        /// Export current search result.
        /// </summary>
        /// <param name="dataServiceType">Type of the data service.</param>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> Index_Export(Framework.Models.DataServiceTypes dataServiceType)
        {
            //log.Info(string.Format("{0}: Index_Export", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));

            Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon> vmFromTempData;
            if (TempData.ContainsKey(TempDataKey_Index))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    vmFromTempData = (Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon>)JsonConvert.DeserializeObject<Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon>>(TempData[TempDataKey_Index].ToString());
                    var serviceInstance = (Elmah.WcfContracts.IElmahApplicationService)scope.ServiceProvider.GetRequiredService(typeof(Elmah.WcfContracts.IElmahApplicationService));
                    var searchResult = await serviceInstance.GetMessageOfEntityByCommon(
                        vmFromTempData.Criteria
                        , new Framework.Queries.QueryPagingSetting(-1, -1)
                        , new Framework.Queries.QueryOrderBySettingCollection(vmFromTempData.QueryOrderBySettingCollecionInString)
                        , dataServiceType);

                    var result = searchResult.DataStreamServiceResult;

                    TempData[TempDataKey_Index] = JsonConvert.SerializeObject(vmFromTempData.GetPrimaryInformationEntity());
                    TempData.Keep(TempDataKey_Index);

                    return File(result.Result, result.MIMEType, string.Format("Index_Export{0}{1}", result.DataServiceType, result.FileExtension));
                }
            }

            return null;
        }

        /// <summary>
        /// Display one entity and all related entities if any, either single item or a list, based on foreign keys
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> Dashboard(string application = default(string))
        {
            var criteria = new Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaIdentifier();
            criteria.Identifier.Application.NullableValueToCompare = application;

            using (var scope = _serviceProvider.CreateScope())
            {
                var vm = (Elmah.AspNetMvcCoreViewModel.ElmahApplication.DashboardVM)scope.ServiceProvider.GetRequiredService(typeof(Elmah.AspNetMvcCoreViewModel.ElmahApplication.DashboardVM));

                vm.SetServiceProvider(this._serviceProvider);

                vm.CriteriaOfMasterEntity = criteria;
                //TODO: this will be changed when have time to re-do asp.net mvc core UI.
                await vm.LoadData(null);

                return View(vm);
            }
        }

        #region Import()

        /// <summary>
        /// Imports a list of entity from csv or excel file
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        [HttpGet()]
        public ActionResult Import()
        {
            ViewBag.Message = "";
            return View();
        }

        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        [HttpPost()]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        public async Task<ActionResult> Import(IFormFile file)
        {
            // The Import is commented out.
            // remove [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
            // if you make it working
            // or remove async to make this method sync method
            ViewBag.FileFormat = "Application";

            //log.Info(string.Format("{0}: Import", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));

            var dataStreamServiceResult = new Framework.Models.DataStreamServiceResult(file.FileName, file.ContentType, file.Length, file.OpenReadStream());
            //dataStreamServiceResult.TempFilePath = Framework.Mvc.WebFormApplicationApplicationVariables.FileStorageRootFolder;
            var dataStreamServiceProvider = new Elmah.CoreCommonBLL.ElmahApplicationDataStreamService();
            var collection = dataStreamServiceProvider.GetCollectionFromStream(dataStreamServiceResult);

            if (collection != null)
            {
                    //var resultCollection = collection;
                    //var result = Elmah.WcfContracts.IElmahApplicationService.BatchInsert(resultCollection);
                    //ViewBag.Message = Framework.Resx.UIStringResource.Data_Import_Success;
            }
            else
            {
                ViewBag.Message = Framework.Resx.UIStringResource.Data_Import_NoRecordInSourceFile;
            }

            //log.Info(string.Format("{0}: Import", Framework.Models.LoggingOptions.UI_Process_Ended.ToString()));

            return View();
        }

        #endregion Import()

        #region ActionResult Details(string application = default(string))

        /// <summary>
        /// GET method of details page, based on identifier or unique constraint, this entity only, no related entities.
        /// GET: /ElmahApplication/Details/5
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> Details(string application = default(string))
        {
            Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.ViewDetails;
            var vm = new Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM();
            vm.SetServiceProvider(this._serviceProvider);
            await vm.Load(!string.IsNullOrEmpty(application), application, uiAction);
            vm.ContentData.Title = string.Format("{0} {1}", Framework.Resx.UIStringResource.Details, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
            //TODO: create a new new entry in xxx.UIStringResourceExt.resx file if you need, and uncomment next line, reference resource key here, then add in .cshtml file.
            //vm.ContentData.Summary = string.Format("{0} {1}", Framework.Resx.UIStringResource.Details, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);

            return View(vm);
        }

        #endregion ActionResult Details(string application = default(string))

        #region ActionResult AddNew()

        /// <summary>
        /// GET method of Adds the new <see cref="ElmahModel.ElmahApplication"/>.
        /// GET: /ElmahApplication/AddNew
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> AddNew()
        {
            var entity = CreateEmptyEntityOrGetFromTempData(TempDataKey_ElmahApplicationController_Copy);

            Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.Create;
            Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM vm = await Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM.CreateNewViewModel(this._serviceProvider, entity);
            await vm.LoadExtraData(uiAction);

            return View(vm);
        }

        /// <summary>
        /// Post method of Adds the new <see cref="ElmahModel.ElmahApplication"/>.
        /// POST: /ElmahApplication/AddNew
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> AddNew(Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM vm)
        {
            try
            {
                //log.Info(string.Format("{0}: AddNew", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
                var entity = vm.Item;

                using (var scope = _serviceProvider.CreateScope())
                {

                    var serviceInstance = (Elmah.WcfContracts.IElmahApplicationService)scope.ServiceProvider.GetRequiredService(typeof(Elmah.WcfContracts.IElmahApplicationService));
                    var request = new Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn();

                    request.Criteria.Add(entity);
                    var _Response = await serviceInstance.UpsertEntity(request);

                    if (_Response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                    {
                        TempData[TempDataKey_ElmahApplicationController_Copy] = null;
                        TempData.Remove(TempDataKey_ElmahApplicationController_Copy);
                        //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.UI_Process_Ended.ToString()));

                        return Redirect(Request.Headers["Referer"].ToString());
                    }
                    else
                    {
                        vm = await Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM.CreateNewViewModel(this._serviceProvider, vm.Item);
                        vm.StatusOfResult = _Response.BusinessLogicLayerResponseStatus;
                        vm.StatusMessageOfResult = _Response.ServerErrorMessage;
                        //log.Error(string.Format("{0}: AddNew: {1}", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Failed.ToString(), _Response.ServerErrorMessage));
                        return View(vm);
                    }
                }
            }
            catch(Exception ex)
            {
                Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.Create;
                var entity = CreateEmptyEntityOrGetFromTempData(TempDataKey_ElmahApplicationController_Copy);
                vm = await Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM.CreateNewViewModel(this._serviceProvider, entity);
                vm.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                vm.StatusMessageOfResult = ex.Message;
                vm.UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage(typeof(Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM).FullName, uiAction.ToString(), uiAction, Framework.ViewModels.UIActionStatus.Failed);
                //log.Error(string.Format("{0}: AddNew: {1}", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Failed.ToString(), ex.Message));
                return View(vm);
            }
        }

        public const string TempDataKey_ElmahApplicationController_Copy = "TempDataKey_ElmahApplicationController_Copy";
        /// <summary>
        /// Copies current <see cref="ElmahModel.ElmahApplication"/>, without identifier
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> Copy(string application = default(string))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var serviceInstance = (Elmah.WcfContracts.IElmahApplicationService)scope.ServiceProvider.GetRequiredService(typeof(Elmah.WcfContracts.IElmahApplicationService));

                var _Response = await serviceInstance.GetMessageOfEntityByIdentifier(!string.IsNullOrEmpty(application), application, -1, -1, null);
                if (_Response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                {
                    TempData[TempDataKey_ElmahApplicationController_Copy] = _Response.Message[0];
                    TempData.Keep(TempDataKey_ElmahApplicationController_Copy);
                }

                return RedirectToAction("AddNew");
            }
        }

        #endregion ActionResult AddNew()

        #region ActionResult Edit(string application = default(string))

        /// <summary>
        /// GET method of editing page of <see cref="ElmahModel.ElmahApplication"/>.
        /// GET: /ElmahApplication/Edit/5
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> Edit(string application = default(string))
        {
            Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.Update;
            using (var scope = _serviceProvider.CreateScope())
            {
                var vm = (Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM)scope.ServiceProvider.GetRequiredService(typeof(Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM));

                vm.SetServiceProvider(this._serviceProvider);
                await vm.Load(!string.IsNullOrEmpty(application), application, uiAction);
                vm.ContentData.Title = string.Format("{0} {1}", Framework.Resx.UIStringResource.Edit, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                //TODO: create a new new entry in xxx.UIStringResourceExt.resx file if you need, and uncomment next line, reference resource key here, then add in .cshtml file.
                //vm.ContentData.Summary = string.Format("{0} {1}", Framework.Resx.UIStringResource.Edit, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);

                return View(vm);
            }
        }

        /// <summary>
        /// POST method of editing page of <see cref="ElmahModel.ElmahApplication"/>.
        /// POST: /ElmahApplication/Edit/5
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        [HttpPost]
        public async Task<ActionResult> Edit(Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM vm)
        {
            try
            {
                //log.Info(string.Format("{0}: Edit", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
                var entity = vm.Item;

                using (var scope = _serviceProvider.CreateScope())
                {

                    var serviceInstance = (Elmah.WcfContracts.IElmahApplicationService)scope.ServiceProvider.GetRequiredService(typeof(Elmah.WcfContracts.IElmahApplicationService));

                    var request1 = new Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfIdentifier();
                    request1.Criteria = new Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaIdentifier();
                    request1.Criteria.Identifier.Application.NullableValueToCompare = vm.Item.Application;

                    var originalItem = await serviceInstance.GetCollectionOfEntityByIdentifier(request1);
                    entity.CopyFrom<Elmah.DataSourceEntities.ElmahApplication>(originalItem.Message[0]);
                    // TODO: Some of the FKs not in view/.cshtml, must assigned here,
                    //entity.ParentBusinessEntityID = originalItem.Message[0].ParentBusinessEntityID;

                    var request = new Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn();

                    request.Criteria.Add(entity);
                    var _Response = await serviceInstance.UpsertEntity(request);

                    if (_Response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                    {
                        //log.Info(string.Format("{0}: Edit", Framework.Models.LoggingOptions.UI_Process_Ended.ToString()));
                        return Redirect(Request.Headers["Referer"].ToString());
                    }
                    else
                    {
                        vm.ContentData.Title = string.Format("{0} {1}", Framework.Resx.UIStringResource.Edit, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                        //TODO: create a new new entry in xxx.UIStringResourceExt.resx file if you need, and uncomment next line, reference resource key here, then add in .cshtml file.
                        //vm.ContentData.Summary = string.Format("{0} {1}", Framework.Resx.UIStringResource.Edit, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                        vm.StatusOfResult = _Response.BusinessLogicLayerResponseStatus;
                        vm.StatusMessageOfResult = _Response.ServerErrorMessage;
                        //log.Error(string.Format("{0}: Edit: {1}", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Failed.ToString(), _Response.ServerErrorMessage));
                        return View(vm);
                    }
                }
            }
            catch(Exception ex)
            {
                Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.Update;
                vm.ContentData.Title = string.Format("{0} {1}", Framework.Resx.UIStringResource.Edit, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                //TODO: create a new new entry in xxx.UIStringResourceExt.resx file if you need, and uncomment next line, reference resource key here, then add in .cshtml file.
                //vm.ContentData.Summary = string.Format("{0} {1}", Framework.Resx.UIStringResource.Edit, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                vm.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                vm.StatusMessageOfResult = ex.Message;
                vm.UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage(typeof(Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM).FullName, uiAction.ToString(), uiAction, Framework.ViewModels.UIActionStatus.Failed);
                //log.Error(string.Format("{0}: {1}, {2}, {3}", uiAction, Framework.Models.LoggingOptions.UI_Process_Failed.ToString(), vm.StatusOfResult, vm.StatusMessageOfResult));

                return View(vm);
            }
        }

        #endregion ActionResult Edit(string application = default(string))

        #region ActionResult Delete(string application = default(string))

        /// <summary>
        /// GET method of delete page of <see cref="ElmahModel.ElmahApplication"/>
        /// GET: /ElmahApplication/Delete/5
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        public async Task<ActionResult> Delete(string application = default(string))
        {
            Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.Delete;
            using (var scope = _serviceProvider.CreateScope())
            {
                var vm = (Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM)scope.ServiceProvider.GetRequiredService(typeof(Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM));
                vm.SetServiceProvider(this._serviceProvider);
                await vm.Load(!string.IsNullOrEmpty(application), application, uiAction);
                vm.ContentData.Title = string.Format("{0} {1}", Framework.Resx.UIStringResource.Delete, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                //TODO: create a new new entry in xxx.UIStringResourceExt.resx file if you need, and uncomment next line, reference resource key here, then add in .cshtml file.
                //vm.ContentData.Summary = string.Format("{0} {1}", Framework.Resx.UIStringResource.Delete, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                return View(vm);
            }
        }

        /// <summary>
        /// TODO: Delete(...) not working.
        /// POST method of delete page of <see cref="ElmahModel.ElmahApplication"/>
        /// POST: /ElmahApplication/Delete/5
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = Elmah.MvcCore.Security.RolesCombination.AllRoles)]
        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        public async Task<ActionResult> Delete(string application = default(string), Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM vm = null)
        {
            // The Delete is commented out.
            // remove [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
            // if you make it working
            // or remove async to make this method sync method

            return Ok();
/*
            try
            {
                //log.Info(string.Format("{0}: Delete", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
                using (var scope = _serviceProvider.CreateScope())
                {
                    var serviceInstance = (Elmah.WcfContracts.IElmahApplicationService)scope.ServiceProvider.GetRequiredService(typeof(Elmah.WcfContracts.IElmahApplicationService));
                    var _Response1 = await serviceInstance.ExistsOfEntityByIdentifier(!string.IsNullOrEmpty(application), application, -1, -1, null);
                    if (_Response1.Message)
                    {
                        var entity = vm.Item;

                        var _Response = await serviceInstance.DeleteEntity(entity);
                        if (_Response.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                        {
                            //log.Info(string.Format("{0}: Delete", Framework.Models.LoggingOptions.UI_Process_Ended.ToString()));
                        }
                        else
                        {
                            vm.ContentData.Title = string.Format("{0} {1}", Framework.Resx.UIStringResource.Delete, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                            //TODO: create a new new entry in xxx.UIStringResourceExt.resx file if you need, and uncomment next line, reference resource key here, then add in .cshtml file.
                            //vm.ContentData.Summary = string.Format("{0} {1}", Framework.Resx.UIStringResource.Delete, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                            vm.StatusOfResult = _Response.BusinessLogicLayerResponseStatus;
                            vm.StatusMessageOfResult = _Response.ServerErrorMessage;
                            //log.Error(string.Format("{0}: Delete: {1}", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Failed.ToString(), _Response.ServerErrorMessage));
                            return View(vm);
                        }
                    }
                    else
                    {
                        //log.Warn(string.Format("{0}: Delete, Entity not exists",  Framework.Models.LoggingOptions.UI_Process_Ended.ToString()));
                    }

                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (Exception ex)
            {
                Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.Delete;
                vm.ContentData.Title = string.Format("{0} {1}", Framework.Resx.UIStringResource.Delete, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                //TODO: create a new new entry in xxx.UIStringResourceExt.resx file if you need, and uncomment next line, reference resource key here, then add in .cshtml file.
                //vm.ContentData.Summary = string.Format("{0} {1}", Framework.Resx.UIStringResource.Delete, Elmah.Resx.UIStringResourcePerApp.ElmahApplication);
                vm.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                vm.StatusMessageOfResult = ex.Message;
                vm.UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage(typeof(Elmah.AspNetMvcCoreViewModel.ElmahApplication.ItemVM).FullName, uiAction.ToString(), uiAction, Framework.ViewModels.UIActionStatus.Failed);
                //log.Error(string.Format("{0}: {1}, {2}, {3}", uiAction, Framework.Models.LoggingOptions.UI_Process_Failed.ToString(), vm.StatusOfResult, vm.StatusMessageOfResult));

                return View(vm);
            }
*/
        }

        #endregion ActionResult Delete(string application = default(string))

        #region FileRepository

        #endregion FileRepository

        #region GoBackList()

        public ActionResult GoBackList()
        {
            return RedirectToAction(Request.Headers["Referer"].ToString());
        }

        #endregion GoBackList()

        private Elmah.DataSourceEntities.ElmahApplication CreateEmptyEntityOrGetFromTempData(string tempDataKey_ElmahApplicationController_Copy)
        {
            Elmah.DataSourceEntities.ElmahApplication entity;
            if (TempData.ContainsKey(tempDataKey_ElmahApplicationController_Copy) && TempData[tempDataKey_ElmahApplicationController_Copy] != null)
            {
                try
                {
                    entity = (Elmah.DataSourceEntities.ElmahApplication)TempData[tempDataKey_ElmahApplicationController_Copy];
                    TempData.Keep(tempDataKey_ElmahApplicationController_Copy);
                }
                catch
                {
                    entity = new Elmah.DataSourceEntities.ElmahApplication();
                }
            }
            else
            {
                entity = new Elmah.DataSourceEntities.ElmahApplication();
            }

            return entity;
        }
    }
}


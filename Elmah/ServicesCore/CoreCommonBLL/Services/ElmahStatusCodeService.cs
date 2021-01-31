using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Framework.Queries;

namespace Elmah.CoreCommonBLL
{
    /// <summary>
    ///  provides Single/Batch Insert/Update/Delete
    ///  query methods based on BusinessLogicLayerRequestMessage, convert data access message to business logic layer response message
    ///  this BLL class targets at entity <see cref="Elmah.DataSourceEntities.ElmahStatusCode"/>
    /// </summary>
    public partial class ElmahStatusCodeService
        : Elmah.WcfContracts.IElmahStatusCodeService
    {
        Elmah.DALContracts.IElmahStatusCodeRepository DALClassInstance { get; set; }
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahStatusCodeService"/> class.
        /// </summary>
        public ElmahStatusCodeService(
            Elmah.DALContracts.IElmahStatusCodeRepository dalClassInstance, ILogger<ElmahStatusCodeService> logger)
        {
            this.DALClassInstance = dalClassInstance;
            this._logger = logger;
        }

        #region InsertEntity/UpdateEntity/DeleteEntity/BatchInsert/BatchUpdate/BatchDelete

        /// <summary>
        /// Deletes the specified input.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>a message with action result</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn request)
        {
            var _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null && request.Criteria.Count == 1)
            {
                try
                {
                    //log.Info(string.Format("{0}: DeleteEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var _resultFromDAL = await this.DALClassInstance.Delete(request.Criteria[0]);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
                    //log.Info(string.Format("{0}: DeleteEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
                }
                catch(Exception ex)
                {
                    _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    _retval.ServerErrorMessage = ex.Message;
                }
            }
            else
            {
                _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
            }

            return _retval;
        }

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>a message with action result</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltInOfIdentifier id)
        {
            var _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = id.BusinessLogicLayerRequestID;

            if (id != null && id.Criteria != null)
            {
                try
                {
                    //log.Info(string.Format("{0}: DeleteByIdentifierEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var _resultFromDAL = await this.DALClassInstance.DeleteByIdentifier(id.Criteria);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
                    //log.Info(string.Format("{0}: DeleteByIdentifierEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
                }
                catch(Exception ex)
                {
                    _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    _retval.ServerErrorMessage = ex.Message;
                }
            }
            else
            {
                _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
            }
            return _retval;
        }

        /// <summary>
        /// Updates the specified input.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>a message with action result</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn request)
        {
            var  _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null && request.Criteria.Count == 1)
            {
                try
                {
                    //log.Info(string.Format("{0}: UpsertEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var  _resultFromDAL = await this.DALClassInstance.Upsert(request.Criteria[0]);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
                    //log.Info(string.Format("{0}: UpsertEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
                }
                catch(Exception ex)
                {
                    _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    _retval.ServerErrorMessage = ex.Message;
                }
            }
            else
            {
                _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
            }

            return _retval;
        }

/*
        /// <summary>
        /// Inserts the specified input.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>a message with action result</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn request)
        {
            var _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
                _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null && request.Criteria.Count == 1)
            {
                try
                {
                    //log.Info(string.Format("{0}: InsertEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var _resultFromDAL = await this.DALClassInstance.Insert(request.Criteria[0]);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
                    //log.Info(string.Format("{0}: InsertEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
                }
                catch(Exception ex)
                {
                    _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    _retval.ServerErrorMessage = ex.Message;
                }
            }
            else
            {
                _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
            }

            return _retval;
        }

        /// <summary>
        /// Updates the specified input.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>a message with action result</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn request)
        {
            var  _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null && request.Criteria.Count == 1)
            {
                try
                {
                    //log.Info(string.Format("{0}: UpdateEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var  _resultFromDAL = await this.DALClassInstance.Update(request.Criteria[0]);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
                    //log.Info(string.Format("{0}: UpdateEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
                }
                catch(Exception ex)
                {
                    _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    _retval.ServerErrorMessage = ex.Message;
                }
            }
            else
            {
                _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
            }

            return _retval;
        }

        /// <summary>
        /// Batches the insert.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>a message with action result</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn request)
        {
            //log.Info(string.Format("{0}: BatchInsert", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
            var _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null)
            {
                try
                {
                    //log.Info(string.Format("{0}: GetCountOfEntityOfCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var _resultFromDAL = await this.DALClassInstance.BatchInsert(request.Criteria);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
                    //log.Info(string.Format("{0}: GetCountOfEntityOfCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
                }
                catch(Exception ex)
                {
                    _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    _retval.ServerErrorMessage = ex.Message;
                }
            }
            else
            {
                _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
            }
            //log.Info(string.Format("{0}: BatchInsert", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Batches the delete.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>a message with action result</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn request)
        {
            //log.Info(string.Format("{0}: BatchDelete", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
            var _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null)
            {
                try
                {
                    var _resultFromDAL = await this.DALClassInstance.BatchDelete(request.Criteria);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
                }
                catch(Exception ex)
                {
                    _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    _retval.ServerErrorMessage = ex.Message;
                }
            }
            else
            {
                _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
            }
            //log.Info(string.Format("{0}: BatchDelete", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Batches the update.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>a message with action result</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn request)
        {
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
            var _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null)
            {
                try
                {
                    var _resultFromDAL = await this.DALClassInstance.BatchDelete(request.Criteria);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
                }
                catch(Exception ex)
                {
                    _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected;
                    _retval.ServerErrorMessage = ex.Message;
                }
            }
            else
            {
                _retval.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
            }
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }
*/

        #endregion InsertEntity/UpdateEntity/DeleteEntity/BatchInsert/BatchUpdate/BatchDelete

        /// <summary>
        /// Gets the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareName">will compare/filter name property/field/column if true, otherwise false</param>
        /// <param name="name" > value to compare/filter with name property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns></returns>
        private static Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon _GetElmahStatusCodeChainedQueryCriteriaCommon(
            bool isToCompareName, string name
            )
        {
            var criteria = new Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon();
            criteria.Common.Name = new QuerySystemStringContainsCriteria(isToCompareName, name);
            return criteria;
        }

        /// <summary>
        /// Gets the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareStatusCode">will compare/filter statusCode property/field/column if true, otherwise false</param>
        /// <param name="statusCode">value to compare/filter with statusCode property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns></returns>
        private static Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaIdentifier _GetElmahStatusCodeChainedQueryCriteriaIdentifier(
            bool isToCompareStatusCode, int? statusCode
            )
        {
            var criteria = new Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaIdentifier();
            criteria.Identifier.StatusCode = new QuerySystemInt32EqualsCriteria(isToCompareStatusCode, statusCode);
            return criteria;
        }

        #region Query Methods Of EntityByCommon

        /// <summary>
        /// Gets the collection of entity of EntityByCommon .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>an instance of Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn if any</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon request)
        {
            //log.Info(string.Format("{0}: GetCollectionOfEntityByCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));

            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode _resultFromDAL = await this.DALClassInstance.GetCollectionOfEntityByCommon(
                request.Criteria.Common.Name
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection
                );
            Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            //Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);

            if (request.DataServiceType == Framework.Models.DataServiceTypes.DataSourceResult)
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
            }
            else
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Elmah.DataSourceEntities.ElmahStatusCode, List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval, request.DataServiceType, new Elmah.CoreCommonBLL.ElmahStatusCodeDataStreamService());
            }

            //log.Info(string.Format("{0}: GetCollectionOfEntityByCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahStatusCode>"/></returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult)
        {
            //log.Info(string.Format("{0}: GetMessageOfEntityByCommon", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
            var _Request = new Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetMessageOfEntityByCommon"
                , Guid.NewGuid().ToString()
                );
            _Request.Criteria = criteria;
            _Request.QueryPagingSetting = queryPagingSetting;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                _Request.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection();
                _Request.QueryOrderBySettingCollection.Add("StatusCode", Framework.Queries.QueryOrderDirections.Ascending);
            }
            else
            {
                _Request.QueryOrderBySettingCollection = queryOrderBySettingCollection;
            }
            _Request.DataServiceType = dataServiceType;

            Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn _Response = await GetCollectionOfEntityByCommon(_Request);
            return _Response;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareName">will compare/filter name property/field/column if true, otherwise false</param>
        /// <param name="name" > value to compare/filter with name property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn"/></returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            bool isToCompareName, string name
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression        )

        {
            var _Request = new Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetCollectionOfEntityByCommon"
                , Guid.NewGuid().ToString()
                , currentIndex
                , pageSize
                , queryOrderByExpression
                );
            _Request.Criteria = _GetElmahStatusCodeChainedQueryCriteriaCommon(
                isToCompareName, name
                );
            _Request.Criteria.CanQueryWhenNoQuery = true;
            Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn _Response = await GetCollectionOfEntityByCommon(_Request);
            return _Response;
        }

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

        /// <summary>
        /// Gets the collection of entity of NameValuePairByCommon .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>an instance of Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection if any</returns>
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon request)
        {
            //log.Info(string.Format("{0}: GetCollectionOfNameValuePairByCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));

            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }

            Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection _resultFromDAL = await this.DALClassInstance.GetCollectionOfNameValuePairByCommon(
                request.Criteria.Common.Name
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection
                );
            Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection _retval = new Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            //Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Framework.Models.NameValuePair>>(_resultFromDAL, _retval);

            if (request.DataServiceType == Framework.Models.DataServiceTypes.DataSourceResult)
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Framework.Models.NameValuePair>>(_resultFromDAL, _retval);
            }
            else
            {
                //Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Framework.Models.NameValuePair, List<Framework.Models.NameValuePair>>(_resultFromDAL, _retval, request.DataServiceType, new Elmah.CoreCommonBLL.ElmahStatusCodeDataStreamService.NameValuePair());
            }

            //log.Info(string.Format("{0}: GetCollectionOfNameValuePairByCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Framework.Models.NameValuePair>"/></returns>
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult)
        {
            //log.Info(string.Format("{0}: GetMessageOfNameValuePairByCommon", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
            var _Request = new Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetMessageOfNameValuePairByCommon"
                , Guid.NewGuid().ToString()
                );
            _Request.Criteria = criteria;
            _Request.QueryPagingSetting = queryPagingSetting;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                _Request.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection();
                _Request.QueryOrderBySettingCollection.Add("Name", Framework.Queries.QueryOrderDirections.Ascending);
            }
            else
            {
                _Request.QueryOrderBySettingCollection = queryOrderBySettingCollection;
            }
            _Request.DataServiceType = dataServiceType;

            Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection _Response = await GetCollectionOfNameValuePairByCommon(_Request);
            return _Response;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareName">will compare/filter name property/field/column if true, otherwise false</param>
        /// <param name="name" > value to compare/filter with name property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection"/></returns>
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            bool isToCompareName, string name
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression        )

        {
            var _Request = new Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetCollectionOfNameValuePairByCommon"
                , Guid.NewGuid().ToString()
                , currentIndex
                , pageSize
                , queryOrderByExpression
                );
            _Request.Criteria = _GetElmahStatusCodeChainedQueryCriteriaCommon(
                isToCompareName, name
                );
            _Request.Criteria.CanQueryWhenNoQuery = true;
            Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection _Response = await GetCollectionOfNameValuePairByCommon(_Request);
            return _Response;
        }

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

        /// <summary>
        /// Exists the of entity of EntityByIdentifier .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns> Framework.Services.BusinessLogicLayerResponseMessageBoolean</returns>
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier request)
        {
            //log.Info(string.Format("{0}: ExistsOfEntityByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Framework.Services.BusinessLogicLayerResponseMessageBoolean();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }
            Framework.Models.DataAccessLayerMessageOfBoolean _resultFromDAL = await this.DALClassInstance.ExistsOfEntityByIdentifier(
                request.Criteria.Identifier.StatusCode
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection);
            Framework.Services.BusinessLogicLayerResponseMessageBoolean _retval = new Framework.Services.BusinessLogicLayerResponseMessageBoolean();

            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;
            Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<bool>(_resultFromDAL, _retval);
            //log.Info(string.Format("{0}: ExistsOfEntityByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets the collection of entity of EntityByIdentifier .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>an instance of Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn if any</returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier request)
        {
            //log.Info(string.Format("{0}: GetCollectionOfEntityByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));

            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode _resultFromDAL = await this.DALClassInstance.GetCollectionOfEntityByIdentifier(
                request.Criteria.Identifier.StatusCode
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection
                );
            Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn _retval = new Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            //Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);

            if (request.DataServiceType == Framework.Models.DataServiceTypes.DataSourceResult)
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval);
            }
            else
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Elmah.DataSourceEntities.ElmahStatusCode, List<Elmah.DataSourceEntities.ElmahStatusCode>>(_resultFromDAL, _retval, request.DataServiceType, new Elmah.CoreCommonBLL.ElmahStatusCodeDataStreamService());
            }

            //log.Info(string.Format("{0}: GetCollectionOfEntityByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahStatusCode>"/></returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult)
        {
            //log.Info(string.Format("{0}: GetMessageOfEntityByIdentifier", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
            var _Request = new Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetMessageOfEntityByIdentifier"
                , Guid.NewGuid().ToString()
                );
            _Request.Criteria = criteria;
            _Request.QueryPagingSetting = queryPagingSetting;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                _Request.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection();
                _Request.QueryOrderBySettingCollection.Add("StatusCode", Framework.Queries.QueryOrderDirections.Ascending);
            }
            else
            {
                _Request.QueryOrderBySettingCollection = queryOrderBySettingCollection;
            }
            _Request.DataServiceType = dataServiceType;

            Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn _Response = await GetCollectionOfEntityByIdentifier(_Request);
            return _Response;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareStatusCode">will compare/filter statusCode property/field/column if true, otherwise false</param>
        /// <param name="statusCode">value to compare/filter with statusCode property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn"/></returns>
        public async Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareStatusCode, int? statusCode
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression        )

        {
            var _Request = new Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetCollectionOfEntityByIdentifier"
                , Guid.NewGuid().ToString()
                , currentIndex
                , pageSize
                , queryOrderByExpression
                );
            _Request.Criteria = _GetElmahStatusCodeChainedQueryCriteriaIdentifier(
                isToCompareStatusCode, statusCode
                );
            _Request.Criteria.CanQueryWhenNoQuery = true;
            Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn _Response = await GetCollectionOfEntityByIdentifier(_Request);
            return _Response;
        }

        #endregion Query Methods Of EntityByIdentifier

        #region GetAscendantAndDescendant

        #endregion GetAscendantAndDescendant

    }
}


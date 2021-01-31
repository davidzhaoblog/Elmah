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
    ///  this BLL class targets at entity <see cref="Elmah.DataSourceEntities.ELMAH_Error"/>
    /// </summary>
    public partial class ELMAH_ErrorService
        : Elmah.WcfContracts.IELMAH_ErrorService
    {
        Elmah.DALContracts.IELMAH_ErrorRepository DALClassInstance { get; set; }
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ELMAH_ErrorService"/> class.
        /// </summary>
        public ELMAH_ErrorService(
            Elmah.DALContracts.IELMAH_ErrorRepository dalClassInstance, ILogger<ELMAH_ErrorService> logger)
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
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn request)
        {
            var _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null && request.Criteria.Count == 1)
            {
                try
                {
                    //log.Info(string.Format("{0}: DeleteEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var _resultFromDAL = await this.DALClassInstance.Delete(request.Criteria[0]);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
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
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltInOfIdentifier id)
        {
            var _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = id.BusinessLogicLayerRequestID;

            if (id != null && id.Criteria != null)
            {
                try
                {
                    //log.Info(string.Format("{0}: DeleteByIdentifierEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var _resultFromDAL = await this.DALClassInstance.DeleteByIdentifier(id.Criteria);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
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
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn request)
        {
            var  _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null && request.Criteria.Count == 1)
            {
                try
                {
                    //log.Info(string.Format("{0}: UpsertEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var  _resultFromDAL = await this.DALClassInstance.Upsert(request.Criteria[0]);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
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
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn request)
        {
            var _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
                _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null && request.Criteria.Count == 1)
            {
                try
                {
                    //log.Info(string.Format("{0}: InsertEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var _resultFromDAL = await this.DALClassInstance.Insert(request.Criteria[0]);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
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
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn request)
        {
            var  _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null && request.Criteria.Count == 1)
            {
                try
                {
                    //log.Info(string.Format("{0}: UpdateEntity", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var  _resultFromDAL = await this.DALClassInstance.Update(request.Criteria[0]);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
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
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn request)
        {
            //log.Info(string.Format("{0}: BatchInsert", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
            var _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null)
            {
                try
                {
                    //log.Info(string.Format("{0}: GetCountOfEntityOfCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
                    var _resultFromDAL = await this.DALClassInstance.BatchInsert(request.Criteria);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
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
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn request)
        {
            //log.Info(string.Format("{0}: BatchDelete", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
            var _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null)
            {
                try
                {
                    var _resultFromDAL = await this.DALClassInstance.BatchDelete(request.Criteria);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
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
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn request)
        {
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
            var _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request != null)
            {
                try
                {
                    var _resultFromDAL = await this.DALClassInstance.BatchDelete(request.Criteria);

                    Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
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
        /// <param name="isToCompareApplication">will compare/filter application property/field/column if true, otherwise false</param>
        /// <param name="application">value to compare/filter with application property/field/column</param>
        /// <param name="isToCompareHost">will compare/filter host property/field/column if true, otherwise false</param>
        /// <param name="host">value to compare/filter with host property/field/column</param>
        /// <param name="isToCompareSource">will compare/filter source property/field/column if true, otherwise false</param>
        /// <param name="source">value to compare/filter with source property/field/column</param>
        /// <param name="isToCompareStatusCode">will compare/filter statusCode property/field/column if true, otherwise false</param>
        /// <param name="statusCode">value to compare/filter with statusCode property/field/column</param>
        /// <param name="isToCompareType">will compare/filter type property/field/column if true, otherwise false</param>
        /// <param name="type">value to compare/filter with type property/field/column</param>
        /// <param name="isToCompareUser">will compare/filter user property/field/column if true, otherwise false</param>
        /// <param name="user">value to compare/filter with user property/field/column</param>
        /// <param name="isToCompareMessage">will compare/filter message property/field/column if true, otherwise false</param>
        /// <param name="message" > value to compare/filter with message property/field/column</param>
        /// <param name="isToCompareAllXml">will compare/filter allXml property/field/column if true, otherwise false</param>
        /// <param name="allXml" > value to compare/filter with allXml property/field/column</param>
        /// <param name="isToCompareTimeUtcRange" > will compare/filter timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="isToCompareTimeUtcRangeLow" > will compare/filter to lower bound of timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="timeUtcRangeLow">value of lower bound</param>
        /// <param name="isToCompareTimeUtcRangeHigh">will compare/filter to upper bound of timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="timeUtcRangeHigh">upper bound</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns></returns>
        private static Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon _GetELMAH_ErrorChainedQueryCriteriaCommon(
            bool isToCompareApplication, string application
            , bool isToCompareHost, string host
            , bool isToCompareSource, string source
            , bool isToCompareStatusCode, int? statusCode
            , bool isToCompareType, string type
            , bool isToCompareUser, string user
            , bool isToCompareMessage, string message
            , bool isToCompareAllXml, string allXml
            , bool isToCompareTimeUtcRange, bool isToCompareTimeUtcRangeLow, System.DateTime? timeUtcRangeLow, bool isToCompareTimeUtcRangeHigh, System.DateTime? timeUtcRangeHigh
            )
        {
            var criteria = new Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon();
            criteria.Common.Application = new QuerySystemStringEqualsCriteria(isToCompareApplication, application);
            criteria.Common.Host = new QuerySystemStringEqualsCriteria(isToCompareHost, host);
            criteria.Common.Source = new QuerySystemStringEqualsCriteria(isToCompareSource, source);
            criteria.Common.StatusCode = new QuerySystemInt32EqualsCriteria(isToCompareStatusCode, statusCode);
            criteria.Common.Type = new QuerySystemStringEqualsCriteria(isToCompareType, type);
            criteria.Common.User = new QuerySystemStringEqualsCriteria(isToCompareUser, user);
            criteria.Common.Message = new QuerySystemStringContainsCriteria(isToCompareMessage, message);
            criteria.Common.AllXml = new QuerySystemStringContainsCriteria(isToCompareAllXml, allXml);
            criteria.Common.TimeUtcRange = new QuerySystemDateTimeRangeCriteria(isToCompareTimeUtcRange, isToCompareTimeUtcRangeLow, timeUtcRangeLow, isToCompareTimeUtcRangeHigh, timeUtcRangeHigh);
            return criteria;
        }

        /// <summary>
        /// Gets the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareErrorId">will compare/filter errorId property/field/column if true, otherwise false</param>
        /// <param name="errorId">value to compare/filter with errorId property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns></returns>
        private static Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier _GetELMAH_ErrorChainedQueryCriteriaIdentifier(
            bool isToCompareErrorId, System.Guid? errorId
            )
        {
            var criteria = new Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier();
            criteria.Identifier.ErrorId = new QuerySystemGuidEqualsCriteria(isToCompareErrorId, errorId);
            return criteria;
        }

        #region Query Methods Of NameValuePairByCommon

        /// <summary>
        /// Gets the collection of entity of NameValuePairByCommon .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>an instance of Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection if any</returns>
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon request)
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
                request.Criteria.Common.Application
                , request.Criteria.Common.Host
                , request.Criteria.Common.Source
                , request.Criteria.Common.StatusCode
                , request.Criteria.Common.Type
                , request.Criteria.Common.User
                , request.Criteria.Common.Message
                , request.Criteria.Common.AllXml
                , request.Criteria.Common.TimeUtcRange
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
                //Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Framework.Models.NameValuePair, List<Framework.Models.NameValuePair>>(_resultFromDAL, _retval, request.DataServiceType, new Elmah.CoreCommonBLL.ELMAH_ErrorDataStreamService.NameValuePair());
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
            Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult)
        {
            //log.Info(string.Format("{0}: GetMessageOfNameValuePairByCommon", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
            var _Request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon(
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
        /// <param name="isToCompareApplication">will compare/filter application property/field/column if true, otherwise false</param>
        /// <param name="application">value to compare/filter with application property/field/column</param>
        /// <param name="isToCompareHost">will compare/filter host property/field/column if true, otherwise false</param>
        /// <param name="host">value to compare/filter with host property/field/column</param>
        /// <param name="isToCompareSource">will compare/filter source property/field/column if true, otherwise false</param>
        /// <param name="source">value to compare/filter with source property/field/column</param>
        /// <param name="isToCompareStatusCode">will compare/filter statusCode property/field/column if true, otherwise false</param>
        /// <param name="statusCode">value to compare/filter with statusCode property/field/column</param>
        /// <param name="isToCompareType">will compare/filter type property/field/column if true, otherwise false</param>
        /// <param name="type">value to compare/filter with type property/field/column</param>
        /// <param name="isToCompareUser">will compare/filter user property/field/column if true, otherwise false</param>
        /// <param name="user">value to compare/filter with user property/field/column</param>
        /// <param name="isToCompareMessage">will compare/filter message property/field/column if true, otherwise false</param>
        /// <param name="message" > value to compare/filter with message property/field/column</param>
        /// <param name="isToCompareAllXml">will compare/filter allXml property/field/column if true, otherwise false</param>
        /// <param name="allXml" > value to compare/filter with allXml property/field/column</param>
        /// <param name="isToCompareTimeUtcRange" > will compare/filter timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="isToCompareTimeUtcRangeLow" > will compare/filter to lower bound of timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="timeUtcRangeLow">value of lower bound</param>
        /// <param name="isToCompareTimeUtcRangeHigh">will compare/filter to upper bound of timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="timeUtcRangeHigh">upper bound</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection"/></returns>
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            bool isToCompareApplication, string application
            , bool isToCompareHost, string host
            , bool isToCompareSource, string source
            , bool isToCompareStatusCode, int? statusCode
            , bool isToCompareType, string type
            , bool isToCompareUser, string user
            , bool isToCompareMessage, string message
            , bool isToCompareAllXml, string allXml
            , bool isToCompareTimeUtcRange, bool isToCompareTimeUtcRangeLow, System.DateTime? timeUtcRangeLow, bool isToCompareTimeUtcRangeHigh, System.DateTime? timeUtcRangeHigh
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression        )

        {
            var _Request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetCollectionOfNameValuePairByCommon"
                , Guid.NewGuid().ToString()
                , currentIndex
                , pageSize
                , queryOrderByExpression
                );
            _Request.Criteria = _GetELMAH_ErrorChainedQueryCriteriaCommon(
                isToCompareApplication, application
                , isToCompareHost, host
                , isToCompareSource, source
                , isToCompareStatusCode, statusCode
                , isToCompareType, type
                , isToCompareUser, user
                , isToCompareMessage, message
                , isToCompareAllXml, allXml
                , isToCompareTimeUtcRange, isToCompareTimeUtcRangeLow, timeUtcRangeLow, isToCompareTimeUtcRangeHigh, timeUtcRangeHigh
                );
            _Request.Criteria.CanQueryWhenNoQuery = true;
            Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection _Response = await GetCollectionOfNameValuePairByCommon(_Request);
            return _Response;
        }

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of DefaultByCommon

        /// <summary>
        /// Gets the collection of entity of DefaultByCommon .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>an instance of Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default if any</returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetCollectionOfDefaultByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon request)
        {
            //log.Info(string.Format("{0}: GetCollectionOfDefaultByCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));

            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }

            Elmah.DataSourceEntities.ELMAH_Error.DataAccessLayerMessageOfDefaultCollection _resultFromDAL = await this.DALClassInstance.GetCollectionOfDefaultByCommon(
                request.Criteria.Common.Application
                , request.Criteria.Common.Host
                , request.Criteria.Common.Source
                , request.Criteria.Common.StatusCode
                , request.Criteria.Common.Type
                , request.Criteria.Common.User
                , request.Criteria.Common.Message
                , request.Criteria.Common.AllXml
                , request.Criteria.Common.TimeUtcRange
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection
                );
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            //Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error.Default>>(_resultFromDAL, _retval);

            if (request.DataServiceType == Framework.Models.DataServiceTypes.DataSourceResult)
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error.Default>>(_resultFromDAL, _retval);
            }
            else
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Elmah.DataSourceEntities.ELMAH_Error.Default, List<Elmah.DataSourceEntities.ELMAH_Error.Default>>(_resultFromDAL, _retval, request.DataServiceType, new Elmah.CoreCommonBLL.ELMAH_ErrorDataStreamService.Default());
            }

            //log.Info(string.Format("{0}: GetCollectionOfDefaultByCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ELMAH_Error.Default>"/></returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult)
        {
            //log.Info(string.Format("{0}: GetMessageOfDefaultByCommon", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
            var _Request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetMessageOfDefaultByCommon"
                , Guid.NewGuid().ToString()
                );
            _Request.Criteria = criteria;
            _Request.QueryPagingSetting = queryPagingSetting;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                _Request.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection();
                _Request.QueryOrderBySettingCollection.Add("ErrorId", Framework.Queries.QueryOrderDirections.Ascending);
            }
            else
            {
                _Request.QueryOrderBySettingCollection = queryOrderBySettingCollection;
            }
            _Request.DataServiceType = dataServiceType;

            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default _Response = await GetCollectionOfDefaultByCommon(_Request);
            return _Response;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareApplication">will compare/filter application property/field/column if true, otherwise false</param>
        /// <param name="application">value to compare/filter with application property/field/column</param>
        /// <param name="isToCompareHost">will compare/filter host property/field/column if true, otherwise false</param>
        /// <param name="host">value to compare/filter with host property/field/column</param>
        /// <param name="isToCompareSource">will compare/filter source property/field/column if true, otherwise false</param>
        /// <param name="source">value to compare/filter with source property/field/column</param>
        /// <param name="isToCompareStatusCode">will compare/filter statusCode property/field/column if true, otherwise false</param>
        /// <param name="statusCode">value to compare/filter with statusCode property/field/column</param>
        /// <param name="isToCompareType">will compare/filter type property/field/column if true, otherwise false</param>
        /// <param name="type">value to compare/filter with type property/field/column</param>
        /// <param name="isToCompareUser">will compare/filter user property/field/column if true, otherwise false</param>
        /// <param name="user">value to compare/filter with user property/field/column</param>
        /// <param name="isToCompareMessage">will compare/filter message property/field/column if true, otherwise false</param>
        /// <param name="message" > value to compare/filter with message property/field/column</param>
        /// <param name="isToCompareAllXml">will compare/filter allXml property/field/column if true, otherwise false</param>
        /// <param name="allXml" > value to compare/filter with allXml property/field/column</param>
        /// <param name="isToCompareTimeUtcRange" > will compare/filter timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="isToCompareTimeUtcRangeLow" > will compare/filter to lower bound of timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="timeUtcRangeLow">value of lower bound</param>
        /// <param name="isToCompareTimeUtcRangeHigh">will compare/filter to upper bound of timeUtcRange property/field/column if true, otherwise false</param>
        /// <param name="timeUtcRangeHigh">upper bound</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default"/></returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByCommon(
            bool isToCompareApplication, string application
            , bool isToCompareHost, string host
            , bool isToCompareSource, string source
            , bool isToCompareStatusCode, int? statusCode
            , bool isToCompareType, string type
            , bool isToCompareUser, string user
            , bool isToCompareMessage, string message
            , bool isToCompareAllXml, string allXml
            , bool isToCompareTimeUtcRange, bool isToCompareTimeUtcRangeLow, System.DateTime? timeUtcRangeLow, bool isToCompareTimeUtcRangeHigh, System.DateTime? timeUtcRangeHigh
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression        )

        {
            var _Request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetCollectionOfDefaultByCommon"
                , Guid.NewGuid().ToString()
                , currentIndex
                , pageSize
                , queryOrderByExpression
                );
            _Request.Criteria = _GetELMAH_ErrorChainedQueryCriteriaCommon(
                isToCompareApplication, application
                , isToCompareHost, host
                , isToCompareSource, source
                , isToCompareStatusCode, statusCode
                , isToCompareType, type
                , isToCompareUser, user
                , isToCompareMessage, message
                , isToCompareAllXml, allXml
                , isToCompareTimeUtcRange, isToCompareTimeUtcRangeLow, timeUtcRangeLow, isToCompareTimeUtcRangeHigh, timeUtcRangeHigh
                );
            _Request.Criteria.CanQueryWhenNoQuery = true;
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default _Response = await GetCollectionOfDefaultByCommon(_Request);
            return _Response;
        }

        #endregion Query Methods Of DefaultByCommon

        #region Query Methods Of EntityByIdentifier

        /// <summary>
        /// Exists the of entity of EntityByIdentifier .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns> Framework.Services.BusinessLogicLayerResponseMessageBoolean</returns>
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request)
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
                request.Criteria.Identifier.ErrorId
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
        /// <returns>an instance of Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn if any</returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request)
        {
            //log.Info(string.Format("{0}: GetCollectionOfEntityByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));

            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error _resultFromDAL = await this.DALClassInstance.GetCollectionOfEntityByIdentifier(
                request.Criteria.Identifier.ErrorId
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection
                );
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            //Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);

            if (request.DataServiceType == Framework.Models.DataServiceTypes.DataSourceResult)
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval);
            }
            else
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Elmah.DataSourceEntities.ELMAH_Error, List<Elmah.DataSourceEntities.ELMAH_Error>>(_resultFromDAL, _retval, request.DataServiceType, new Elmah.CoreCommonBLL.ELMAH_ErrorDataStreamService());
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
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ELMAH_Error>"/></returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult)
        {
            //log.Info(string.Format("{0}: GetMessageOfEntityByIdentifier", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
            var _Request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetMessageOfEntityByIdentifier"
                , Guid.NewGuid().ToString()
                );
            _Request.Criteria = criteria;
            _Request.QueryPagingSetting = queryPagingSetting;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                _Request.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection();
                _Request.QueryOrderBySettingCollection.Add("ErrorId", Framework.Queries.QueryOrderDirections.Ascending);
            }
            else
            {
                _Request.QueryOrderBySettingCollection = queryOrderBySettingCollection;
            }
            _Request.DataServiceType = dataServiceType;

            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn _Response = await GetCollectionOfEntityByIdentifier(_Request);
            return _Response;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareErrorId">will compare/filter errorId property/field/column if true, otherwise false</param>
        /// <param name="errorId">value to compare/filter with errorId property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn"/></returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareErrorId, System.Guid? errorId
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression        )

        {
            var _Request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetCollectionOfEntityByIdentifier"
                , Guid.NewGuid().ToString()
                , currentIndex
                , pageSize
                , queryOrderByExpression
                );
            _Request.Criteria = _GetELMAH_ErrorChainedQueryCriteriaIdentifier(
                isToCompareErrorId, errorId
                );
            _Request.Criteria.CanQueryWhenNoQuery = true;
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn _Response = await GetCollectionOfEntityByIdentifier(_Request);
            return _Response;
        }

        #endregion Query Methods Of EntityByIdentifier

        #region Query Methods Of DefaultByIdentifier

        /// <summary>
        /// Exists the of entity of DefaultByIdentifier .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns> Framework.Services.BusinessLogicLayerResponseMessageBoolean</returns>
        public async Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfDefaultByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request)
        {
            //log.Info(string.Format("{0}: ExistsOfDefaultByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));
            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Framework.Services.BusinessLogicLayerResponseMessageBoolean();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }
            Framework.Models.DataAccessLayerMessageOfBoolean _resultFromDAL = await this.DALClassInstance.ExistsOfDefaultByIdentifier(
                request.Criteria.Identifier.ErrorId
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection);
            Framework.Services.BusinessLogicLayerResponseMessageBoolean _retval = new Framework.Services.BusinessLogicLayerResponseMessageBoolean();

            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;
            Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<bool>(_resultFromDAL, _retval);
            //log.Info(string.Format("{0}: ExistsOfDefaultByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets the collection of entity of DefaultByIdentifier .
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>an instance of Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default if any</returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetCollectionOfDefaultByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request)
        {
            //log.Info(string.Format("{0}: GetCollectionOfDefaultByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));

            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }

            Elmah.DataSourceEntities.ELMAH_Error.DataAccessLayerMessageOfDefaultCollection _resultFromDAL = await this.DALClassInstance.GetCollectionOfDefaultByIdentifier(
                request.Criteria.Identifier.ErrorId
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection
                );
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default _retval = new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            //Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error.Default>>(_resultFromDAL, _retval);

            if (request.DataServiceType == Framework.Models.DataServiceTypes.DataSourceResult)
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<List<Elmah.DataSourceEntities.ELMAH_Error.Default>>(_resultFromDAL, _retval);
            }
            else
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Elmah.DataSourceEntities.ELMAH_Error.Default, List<Elmah.DataSourceEntities.ELMAH_Error.Default>>(_resultFromDAL, _retval, request.DataServiceType, new Elmah.CoreCommonBLL.ELMAH_ErrorDataStreamService.Default());
            }

            //log.Info(string.Format("{0}: GetCollectionOfDefaultByIdentifier", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ELMAH_Error.Default>"/></returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult)
        {
            //log.Info(string.Format("{0}: GetMessageOfDefaultByIdentifier", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
            var _Request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetMessageOfDefaultByIdentifier"
                , Guid.NewGuid().ToString()
                );
            _Request.Criteria = criteria;
            _Request.QueryPagingSetting = queryPagingSetting;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                _Request.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection();
                _Request.QueryOrderBySettingCollection.Add("ErrorId", Framework.Queries.QueryOrderDirections.Ascending);
            }
            else
            {
                _Request.QueryOrderBySettingCollection = queryOrderBySettingCollection;
            }
            _Request.DataServiceType = dataServiceType;

            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default _Response = await GetCollectionOfDefaultByIdentifier(_Request);
            return _Response;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareErrorId">will compare/filter errorId property/field/column if true, otherwise false</param>
        /// <param name="errorId">value to compare/filter with errorId property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default"/></returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByIdentifier(
            bool isToCompareErrorId, System.Guid? errorId
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression        )

        {
            var _Request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier(
                Framework.Services.BusinessLogicLayerRequestTypes.Search
                , "GetCollectionOfDefaultByIdentifier"
                , Guid.NewGuid().ToString()
                , currentIndex
                , pageSize
                , queryOrderByExpression
                );
            _Request.Criteria = _GetELMAH_ErrorChainedQueryCriteriaIdentifier(
                isToCompareErrorId, errorId
                );
            _Request.Criteria.CanQueryWhenNoQuery = true;
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default _Response = await GetCollectionOfDefaultByIdentifier(_Request);
            return _Response;
        }

        #endregion Query Methods Of DefaultByIdentifier

        #region GetAscendantAndDescendant

        #endregion GetAscendantAndDescendant

    }
}


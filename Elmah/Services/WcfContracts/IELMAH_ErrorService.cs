using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WcfContracts
{
/*
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IELMAH_ErrorService")]
*/
    public interface IELMAH_ErrorService
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ELMAH_Error>,Elmah.DataSourceEntities.ELMAH_Error,Elmah.DataSourceEntities.ELMAH_ErrorIdentifier> Members
/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/DeleteEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/DeleteByIdentifierEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltInOfIdentifier id);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/UpsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/UpsertEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/InsertEntityResponse")]
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/UpdateEntityResponse")]
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/BatchInsertResponse")]
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/BatchDeleteResponse")]
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/BatchUpdateResponse")]
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);
*/

        #endregion

        #region Query Methods Of NameValuePairByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfNameValuePairByCommonResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Framework.Models.NameValuePair>"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

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
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
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
            , string queryOrderByExpression);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of DefaultByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByCommonResponse")]
*/
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetCollectionOfDefaultByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ELMAH_Error.Default>"/></returns>
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

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
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByCommon(
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
            , string queryOrderByExpression);

        #endregion Query Methods Of DefaultByCommon

        #region Query Methods Of EntityByIdentifier

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfEntityByIdentifierResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request);

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfEntityByIdentifierResponse")]
*/
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ELMAH_Error>"/></returns>
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareErrorId">will compare/filter errorId property/field/column if true, otherwise false</param>
        /// <param name="errorId">value to compare/filter with errorId property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareErrorId, System.Guid? errorId
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByIdentifier

        #region Query Methods Of DefaultByIdentifier

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfDefaultByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfDefaultByIdentifierResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfDefaultByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request);

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByIdentifierResponse")]
*/
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetCollectionOfDefaultByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ELMAH_Error.Default>"/></returns>
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareErrorId">will compare/filter errorId property/field/column if true, otherwise false</param>
        /// <param name="errorId">value to compare/filter with errorId property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default"/></returns>
        Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByIdentifier(
            bool isToCompareErrorId, System.Guid? errorId
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of DefaultByIdentifier

    }
}


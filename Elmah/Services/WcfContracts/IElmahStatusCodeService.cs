using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WcfContracts
{
/*
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahStatusCodeService")]
*/
    public interface IElmahStatusCodeService
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahStatusCode>,Elmah.DataSourceEntities.ElmahStatusCode,Elmah.DataSourceEntities.ElmahStatusCodeIdentifier> Members
/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/DeleteEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/DeleteEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/DeleteByIdentifierEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltInOfIdentifier id);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/UpsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/UpsertEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/InsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/InsertEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/UpdateEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/UpdateEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/BatchInsert",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/BatchInsertResponse")]
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/BatchDelete",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/BatchDeleteResponse")]
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/BatchUpdate",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/BatchUpdateResponse")]
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);
*/

        #endregion

        #region Query Methods Of EntityByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByCommonResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahStatusCode>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareName">will compare/filter name property/field/column if true, otherwise false</param>
        /// <param name="name" > value to compare/filter with name property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            bool isToCompareName, string name
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfNameValuePairByCommonResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Framework.Models.NameValuePair>"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareName">will compare/filter name property/field/column if true, otherwise false</param>
        /// <param name="name" > value to compare/filter with name property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            bool isToCompareName, string name
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/ExistsOfEntityByIdentifierResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier request);

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByIdentifierResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahStatusCode>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareStatusCode">will compare/filter statusCode property/field/column if true, otherwise false</param>
        /// <param name="statusCode">value to compare/filter with statusCode property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareStatusCode, int? statusCode
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByIdentifier

    }
}


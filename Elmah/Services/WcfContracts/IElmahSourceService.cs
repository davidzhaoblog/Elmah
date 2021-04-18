using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WcfContracts
{
/*
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahSourceService")]
*/
    public interface IElmahSourceService
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahSource>,Elmah.DataSourceEntities.ElmahSource,Elmah.DataSourceEntities.ElmahSourceIdentifier> Members
/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/DeleteEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/DeleteEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ElmahSourceRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/DeleteByIdentifierEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahSourceRequestMessageBuiltInOfIdentifier id);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/UpsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/UpsertEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ElmahSourceRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/InsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/InsertEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ElmahSourceRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/UpdateEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/UpdateEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ElmahSourceRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/BatchInsert",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/BatchInsertResponse")]
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ElmahSourceRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/BatchDelete",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/BatchDeleteResponse")]
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ElmahSourceRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/BatchUpdate",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/BatchUpdateResponse")]
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ElmahSourceRequestMessageBuiltIn input);
*/

        #endregion

        #region Query Methods Of EntityByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/GetCollectionOfEntityByCommonResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahSourceRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahSource>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareSource">will compare/filter source property/field/column if true, otherwise false</param>
        /// <param name="source" > value to compare/filter with source property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            bool isToCompareSource, string source
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/GetCollectionOfNameValuePairByCommonResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahSourceRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Framework.Models.NameValuePair>"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareSource">will compare/filter source property/field/column if true, otherwise false</param>
        /// <param name="source" > value to compare/filter with source property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            bool isToCompareSource, string source
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/ExistsOfEntityByIdentifierResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahSourceRequestMessageUserDefinedOfIdentifier request);

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahSourceService/GetCollectionOfEntityByIdentifierResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahSourceRequestMessageUserDefinedOfIdentifier request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahSource>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareSource">will compare/filter source property/field/column if true, otherwise false</param>
        /// <param name="source">value to compare/filter with source property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahSourceResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareSource, string source
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByIdentifier

    }
}


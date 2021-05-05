using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WcfContracts
{
/*
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahApplicationService")]
*/
    public interface IElmahApplicationService
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahApplication>,Elmah.DataSourceEntities.ElmahApplication,Elmah.DataSourceEntities.ElmahApplicationIdentifier> Members
/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteByIdentifierEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltInOfIdentifier id);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/UpsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/UpsertEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/InsertEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/UpdateEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchInsertResponse")]
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchDeleteResponse")]
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchUpdateResponse")]
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);
*/

        #endregion

        #region Query Methods Of EntityByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByCommonResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahApplication>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareApplication">will compare/filter application property/field/column if true, otherwise false</param>
        /// <param name="application" > value to compare/filter with application property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            bool isToCompareApplication, string application
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfNameValuePairByCommonResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Framework.Models.NameValuePair>"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareApplication">will compare/filter application property/field/column if true, otherwise false</param>
        /// <param name="application" > value to compare/filter with application property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            bool isToCompareApplication, string application
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/ExistsOfEntityByIdentifierResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfIdentifier request);

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByIdentifierResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfIdentifier request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahApplication>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareApplication">will compare/filter application property/field/column if true, otherwise false</param>
        /// <param name="application">value to compare/filter with application property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareApplication, string application
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByIdentifier

    }
}


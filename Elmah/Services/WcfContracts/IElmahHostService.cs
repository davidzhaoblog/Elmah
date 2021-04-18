using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WcfContracts
{
/*
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahHostService")]
*/
    public interface IElmahHostService
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahHost>,Elmah.DataSourceEntities.ElmahHost,Elmah.DataSourceEntities.ElmahHostIdentifier> Members
/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/DeleteEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/DeleteEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/DeleteByIdentifierEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltInOfIdentifier id);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/UpsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/UpsertEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/InsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/InsertEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/UpdateEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/UpdateEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/BatchInsert",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/BatchInsertResponse")]
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/BatchDelete",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/BatchDeleteResponse")]
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/BatchUpdate",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/BatchUpdateResponse")]
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);
*/

        #endregion

        #region Query Methods Of EntityByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByCommonResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahHost>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareHost">will compare/filter host property/field/column if true, otherwise false</param>
        /// <param name="host" > value to compare/filter with host property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            bool isToCompareHost, string host
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/GetCollectionOfNameValuePairByCommonResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Framework.Models.NameValuePair>"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareHost">will compare/filter host property/field/column if true, otherwise false</param>
        /// <param name="host" > value to compare/filter with host property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            bool isToCompareHost, string host
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/ExistsOfEntityByIdentifierResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfIdentifier request);

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByIdentifierResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfIdentifier request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahHost>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareHost">will compare/filter host property/field/column if true, otherwise false</param>
        /// <param name="host">value to compare/filter with host property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareHost, string host
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByIdentifier

    }
}


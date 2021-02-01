using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WcfContracts
{
/*
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahUserService")]
*/
    public interface IElmahUserService
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahUser>,Elmah.DataSourceEntities.ElmahUser,Elmah.DataSourceEntities.ElmahUserIdentifier> Members
/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteByIdentifierEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltInOfIdentifier id);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/UpsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/UpsertEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/InsertEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/UpdateEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchInsertResponse")]
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchDeleteResponse")]
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchUpdateResponse")]
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);
*/

        #endregion

        #region Query Methods Of EntityByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByCommonResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahUser>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareUser">will compare/filter user property/field/column if true, otherwise false</param>
        /// <param name="user" > value to compare/filter with user property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            bool isToCompareUser, string user
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfNameValuePairByCommonResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Framework.Models.NameValuePair>"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareUser">will compare/filter user property/field/column if true, otherwise false</param>
        /// <param name="user" > value to compare/filter with user property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            bool isToCompareUser, string user
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/ExistsOfEntityByIdentifierResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfIdentifier request);

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByIdentifierResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfIdentifier request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahUser>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareUser">will compare/filter user property/field/column if true, otherwise false</param>
        /// <param name="user">value to compare/filter with user property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareUser, string user
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByIdentifier

    }
}


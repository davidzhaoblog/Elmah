using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WcfContracts
{
/*
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahTypeService")]
*/
    public interface IElmahTypeService
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahType>,Elmah.DataSourceEntities.ElmahType,Elmah.DataSourceEntities.ElmahTypeIdentifier> Members
/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/DeleteEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/DeleteEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> DeleteEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/DeleteByIdentifierEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltInOfIdentifier id);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/UpsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/UpsertEntityResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> UpsertEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/InsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/InsertEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> InsertEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/UpdateEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/UpdateEntityResponse")]
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> UpdateEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/BatchInsert",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/BatchInsertResponse")]
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> BatchInsert(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/BatchDelete",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/BatchDeleteResponse")]
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> BatchDelete(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/BatchUpdate",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/BatchUpdateResponse")]
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> BatchUpdate(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);
*/

        #endregion

        #region Query Methods Of EntityByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByCommonResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahType>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareType">will compare/filter type property/field/column if true, otherwise false</param>
        /// <param name="type" > value to compare/filter with type property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            bool isToCompareType, string type
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfNameValuePairByCommonResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfCommon request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Framework.Models.NameValuePair>"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaCommon criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareType">will compare/filter type property/field/column if true, otherwise false</param>
        /// <param name="type" > value to compare/filter with type property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection"/></returns>
        Task<Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection> GetMessageOfNameValuePairByCommon(
            bool isToCompareType, string type
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/ExistsOfEntityByIdentifierResponse")]
*/
        Task<Framework.Services.BusinessLogicLayerResponseMessageBoolean> ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfIdentifier request);

/*
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByIdentifierResponse")]
*/
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfIdentifier request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="List<Elmah.DataSourceEntities.ElmahType>"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaIdentifier criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType = Framework.Models.DataServiceTypes.DataSourceResult);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="isToCompareType">will compare/filter type property/field/column if true, otherwise false</param>
        /// <param name="type">value to compare/filter with type property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn"/></returns>
        Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            bool isToCompareType, string type
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression);

        #endregion Query Methods Of EntityByIdentifier

    }
}


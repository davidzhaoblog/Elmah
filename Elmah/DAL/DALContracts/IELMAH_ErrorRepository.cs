using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace Elmah.DALContracts
{
    /// <summary>
    /// There is a DataAccessLayerEntityContract class for each entity, which inherits from <see cref="Framework.Repositories.DataAccessLayerContractBase&lt;TCollection, T, TIdentifier&gt;"/> with 3 type arguments, entity class and its collection class, and identifier class.
    /// </summary>
    public interface IELMAH_ErrorRepository: Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ELMAH_Error>, Elmah.DataSourceEntities.ELMAH_Error, Elmah.DataSourceEntities.ELMAH_ErrorIdentifier, Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error>
    {

        #region Query Methods Of NameValuePairByCommon

        Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection> GetCollectionOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of DefaultByCommon

        Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfDefaultByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfDefaultByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Elmah.DataSourceEntities.ELMAH_Error.DataAccessLayerMessageOfDefaultCollection> GetCollectionOfDefaultByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        #endregion Query Methods Of DefaultByCommon

        #region Query Methods Of EntityByIdentifier

        Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfEntityByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> GetCollectionOfEntityByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        #endregion Query Methods Of EntityByIdentifier

        #region Query Methods Of DefaultByIdentifier

        Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfDefaultByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfDefaultByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Elmah.DataSourceEntities.ELMAH_Error.DataAccessLayerMessageOfDefaultCollection> GetCollectionOfDefaultByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        #endregion Query Methods Of DefaultByIdentifier

    }
}


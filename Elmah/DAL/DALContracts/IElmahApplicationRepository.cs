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
    public interface IElmahApplicationRepository: Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahApplication>, Elmah.DataSourceEntities.ElmahApplication, Elmah.DataSourceEntities.ElmahApplicationIdentifier, Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahApplication>
    {
        #region Query Methods Of EntityByCommon

        Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahApplication> GetCollectionOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

        Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection> GetCollectionOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

        Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahApplication> GetCollectionOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            );

        #endregion Query Methods Of EntityByIdentifier

    }
}


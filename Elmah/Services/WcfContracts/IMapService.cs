using System.Threading.Tasks;

namespace Elmah.WcfContracts
{
/*
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "Elmah.WcfContracts.IMapService")]
*/
    public interface IMapService
    {
/*
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IMapService/GetMapItems",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IMapService/GetMapItemsResponse")]
*/
        Task<Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection> GetMapItems(
            Elmah.CommonBLLEntities.GeoSearchRequestMessage request);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="Elmah.DataSourceEntities.MapItemCollection"/></returns>
        Task<Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection> GetMessageOfMapItems(
            Elmah.CommonBLLEntities.GeoSearchCriteria criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection);

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="Elmah.DataSourceEntities.MapItemCollection"/></returns>
        Task<Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection> GetMessageOfMapItems(
            Elmah.CommonBLLEntities.GeoSearchCriteria criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType);
    }
}


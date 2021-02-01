using System.Threading.Tasks;

namespace Elmah.DALContracts
{
    public interface IMapRepository
    {
        Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfMapItemCollection> GetMapItems(
            Elmah.EntityContracts.MapItemCategory[] mapItemCategories
            , Framework.Queries.QuerySystemStringContainsCriteria anyText
            , Framework.Queries.QueryGeographyRangeCriteria geographyRange
            , Framework.Queries.QueryGeographyIntersectsCriteria geographyIntersects
            , int currentIndex
            , int pageSize
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection);
    }
}


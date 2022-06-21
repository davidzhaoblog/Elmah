using Framework.Interfaces;
using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahSourceService: IService<ElmahSourceModel, ElmahSourceIdModel>
    {

        Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);

        Task<ElmahSourceCompositeModel> GetCompositeModel(
            ElmahSourceIdModel id, ElmahSourceCompositeDataOptions[]? dataOptions = null);

    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahSourceService
    {

        Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<ElmahSourceCompositeModel> GetCompositeModel(
            ElmahSourceIdModel id, ElmahSourceCompositeDataOptions[]? dataOptions = null);

        Task<Response<ElmahSourceModel>> Update(ElmahSourceModel input);

        Task<Response<ElmahSourceModel>> Get(ElmahSourceIdModel id);

        Task<Response<ElmahSourceModel>> Create(ElmahSourceModel input);
        ElmahSourceModel GetDefault();

        Task<Response> Delete(ElmahSourceIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);

    }
}


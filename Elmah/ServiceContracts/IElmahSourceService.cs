using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahSourceService
    {

        Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<ElmahSourceCompositeModel> GetCompositeModel(
            ElmahSourceIdentifier id, ElmahSourceCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response<ElmahSourceModel>> Update(ElmahSourceIdentifier id, ElmahSourceModel input);

        Task<Response<ElmahSourceModel>> Get(ElmahSourceIdentifier id);

        Task<Response<ElmahSourceModel>> Create(ElmahSourceModel input);
        ElmahSourceModel GetDefault();

        Task<Response> Delete(ElmahSourceIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);

    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahStatusCodeService
    {

        Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query);

        Task<ElmahStatusCodeCompositeModel> GetCompositeModel(
            ElmahStatusCodeIdentifier id, ElmahStatusCodeCompositeDataOptions[]? dataOptions = null);

        Task<Response<ElmahStatusCodeModel>> Update(ElmahStatusCodeIdentifier id, ElmahStatusCodeModel input);

        Task<Response<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdentifier id);

        Task<Response<ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input);
        ElmahStatusCodeModel GetDefault();

        Task<Response> Delete(ElmahStatusCodeIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query);

    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahStatusCodeService
    {

        Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query);

        Task<ElmahStatusCodeCompositeModel> GetCompositeModel(
            ElmahStatusCodeIdModel id, ElmahStatusCodeCompositeDataOptions[]? dataOptions = null);

        Task<Response<ElmahStatusCodeModel>> Update(ElmahStatusCodeModel input);

        Task<Response<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdModel id);

        Task<Response<ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input);
        ElmahStatusCodeModel GetDefault();

        Task<Response> Delete(ElmahStatusCodeIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query);

    }
}


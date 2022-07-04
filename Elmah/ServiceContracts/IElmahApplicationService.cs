using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahApplicationService
    {

        Task<PagedResponse<ElmahApplicationModel[]>> Search(
            ElmahApplicationAdvancedQuery query);

        Task<ElmahApplicationCompositeModel> GetCompositeModel(
            ElmahApplicationIdModel id, ElmahApplicationCompositeDataOptions[]? dataOptions = null);

        Task<Response<ElmahApplicationModel>> Update(ElmahApplicationModel input);

        Task<Response<ElmahApplicationModel>> Get(ElmahApplicationIdModel id);

        Task<Response<ElmahApplicationModel>> Create(ElmahApplicationModel input);
        ElmahApplicationModel GetDefault();

        Task<Response> Delete(ElmahApplicationIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query);

    }
}


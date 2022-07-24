using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahApplicationService
    {

        Task<PagedResponse<ElmahApplicationModel[]>> Search(
            ElmahApplicationAdvancedQuery query);

        Task<ElmahApplicationCompositeModel> GetCompositeModel(
            ElmahApplicationIdentifier id, ElmahApplicationCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahApplicationIdentifier> ids);

        Task<Response<ElmahApplicationModel>> Update(ElmahApplicationIdentifier id, ElmahApplicationModel input);

        Task<Response<ElmahApplicationModel>> Get(ElmahApplicationIdentifier id);

        Task<Response<ElmahApplicationModel>> Create(ElmahApplicationModel input);
        ElmahApplicationModel GetDefault();

        Task<Response> Delete(ElmahApplicationIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query);
    }
}


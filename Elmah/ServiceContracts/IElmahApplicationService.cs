using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahApplicationService
    {

        Task<PagedResponse<ElmahApplicationDataModel[]>> Search(
            ElmahApplicationAdvancedQuery query);

        Task<ElmahApplicationCompositeModel> GetCompositeModel(
            ElmahApplicationIdentifier id,
            Dictionary<ElmahApplicationCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahApplicationCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahApplicationIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationDataModel> input);

        Task<Response<ElmahApplicationDataModel>> Update(ElmahApplicationIdentifier id, ElmahApplicationDataModel input);

        Task<Response<ElmahApplicationDataModel>> Get(ElmahApplicationIdentifier id);

        Task<Response<ElmahApplicationDataModel>> Create(ElmahApplicationDataModel input);
        ElmahApplicationDataModel GetDefault();

        Task<Response> Delete(ElmahApplicationIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query);
    }
}


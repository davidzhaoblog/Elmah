using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahHostService
    {

        Task<PagedResponse<ElmahHostDataModel[]>> Search(
            ElmahHostAdvancedQuery query);

        Task<ElmahHostCompositeModel> GetCompositeModel(
            ElmahHostIdentifier id,
            Dictionary<ElmahHostCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahHostCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahHostIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel> input);

        Task<Response<ElmahHostDataModel>> Update(ElmahHostIdentifier id, ElmahHostDataModel input);

        Task<Response<ElmahHostDataModel>> Get(ElmahHostIdentifier id);

        Task<Response<ElmahHostDataModel>> Create(ElmahHostDataModel input);
        ElmahHostDataModel GetDefault();

        Task<Response> Delete(ElmahHostIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query);
    }
}


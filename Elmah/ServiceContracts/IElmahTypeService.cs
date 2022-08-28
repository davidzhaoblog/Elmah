using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahTypeService
    {

        Task<PagedResponse<ElmahTypeDataModel[]>> Search(
            ElmahTypeAdvancedQuery query);

        Task<ElmahTypeCompositeModel> GetCompositeModel(
            ElmahTypeIdentifier id,
            Dictionary<ElmahTypeCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahTypeCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahTypeIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel> input);

        Task<Response<ElmahTypeDataModel>> Update(ElmahTypeIdentifier id, ElmahTypeDataModel input);

        Task<Response<ElmahTypeDataModel>> Get(ElmahTypeIdentifier id);

        Task<Response<ElmahTypeDataModel>> Create(ElmahTypeDataModel input);
        ElmahTypeDataModel GetDefault();

        Task<Response> Delete(ElmahTypeIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query);
    }
}


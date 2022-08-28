using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahSourceService
    {

        Task<PagedResponse<ElmahSourceDataModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<ElmahSourceCompositeModel> GetCompositeModel(
            ElmahSourceIdentifier id,
            Dictionary<ElmahSourceCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahSourceCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahSourceIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel> input);

        Task<Response<ElmahSourceDataModel>> Update(ElmahSourceIdentifier id, ElmahSourceDataModel input);

        Task<Response<ElmahSourceDataModel>> Get(ElmahSourceIdentifier id);

        Task<Response<ElmahSourceDataModel>> Create(ElmahSourceDataModel input);
        ElmahSourceDataModel GetDefault();

        Task<Response> Delete(ElmahSourceIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);
    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahStatusCodeService
    {

        Task<ListResponse<ElmahStatusCodeDataModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query);

        Task<ElmahStatusCodeCompositeModel> GetCompositeModel(
            ElmahStatusCodeIdentifier id,
            Dictionary<ElmahStatusCodeCompositeModel.__DataOptions__, CompositeListItemRequest> listItemRequest,
            ElmahStatusCodeCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response<ElmahStatusCodeDataModel>> Update(ElmahStatusCodeIdentifier id, ElmahStatusCodeDataModel input);

        Task<Response<ElmahStatusCodeDataModel>> Get(ElmahStatusCodeIdentifier id);

        Task<Response<ElmahStatusCodeDataModel>> Create(ElmahStatusCodeDataModel input);
        ElmahStatusCodeDataModel GetDefault();

        Task<Response> Delete(ElmahStatusCodeIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query);
    }
}


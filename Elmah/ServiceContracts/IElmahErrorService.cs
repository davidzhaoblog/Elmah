using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahErrorService
    {

        Task<PagedResponse<ElmahErrorModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query);

        Task<ElmahErrorCompositeModel> GetCompositeModel(
            ElmahErrorIdentifier id, ElmahErrorCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahErrorIdentifier> ids);

        Task<Response> BulkUpdate(BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorModel.DefaultView> data);

        Task<Response<ElmahErrorModel.DefaultView>> Update(ElmahErrorIdentifier id, ElmahErrorModel input);

        Task<Response<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdentifier id);

        Task<Response<ElmahErrorModel.DefaultView>> Create(ElmahErrorModel input);
        ElmahErrorModel.DefaultView GetDefault();

        Task<Response> Delete(ElmahErrorIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahErrorAdvancedQuery query);
    }
}


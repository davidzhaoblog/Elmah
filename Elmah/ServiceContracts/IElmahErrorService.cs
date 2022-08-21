using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahErrorService
    {

        Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query);

        Task<ElmahErrorCompositeModel> GetCompositeModel(
            ElmahErrorIdentifier id, ElmahErrorCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahErrorIdentifier> ids);

        Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>> BulkUpdate(BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> data);

        Task<Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> input);

        Task<Response<ElmahErrorDataModel.DefaultView>> Update(ElmahErrorIdentifier id, ElmahErrorDataModel input);

        Task<Response<ElmahErrorDataModel.DefaultView>> Get(ElmahErrorIdentifier id);

        Task<Response<ElmahErrorDataModel.DefaultView>> Create(ElmahErrorDataModel input);
        ElmahErrorDataModel.DefaultView GetDefault();

        Task<Response> Delete(ElmahErrorIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahErrorAdvancedQuery query);
    }
}


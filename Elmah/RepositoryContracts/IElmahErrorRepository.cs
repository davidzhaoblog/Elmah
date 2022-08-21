using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahErrorRepository
    {

        Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahErrorIdentifier> ids);

        Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>> BulkUpdate(BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> data);

        Task<Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> input);

        Task<Response<ElmahErrorDataModel.DefaultView>> Update(ElmahErrorIdentifier id, ElmahErrorDataModel input);

        Task<Response<ElmahErrorDataModel.DefaultView>> Get(ElmahErrorIdentifier id);

        Task<Response<ElmahErrorDataModel.DefaultView>> Create(ElmahErrorDataModel input);

        Task<Response> Delete(ElmahErrorIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahErrorAdvancedQuery query);
    }
}


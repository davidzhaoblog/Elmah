using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahErrorRepository
    {

        Task<PagedResponse<ElmahErrorModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahErrorIdentifier> ids);

        Task<PagedResponse<ElmahErrorModel.DefaultView[]>> BulkUpdate(BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorModel.DefaultView> data);

        Task<Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorModel.DefaultView> input);

        Task<Response<ElmahErrorModel.DefaultView>> Update(ElmahErrorIdentifier id, ElmahErrorModel input);

        Task<Response<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdentifier id);

        Task<Response<ElmahErrorModel.DefaultView>> Create(ElmahErrorModel input);

        Task<Response> Delete(ElmahErrorIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahErrorAdvancedQuery query);
    }
}


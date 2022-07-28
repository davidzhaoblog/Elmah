using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahErrorRepository
    {

        Task<PagedResponse<ElmahErrorModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query);

        Task<Response<ElmahErrorModel.DefaultView>> Update(ElmahErrorModel input);

        Task<Response<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdentifier id);

        Task<Response<ElmahErrorModel.DefaultView>> Create(ElmahErrorModel input);

        Task<Response> Delete(ElmahErrorIdentifier id);

        Task<Response> BatchDelete(List<ElmahErrorIdentifier> ids);
        Task<Framework.Models.PagedResponse<List<Elmah.Models.ElmahErrorModel.DefaultView>>> BulkUpdate(Framework.Models.BatchActionViewModel<ElmahErrorIdentifier, Elmah.Models.ElmahErrorModel.DefaultView> data);

    }
}


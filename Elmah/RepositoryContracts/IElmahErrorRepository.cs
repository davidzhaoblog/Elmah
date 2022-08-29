using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahErrorRepository
    {

        Task<ListResponse<ElmahErrorDataModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query);

        Task<ListResponse<ElmahErrorDataModel.DefaultView[]>> BulkUpdate(BatchActionRequest<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> data);

        Task<Response<ElmahErrorDataModel.DefaultView>> Get(ElmahErrorIdentifier id);
    }
}


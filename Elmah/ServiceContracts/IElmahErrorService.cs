using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahErrorService
    {

        Task<PagedResponse<ElmahErrorModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahErrorIdentifier> ids);

        Task<Response<ElmahErrorModel.DefaultView>> Update(ElmahErrorIdentifier id, ElmahErrorModel input);

        Task<Response<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdentifier id);

        Task<Response<ElmahErrorModel.DefaultView>> Create(ElmahErrorModel input);
        ElmahErrorModel.DefaultView GetDefault();

        Task<Response> Delete(ElmahErrorIdentifier id);
    }
}


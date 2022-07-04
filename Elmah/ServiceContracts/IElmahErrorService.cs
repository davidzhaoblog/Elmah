using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahErrorService
    {

        Task<PagedResponse<ElmahErrorModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query);

        Task<Response<ElmahErrorModel.DefaultView>> Update(ElmahErrorModel input);

        Task<Response<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdModel id);

        Task<Response<ElmahErrorModel.DefaultView>> Create(ElmahErrorModel input);
        ElmahErrorModel.DefaultView GetDefault();

        Task<Response> Delete(ElmahErrorIdModel id);

    }
}


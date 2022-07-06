using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahApplicationRepository
    {

        Task<PagedResponse<ElmahApplicationModel[]>> Search(
            ElmahApplicationAdvancedQuery query);

        Task<Response<ElmahApplicationModel>> Update(ElmahApplicationModel input);

        Task<Response<ElmahApplicationModel>> Get(ElmahApplicationIdentifier id);

        Task<Response<ElmahApplicationModel>> Create(ElmahApplicationModel input);

        Task<Response> Delete(ElmahApplicationIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query);

    }
}


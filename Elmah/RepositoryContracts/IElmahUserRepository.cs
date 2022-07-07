using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahUserRepository
    {

        Task<PagedResponse<ElmahUserModel[]>> Search(
            ElmahUserAdvancedQuery query);

        Task<Response<ElmahUserModel>> Update(ElmahUserIdentifier id, ElmahUserModel input);

        Task<Response<ElmahUserModel>> Get(ElmahUserIdentifier id);

        Task<Response<ElmahUserModel>> Create(ElmahUserModel input);

        Task<Response> Delete(ElmahUserIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query);

    }
}


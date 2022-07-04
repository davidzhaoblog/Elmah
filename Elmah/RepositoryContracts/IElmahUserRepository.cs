using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahUserRepository
    {

        Task<PagedResponse<ElmahUserModel[]>> Search(
            ElmahUserAdvancedQuery query);

        Task<Response<ElmahUserModel>> Update(ElmahUserModel input);

        Task<Response<ElmahUserModel>> Get(ElmahUserIdModel id);

        Task<Response<ElmahUserModel>> Create(ElmahUserModel input);

        Task<Response> Delete(ElmahUserIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query);

    }
}


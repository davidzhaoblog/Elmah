using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahUserRepository
    {

        Task<ListResponse<ElmahUserDataModel[]>> Search(
            ElmahUserAdvancedQuery query);

        Task<Response<ElmahUserDataModel>> Update(ElmahUserIdentifier id, ElmahUserDataModel input);

        Task<Response<ElmahUserDataModel>> Get(ElmahUserIdentifier id);

        Task<Response<ElmahUserDataModel>> Create(ElmahUserDataModel input);

        Task<Response> Delete(ElmahUserIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query);
    }
}


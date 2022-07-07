using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahHostRepository
    {

        Task<PagedResponse<ElmahHostModel[]>> Search(
            ElmahHostAdvancedQuery query);

        Task<Response<ElmahHostModel>> Update(ElmahHostIdentifier id, ElmahHostModel input);

        Task<Response<ElmahHostModel>> Get(ElmahHostIdentifier id);

        Task<Response<ElmahHostModel>> Create(ElmahHostModel input);

        Task<Response> Delete(ElmahHostIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query);

    }
}


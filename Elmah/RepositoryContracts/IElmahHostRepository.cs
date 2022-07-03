using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahHostRepository
    {

        Task<PagedResponse<ElmahHostModel[]>> Search(
            ElmahHostAdvancedQuery query);

        Task<Response<ElmahHostModel>> Update(ElmahHostModel input);

        Task<Response<ElmahHostModel>> Get(ElmahHostIdModel id);

        Task<Response<ElmahHostModel>> Create(ElmahHostModel input);

        Task<Response> Delete(ElmahHostIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query);

    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahSourceRepository
    {

        Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<Response<ElmahSourceModel>> Update(ElmahSourceModel input);

        Task<Response<ElmahSourceModel>> Get(ElmahSourceIdModel id);

        Task<Response<ElmahSourceModel>> Create(ElmahSourceModel input);

        Task<Response> Delete(ElmahSourceIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);

    }
}


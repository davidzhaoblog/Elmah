using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahSourceRepository
    {

        Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahSourceIdentifier> ids);

        Task<Response<ElmahSourceModel>> Update(ElmahSourceIdentifier id, ElmahSourceModel input);

        Task<Response<ElmahSourceModel>> Get(ElmahSourceIdentifier id);

        Task<Response<ElmahSourceModel>> Create(ElmahSourceModel input);

        Task<Response> Delete(ElmahSourceIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);
    }
}


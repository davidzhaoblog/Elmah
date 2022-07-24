using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahStatusCodeRepository
    {

        Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahStatusCodeIdentifier> ids);

        Task<Response<ElmahStatusCodeModel>> Update(ElmahStatusCodeIdentifier id, ElmahStatusCodeModel input);

        Task<Response<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdentifier id);

        Task<Response<ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input);

        Task<Response> Delete(ElmahStatusCodeIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query);
    }
}


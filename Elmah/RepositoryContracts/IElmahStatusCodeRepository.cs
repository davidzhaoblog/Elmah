using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahStatusCodeRepository
    {

        Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query);

        Task<Response<ElmahStatusCodeModel>> Update(ElmahStatusCodeModel input);

        Task<Response<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdModel id);

        Task<Response<ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input);

        Task<Response> Delete(ElmahStatusCodeIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query);

    }
}


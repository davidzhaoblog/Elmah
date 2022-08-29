using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahStatusCodeRepository
    {

        Task<ListResponse<ElmahStatusCodeDataModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query);

        Task<Response<ElmahStatusCodeDataModel>> Update(ElmahStatusCodeIdentifier id, ElmahStatusCodeDataModel input);

        Task<Response<ElmahStatusCodeDataModel>> Get(ElmahStatusCodeIdentifier id);

        Task<Response<ElmahStatusCodeDataModel>> Create(ElmahStatusCodeDataModel input);

        Task<Response> Delete(ElmahStatusCodeIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query);
    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahApplicationRepository
    {

        Task<ListResponse<ElmahApplicationDataModel[]>> Search(
            ElmahApplicationAdvancedQuery query);

        Task<Response<ElmahApplicationDataModel>> Update(ElmahApplicationIdentifier id, ElmahApplicationDataModel input);

        Task<Response<ElmahApplicationDataModel>> Get(ElmahApplicationIdentifier id);

        Task<Response<ElmahApplicationDataModel>> Create(ElmahApplicationDataModel input);

        Task<Response> Delete(ElmahApplicationIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query);
    }
}


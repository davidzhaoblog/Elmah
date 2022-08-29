using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahSourceRepository
    {

        Task<ListResponse<ElmahSourceDataModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<Response<ElmahSourceDataModel>> Update(ElmahSourceIdentifier id, ElmahSourceDataModel input);

        Task<Response<ElmahSourceDataModel>> Get(ElmahSourceIdentifier id);

        Task<Response<ElmahSourceDataModel>> Create(ElmahSourceDataModel input);

        Task<Response> Delete(ElmahSourceIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);
    }
}


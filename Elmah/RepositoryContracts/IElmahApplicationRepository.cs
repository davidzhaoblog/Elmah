using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahApplicationRepository
    {

        Task<PagedResponse<ElmahApplicationDataModel[]>> Search(
            ElmahApplicationAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahApplicationIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationDataModel> input);

        Task<Response<ElmahApplicationDataModel>> Update(ElmahApplicationIdentifier id, ElmahApplicationDataModel input);

        Task<Response<ElmahApplicationDataModel>> Get(ElmahApplicationIdentifier id);

        Task<Response<ElmahApplicationDataModel>> Create(ElmahApplicationDataModel input);

        Task<Response> Delete(ElmahApplicationIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query);
    }
}


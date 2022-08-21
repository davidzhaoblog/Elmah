using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahHostRepository
    {

        Task<PagedResponse<ElmahHostDataModel[]>> Search(
            ElmahHostAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahHostIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel> input);

        Task<Response<ElmahHostDataModel>> Update(ElmahHostIdentifier id, ElmahHostDataModel input);

        Task<Response<ElmahHostDataModel>> Get(ElmahHostIdentifier id);

        Task<Response<ElmahHostDataModel>> Create(ElmahHostDataModel input);

        Task<Response> Delete(ElmahHostIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query);
    }
}


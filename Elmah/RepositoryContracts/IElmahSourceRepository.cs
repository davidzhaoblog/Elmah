using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahSourceRepository
    {

        Task<PagedResponse<ElmahSourceDataModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahSourceIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel> input);

        Task<Response<ElmahSourceDataModel>> Update(ElmahSourceIdentifier id, ElmahSourceDataModel input);

        Task<Response<ElmahSourceDataModel>> Get(ElmahSourceIdentifier id);

        Task<Response<ElmahSourceDataModel>> Create(ElmahSourceDataModel input);

        Task<Response> Delete(ElmahSourceIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);
    }
}


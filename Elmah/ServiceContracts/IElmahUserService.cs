using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahUserService
    {

        Task<PagedResponse<ElmahUserDataModel[]>> Search(
            ElmahUserAdvancedQuery query);

        Task<ElmahUserCompositeModel> GetCompositeModel(
            ElmahUserIdentifier id, ElmahUserCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahUserIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserDataModel> input);

        Task<Response<ElmahUserDataModel>> Update(ElmahUserIdentifier id, ElmahUserDataModel input);

        Task<Response<ElmahUserDataModel>> Get(ElmahUserIdentifier id);

        Task<Response<ElmahUserDataModel>> Create(ElmahUserDataModel input);
        ElmahUserDataModel GetDefault();

        Task<Response> Delete(ElmahUserIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query);
    }
}


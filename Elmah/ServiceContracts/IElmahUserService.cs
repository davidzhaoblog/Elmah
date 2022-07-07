using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahUserService
    {

        Task<PagedResponse<ElmahUserModel[]>> Search(
            ElmahUserAdvancedQuery query);

        Task<ElmahUserCompositeModel> GetCompositeModel(
            ElmahUserIdentifier id, ElmahUserCompositeDataOptions[]? dataOptions = null);

        Task<Response<ElmahUserModel>> Update(ElmahUserIdentifier id, ElmahUserModel input);

        Task<Response<ElmahUserModel>> Get(ElmahUserIdentifier id);

        Task<Response<ElmahUserModel>> Create(ElmahUserModel input);
        ElmahUserModel GetDefault();

        Task<Response> Delete(ElmahUserIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query);

    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahUserService
    {

        Task<PagedResponse<ElmahUserModel[]>> Search(
            ElmahUserAdvancedQuery query);

        Task<ElmahUserCompositeModel> GetCompositeModel(
            ElmahUserIdModel id, ElmahUserCompositeDataOptions[]? dataOptions = null);

        Task<Response<ElmahUserModel>> Update(ElmahUserModel input);

        Task<Response<ElmahUserModel>> Get(ElmahUserIdModel id);

        Task<Response<ElmahUserModel>> Create(ElmahUserModel input);
        ElmahUserModel GetDefault();

        Task<Response> Delete(ElmahUserIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query);

    }
}


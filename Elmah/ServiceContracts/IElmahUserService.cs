using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahUserService
    {

        Task<ListResponse<ElmahUserDataModel[]>> Search(
            ElmahUserAdvancedQuery query);

        Task<Response<ElmahUserDataModel>> Update(ElmahUserIdentifier id, ElmahUserDataModel input);

        Task<Response<ElmahUserDataModel>> Get(ElmahUserIdentifier id);

        Task<Response<ElmahUserDataModel>> Create(ElmahUserDataModel input);
        ElmahUserDataModel GetDefault();

        Task<Response> Delete(ElmahUserIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahUserAdvancedQuery query);
    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahApplicationService
    {

        Task<ListResponse<ElmahApplicationDataModel[]>> Search(
            ElmahApplicationAdvancedQuery query);

        Task<Response<ElmahApplicationDataModel>> Update(ElmahApplicationIdentifier id, ElmahApplicationDataModel input);

        Task<Response<ElmahApplicationDataModel>> Get(ElmahApplicationIdentifier id);

        Task<Response<ElmahApplicationDataModel>> Create(ElmahApplicationDataModel input);
        ElmahApplicationDataModel GetDefault();

        Task<Response> Delete(ElmahApplicationIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query);
    }
}


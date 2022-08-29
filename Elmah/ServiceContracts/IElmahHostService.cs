using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahHostService
    {

        Task<ListResponse<ElmahHostDataModel[]>> Search(
            ElmahHostAdvancedQuery query);

        Task<Response<ElmahHostDataModel>> Update(ElmahHostIdentifier id, ElmahHostDataModel input);

        Task<Response<ElmahHostDataModel>> Get(ElmahHostIdentifier id);

        Task<Response<ElmahHostDataModel>> Create(ElmahHostDataModel input);
        ElmahHostDataModel GetDefault();

        Task<Response> Delete(ElmahHostIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query);
    }
}


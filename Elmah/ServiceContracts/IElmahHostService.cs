using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahHostService
    {

        Task<PagedResponse<ElmahHostModel[]>> Search(
            ElmahHostAdvancedQuery query);

        Task<ElmahHostCompositeModel> GetCompositeModel(
            ElmahHostIdentifier id, ElmahHostCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response<ElmahHostModel>> Update(ElmahHostIdentifier id, ElmahHostModel input);

        Task<Response<ElmahHostModel>> Get(ElmahHostIdentifier id);

        Task<Response<ElmahHostModel>> Create(ElmahHostModel input);
        ElmahHostModel GetDefault();

        Task<Response> Delete(ElmahHostIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query);

    }
}


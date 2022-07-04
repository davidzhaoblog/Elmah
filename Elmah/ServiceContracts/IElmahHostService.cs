using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahHostService
    {

        Task<PagedResponse<ElmahHostModel[]>> Search(
            ElmahHostAdvancedQuery query);

        Task<ElmahHostCompositeModel> GetCompositeModel(
            ElmahHostIdModel id, ElmahHostCompositeDataOptions[]? dataOptions = null);

        Task<Response<ElmahHostModel>> Update(ElmahHostModel input);

        Task<Response<ElmahHostModel>> Get(ElmahHostIdModel id);

        Task<Response<ElmahHostModel>> Create(ElmahHostModel input);
        ElmahHostModel GetDefault();

        Task<Response> Delete(ElmahHostIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query);

    }
}


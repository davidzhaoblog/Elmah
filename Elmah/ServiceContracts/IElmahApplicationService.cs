using Framework.Interfaces;
using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahApplicationService: IService<ElmahApplicationModel, ElmahApplicationIdModel>
    {

        Task<PagedResponse<ElmahApplicationModel[]>> Search(
            ElmahApplicationAdvancedQuery query);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahApplicationAdvancedQuery query);

        Task<ElmahApplicationCompositeModel> GetCompositeModel(
            ElmahApplicationIdModel id, ElmahApplicationCompositeDataOptions[]? dataOptions = null);

    }
}


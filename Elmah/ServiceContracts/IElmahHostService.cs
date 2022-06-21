using Framework.Interfaces;
using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahHostService: IService<ElmahHostModel, ElmahHostIdModel>
    {

        Task<PagedResponse<ElmahHostModel[]>> Search(
            ElmahHostAdvancedQuery query);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahHostAdvancedQuery query);

        Task<ElmahHostCompositeModel> GetCompositeModel(
            ElmahHostIdModel id, ElmahHostCompositeDataOptions[]? dataOptions = null);

    }
}


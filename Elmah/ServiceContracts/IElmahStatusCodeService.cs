using Framework.Interfaces;
using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahStatusCodeService: IService<ElmahStatusCodeModel, ElmahStatusCodeIdModel>
    {

        Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query);

        Task<ElmahStatusCodeCompositeModel> GetCompositeModel(
            ElmahStatusCodeIdModel id, ElmahStatusCodeCompositeDataOptions[]? dataOptions = null);

    }
}


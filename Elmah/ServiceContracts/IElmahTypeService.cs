using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahTypeService
    {

        Task<PagedResponse<ElmahTypeModel[]>> Search(
            ElmahTypeAdvancedQuery query);

        Task<ElmahTypeCompositeModel> GetCompositeModel(
            ElmahTypeIdModel id, ElmahTypeCompositeDataOptions[]? dataOptions = null);

        Task<Response<ElmahTypeModel>> Update(ElmahTypeModel input);

        Task<Response<ElmahTypeModel>> Get(ElmahTypeIdModel id);

        Task<Response<ElmahTypeModel>> Create(ElmahTypeModel input);
        ElmahTypeModel GetDefault();

        Task<Response> Delete(ElmahTypeIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query);

    }
}


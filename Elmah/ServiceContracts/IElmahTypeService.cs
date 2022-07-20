using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahTypeService
    {

        Task<PagedResponse<ElmahTypeModel[]>> Search(
            ElmahTypeAdvancedQuery query);

        Task<ElmahTypeCompositeModel> GetCompositeModel(
            ElmahTypeIdentifier id, ElmahTypeCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response<ElmahTypeModel>> Update(ElmahTypeIdentifier id, ElmahTypeModel input);

        Task<Response<ElmahTypeModel>> Get(ElmahTypeIdentifier id);

        Task<Response<ElmahTypeModel>> Create(ElmahTypeModel input);
        ElmahTypeModel GetDefault();

        Task<Response> Delete(ElmahTypeIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query);
    }
}


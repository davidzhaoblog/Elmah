using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahStatusCodeService
    {

        Task<PagedResponse<ElmahStatusCodeDataModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query);

        Task<ElmahStatusCodeCompositeModel> GetCompositeModel(
            ElmahStatusCodeIdentifier id, ElmahStatusCodeCompositeModel.__DataOptions__[]? dataOptions = null);

        Task<Response> BulkDelete(List<ElmahStatusCodeIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahStatusCodeIdentifier, ElmahStatusCodeDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahStatusCodeIdentifier, ElmahStatusCodeDataModel> input);

        Task<Response<ElmahStatusCodeDataModel>> Update(ElmahStatusCodeIdentifier id, ElmahStatusCodeDataModel input);

        Task<Response<ElmahStatusCodeDataModel>> Get(ElmahStatusCodeIdentifier id);

        Task<Response<ElmahStatusCodeDataModel>> Create(ElmahStatusCodeDataModel input);
        ElmahStatusCodeDataModel GetDefault();

        Task<Response> Delete(ElmahStatusCodeIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query);
    }
}


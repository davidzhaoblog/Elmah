using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahTypeRepository
    {

        Task<PagedResponse<ElmahTypeDataModel[]>> Search(
            ElmahTypeAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahTypeIdentifier> ids);

        Task<Response<MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel> input);

        Task<Response<ElmahTypeDataModel>> Update(ElmahTypeIdentifier id, ElmahTypeDataModel input);

        Task<Response<ElmahTypeDataModel>> Get(ElmahTypeIdentifier id);

        Task<Response<ElmahTypeDataModel>> Create(ElmahTypeDataModel input);

        Task<Response> Delete(ElmahTypeIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query);
    }
}


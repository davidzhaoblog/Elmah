using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahTypeRepository
    {

        Task<PagedResponse<ElmahTypeModel[]>> Search(
            ElmahTypeAdvancedQuery query);

        Task<Response> BulkDelete(List<ElmahTypeIdentifier> ids);

        Task<Response<ElmahTypeModel>> Update(ElmahTypeIdentifier id, ElmahTypeModel input);

        Task<Response<ElmahTypeModel>> Get(ElmahTypeIdentifier id);

        Task<Response<ElmahTypeModel>> Create(ElmahTypeModel input);

        Task<Response> Delete(ElmahTypeIdentifier id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query);
    }
}


using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahTypeRepository
    {

        Task<PagedResponse<ElmahTypeModel[]>> Search(
            ElmahTypeAdvancedQuery query);

        Task<Response<ElmahTypeModel>> Update(ElmahTypeModel input);

        Task<Response<ElmahTypeModel>> Get(ElmahTypeIdModel id);

        Task<Response<ElmahTypeModel>> Create(ElmahTypeModel input);

        Task<Response> Delete(ElmahTypeIdModel id);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query);

    }
}


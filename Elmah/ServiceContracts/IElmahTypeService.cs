using Elmah.Models;
using Framework.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahTypeService
    {

        Task<ListResponse<ElmahTypeDataModel[]>> Search(
            ElmahTypeAdvancedQuery query);

        Task<Response<ElmahTypeDataModel>> Update(ElmahTypeIdentifier id, ElmahTypeDataModel input);

        Task<Response<ElmahTypeDataModel>> Get(ElmahTypeIdentifier id);

        Task<Response<ElmahTypeDataModel>> Create(ElmahTypeDataModel input);
        ElmahTypeDataModel GetDefault();

        Task<Response> Delete(ElmahTypeIdentifier id);

        Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahTypeAdvancedQuery query);
    }
}


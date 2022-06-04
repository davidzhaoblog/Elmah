using Framework.Interfaces;
using Elmah.Models;
using Framework.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahSourceRepository: IRepository<ElmahSourceModel, ElmahSourceIdModel>
    {

        Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query);

        Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query);
    }
}


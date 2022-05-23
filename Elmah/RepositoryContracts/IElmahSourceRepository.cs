using Framework.Interfaces;
using Elmah.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahSourceRepository: IRepository<ElmahSourceModel, ElmahSourceIdModel>
    {
    }
}


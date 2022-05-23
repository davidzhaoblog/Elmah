using Framework.Interfaces;
using Elmah.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahErrorRepository: IRepository<ElmahErrorModel, ElmahErrorIdModel>
    {
    }
}


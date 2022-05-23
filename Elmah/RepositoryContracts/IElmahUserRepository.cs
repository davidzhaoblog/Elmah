using Framework.Interfaces;
using Elmah.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahUserRepository: IRepository<ElmahUserModel, ElmahUserIdModel>
    {
    }
}


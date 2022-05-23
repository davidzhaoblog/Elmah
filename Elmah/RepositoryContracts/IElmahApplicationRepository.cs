using Framework.Interfaces;
using Elmah.Models;
namespace Elmah.RepositoryContracts
{
    public interface IElmahApplicationRepository: IRepository<ElmahApplicationModel, ElmahApplicationIdModel>
    {
    }
}


using Framework.Interfaces;
using Elmah.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahApplicationService: IService<ElmahApplicationModel, ElmahApplicationIdModel>
    {
    }
}


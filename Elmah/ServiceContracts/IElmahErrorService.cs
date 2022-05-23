using Framework.Interfaces;
using Elmah.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahErrorService: IService<ElmahErrorModel, ElmahErrorIdModel>
    {
    }
}


using Framework.Interfaces;
using Elmah.Models;
namespace Elmah.ServiceContracts
{
    public interface IElmahUserService: IService<ElmahUserModel, ElmahUserIdModel>
    {
    }
}


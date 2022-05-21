using System.Net;

namespace Framework.Interfaces
{
    public interface IRepository<T, TIdentifier>
    {
        Task<Framework.Models.Response<HttpStatusCode, T>> Create(T input);
        Task<Framework.Models.Response<HttpStatusCode, T>> Delete(TIdentifier id);
        Task<Framework.Models.Response<HttpStatusCode, T>> Get(TIdentifier id);
        Task<Framework.Models.Response<HttpStatusCode, T>> Update(T input);
    }
}


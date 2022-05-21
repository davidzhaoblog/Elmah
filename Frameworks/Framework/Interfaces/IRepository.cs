using Framework.Models;
using System.Net;

namespace Framework.Interfaces
{
    public interface IRepository<T, TIdentifier>
    {
        Task<Response<HttpStatusCode, T>> Create(T input);
        Task<Response<HttpStatusCode, T>> Delete(TIdentifier id);
        Task<Response<HttpStatusCode, T>> Get(TIdentifier id);
        Task<Response<HttpStatusCode, T>> Update(T input);
    }
}


using Framework.Models;
using System.Net;

namespace Framework.Interfaces
{
    public interface IRepository<T, TIdentifier>: IRepository<T, T, TIdentifier>
    {
    }

    public interface IRepository<TRequest, TResponse, TIdentifier>
    {
        Task<Response<HttpStatusCode, TResponse>> Create(TRequest input);
        Task<Response<HttpStatusCode, TResponse>> Delete(TIdentifier id);
        Task<Response<HttpStatusCode, TResponse>> Get(TIdentifier id);
        Task<Response<HttpStatusCode, TResponse>> Update(TRequest input);
    }
}


using Framework.Models;
using System.Net;

namespace Framework.Interfaces
{
    public interface IService<T, TIdentifier>: IService<T, T, TIdentifier>
    {
    }

    public interface IService<TRequest, TResponse, TIdentifier>
    {
        Task<Response<TResponse>> Create(TRequest input);
        Task<Response> Delete(TIdentifier id);
        Task<Response<TResponse>> Get(TIdentifier id);
        Task<Response<TResponse>> Update(TRequest input);
    }
}


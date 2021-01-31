using System;
namespace Framework.Models
{
    public interface IDataStreamServiceProviderBase<TCollection, TItem>
        where TCollection : System.Collections.Generic.List<TItem>
    {
        Framework.Models.DataStreamServiceResult BuildResult(TCollection input, Framework.Models.DataServiceTypes dataServiceType);
    }
}


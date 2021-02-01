using System;
using System.Runtime.Serialization;

namespace Elmah.DataSourceEntities
{
    public partial class ElmahHostIdentifier
        : Elmah.EntityContracts.IElmahHostIdentifier, Framework.Models.IClone<Elmah.EntityContracts.IElmahHostIdentifier>
    {

        public System.String Host { get; set; }

        public T GetAClone<T>()
            where T: Elmah.EntityContracts.IElmahHostIdentifier, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahHostIdentifier
        {
            destination.Host = Host;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahHostIdentifier
        {
            Host = source.Host;

        }
    }
}


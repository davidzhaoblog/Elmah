using System;
using System.Runtime.Serialization;

namespace Elmah.DataSourceEntities
{
    public partial class ElmahUserIdentifier
        : Elmah.EntityContracts.IElmahUserIdentifier, Framework.Models.IClone<Elmah.EntityContracts.IElmahUserIdentifier>
    {

        public System.String User { get; set; }

        public T GetAClone<T>()
            where T: Elmah.EntityContracts.IElmahUserIdentifier, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahUserIdentifier
        {
            destination.User = User;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahUserIdentifier
        {
            User = source.User;

        }
    }
}


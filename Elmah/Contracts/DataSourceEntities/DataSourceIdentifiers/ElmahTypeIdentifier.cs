using System;
using System.Runtime.Serialization;

namespace Elmah.DataSourceEntities
{
    public partial class ElmahTypeIdentifier
        : Elmah.EntityContracts.IElmahTypeIdentifier, Framework.Models.IClone<Elmah.EntityContracts.IElmahTypeIdentifier>
    {

        public System.String Type { get; set; }

        public T GetAClone<T>()
            where T: Elmah.EntityContracts.IElmahTypeIdentifier, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahTypeIdentifier
        {
            destination.Type = Type;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahTypeIdentifier
        {
            Type = source.Type;

        }
    }
}


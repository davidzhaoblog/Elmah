using System;
using System.Runtime.Serialization;

namespace Elmah.DataSourceEntities
{
    public partial class ElmahStatusCodeIdentifier
        : Elmah.EntityContracts.IElmahStatusCodeIdentifier, Framework.Models.IClone<Elmah.EntityContracts.IElmahStatusCodeIdentifier>
    {

        public System.Int32 StatusCode { get; set; }

        public T GetAClone<T>()
            where T: Elmah.EntityContracts.IElmahStatusCodeIdentifier, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahStatusCodeIdentifier
        {
            destination.StatusCode = StatusCode;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahStatusCodeIdentifier
        {
            StatusCode = source.StatusCode;

        }
    }
}


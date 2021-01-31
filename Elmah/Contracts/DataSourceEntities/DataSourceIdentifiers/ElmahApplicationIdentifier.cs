using System;
using System.Runtime.Serialization;

namespace Elmah.DataSourceEntities
{
    public partial class ElmahApplicationIdentifier
        : Elmah.EntityContracts.IElmahApplicationIdentifier, Framework.Models.IClone<Elmah.EntityContracts.IElmahApplicationIdentifier>
    {

        public System.String Application { get; set; }

        public T GetAClone<T>()
            where T: Elmah.EntityContracts.IElmahApplicationIdentifier, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahApplicationIdentifier
        {
            destination.Application = Application;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahApplicationIdentifier
        {
            Application = source.Application;

        }
    }
}


using System;
using System.Runtime.Serialization;

namespace Elmah.DataSourceEntities
{
    public partial class ElmahSourceIdentifier
        : Elmah.EntityContracts.IElmahSourceIdentifier, Framework.Models.IClone<Elmah.EntityContracts.IElmahSourceIdentifier>
    {

        public System.String Source { get; set; }

        public T GetAClone<T>()
            where T: Elmah.EntityContracts.IElmahSourceIdentifier, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahSourceIdentifier
        {
            destination.Source = Source;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahSourceIdentifier
        {
            Source = source.Source;

        }
    }
}


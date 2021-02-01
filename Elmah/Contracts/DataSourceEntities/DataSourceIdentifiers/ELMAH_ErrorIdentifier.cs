using System;
using System.Runtime.Serialization;

namespace Elmah.DataSourceEntities
{
    public partial class ELMAH_ErrorIdentifier
        : Elmah.EntityContracts.IELMAH_ErrorIdentifier, Framework.Models.IClone<Elmah.EntityContracts.IELMAH_ErrorIdentifier>
    {

        public System.Guid ErrorId { get; set; }

        public T GetAClone<T>()
            where T: Elmah.EntityContracts.IELMAH_ErrorIdentifier, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IELMAH_ErrorIdentifier
        {
            destination.ErrorId = ErrorId;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IELMAH_ErrorIdentifier
        {
            ErrorId = source.ErrorId;

        }
    }
}


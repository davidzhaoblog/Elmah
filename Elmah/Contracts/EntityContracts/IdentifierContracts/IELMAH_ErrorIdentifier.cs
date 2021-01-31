using System;
using System.Runtime.Serialization;

namespace Elmah.EntityContracts
{

    public partial interface IELMAH_ErrorIdentifier
    {

        System.Guid ErrorId { get; set; }

    }
}


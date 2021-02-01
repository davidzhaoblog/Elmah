using System;
using System.Runtime.Serialization;

namespace Elmah.EntityContracts
{

    public partial interface IElmahTypeIdentifier
    {

        System.String Type { get; set; }

    }
}


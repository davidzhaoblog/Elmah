using System;
using System.Runtime.Serialization;

namespace Elmah.EntityContracts
{

    public partial interface IElmahHostIdentifier
    {

        System.String Host { get; set; }

    }
}


using System;
using System.Runtime.Serialization;

namespace Elmah.EntityContracts
{

    public partial interface IElmahApplicationIdentifier
    {

        System.String Application { get; set; }

    }
}


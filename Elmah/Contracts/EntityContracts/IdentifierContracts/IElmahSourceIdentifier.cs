using System;
using System.Runtime.Serialization;

namespace Elmah.EntityContracts
{

    public partial interface IElmahSourceIdentifier
    {

        System.String Source { get; set; }

    }
}


using System;
using System.Runtime.Serialization;

namespace Elmah.EntityContracts
{

    public partial interface IElmahStatusCodeIdentifier
    {

        System.Int32 StatusCode { get; set; }

    }
}


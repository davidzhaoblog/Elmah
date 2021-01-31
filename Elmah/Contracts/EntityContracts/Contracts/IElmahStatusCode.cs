using System;
using System.Runtime.Serialization;

namespace Elmah.EntityContracts
{
    /// <summary>
    /// definition of ElmahStatusCode with parameters of .Net value type.
    /// </summary>
    public partial interface IElmahStatusCode : IElmahStatusCodeIdentifier
    {

        System.String Name { get; set; }

    }
}


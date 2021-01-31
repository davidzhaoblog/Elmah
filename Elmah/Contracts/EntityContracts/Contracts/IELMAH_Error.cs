using System;
using System.Runtime.Serialization;

namespace Elmah.EntityContracts
{
    /// <summary>
    /// definition of ELMAH_Error with parameters of .Net value type.
    /// </summary>
    public partial interface IELMAH_Error : IELMAH_ErrorIdentifier
    {

        System.String Application { get; set; }

        System.String Host { get; set; }

        System.String Type { get; set; }

        System.String Source { get; set; }

        System.String Message { get; set; }

        System.String User { get; set; }

        System.Int32 StatusCode { get; set; }

        System.DateTime TimeUtc { get; set; }

        System.Int32 Sequence { get; set; }

        System.String AllXml { get; set; }

    }
}


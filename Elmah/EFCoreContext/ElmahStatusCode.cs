using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EFCoreContext
{
    public partial class ElmahStatusCode
    {
        public ElmahStatusCode()
        {
            this.ELMAH_Error = new HashSet<ElmahError>();

        }
        public int StatusCode { get; set; }

        public string Name { get; set; }

        public ICollection<ElmahError> ELMAH_Error { get; set; }

    }
}


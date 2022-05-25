using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EFCoreContext
{
    public partial class ElmahHost
    {
        public ElmahHost()
        {
            this.ELMAH_Error = new HashSet<ElmahError>();

        }
        public string Host { get; set; } = null!;

        public Geometry? SpatialLocation____ { get; set; }

        public ICollection<ElmahError> ELMAH_Error { get; set; }

    }
}


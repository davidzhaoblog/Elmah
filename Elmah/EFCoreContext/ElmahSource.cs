using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EFCoreContext
{
    public partial class ElmahSource
    {
        public ElmahSource()
        {
            this.ELMAH_Error = new HashSet<ElmahError>();

        }
        public string Source { get; set; } = null!;

        public ICollection<ElmahError> ELMAH_Error { get; set; }

    }
}


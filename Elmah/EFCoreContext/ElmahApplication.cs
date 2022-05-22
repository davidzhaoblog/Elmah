using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EFCoreContext
{
    public partial class ElmahApplication
    {
        public ElmahApplication()
        {
            this.ELMAH_Error = new HashSet<ElmahError>();

        }
        public string Application { get; set; } = null!;

        public ICollection<ElmahError> ELMAH_Error { get; set; }

    }
}


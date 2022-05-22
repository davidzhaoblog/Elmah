using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EFCoreContext
{
    public partial class ElmahType
    {
        public ElmahType()
        {
            this.ELMAH_Error = new HashSet<ElmahError>();

        }
        public string Type { get; set; } = null!;

        public ICollection<ElmahError> ELMAH_Error { get; set; }

    }
}


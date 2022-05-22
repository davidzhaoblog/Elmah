using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EFCoreContext
{
    public partial class ElmahUser
    {
        public ElmahUser()
        {
            this.ELMAH_Error = new HashSet<ElmahError>();

        }
        public string User { get; set; } = null!;

        public ICollection<ElmahError> ELMAH_Error { get; set; }

    }
}


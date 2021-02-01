using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EntityFrameworkContext
{
    public partial class ElmahApplication
    {
        public ElmahApplication ()
        {
            this.ELMAH_Error = new HashSet<ELMAH_Error>();

        }

        public System.String Application { get; set; }

        public ICollection<ELMAH_Error> ELMAH_Error { get; set; }

    }
}


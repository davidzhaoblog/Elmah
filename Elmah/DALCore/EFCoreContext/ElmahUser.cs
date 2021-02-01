using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EntityFrameworkContext
{
    public partial class ElmahUser
    {
        public ElmahUser ()
        {
            this.ELMAH_Error = new HashSet<ELMAH_Error>();

        }

        public System.String User { get; set; }

        public ICollection<ELMAH_Error> ELMAH_Error { get; set; }

    }
}


using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EntityFrameworkContext
{
    public partial class ElmahType
    {
        public ElmahType ()
        {
            this.ELMAH_Error = new HashSet<ELMAH_Error>();

        }

        public System.String Type { get; set; }

        public ICollection<ELMAH_Error> ELMAH_Error { get; set; }

    }
}


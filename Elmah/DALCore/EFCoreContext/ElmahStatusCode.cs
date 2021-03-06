using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EntityFrameworkContext
{
    public partial class ElmahStatusCode
    {
        public ElmahStatusCode ()
        {
            this.ELMAH_Error = new HashSet<ELMAH_Error>();

        }

        public System.Int32 StatusCode { get; set; }

        public System.String Name { get; set; }

        public ICollection<ELMAH_Error> ELMAH_Error { get; set; }

    }
}


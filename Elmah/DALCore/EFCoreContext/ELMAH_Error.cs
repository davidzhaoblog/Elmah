using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EntityFrameworkContext
{
    public partial class ELMAH_Error
    {
        public ELMAH_Error ()
        {

        }

        public System.Guid ErrorId { get; set; }

        public System.String Application { get; set; }

        public System.String Host { get; set; }

        public System.String Type { get; set; }

        public System.String Source { get; set; }

        public System.String Message { get; set; }

        public System.String User { get; set; }

        public System.Int32 StatusCode { get; set; }

        public System.DateTime TimeUtc { get; set; }

        public System.Int32 Sequence { get; set; }

        public System.String AllXml { get; set; }

        public ElmahApplication ElmahApplication { get; set; }

        public ElmahHost ElmahHost { get; set; }

        public ElmahSource ElmahSource { get; set; }

        public ElmahStatusCode ElmahStatusCode { get; set; }

        public ElmahType ElmahType { get; set; }

        public ElmahUser ElmahUser { get; set; }

    }
}


using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Elmah.EFCoreContext
{
    public partial class ElmahError
    {
        public ElmahError()
        {

        }
        public System.Guid? ErrorId { get; set; }

        public string Application { get; set; } = null!;

        public string Host { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Source { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string User { get; set; } = null!;

        public int StatusCode { get; set; }

        public System.DateTime TimeUtc { get; set; }

        public int Sequence { get; set; }

        public string AllXml { get; set; } = null!;

        public ElmahApplication ElmahApplication { get; set; } = null!;

        public ElmahHost ElmahHost { get; set; } = null!;

        public ElmahSource ElmahSource { get; set; } = null!;

        public ElmahStatusCode ElmahStatusCode { get; set; } = null!;

        public ElmahType ElmahType { get; set; } = null!;

        public ElmahUser ElmahUser { get; set; } = null!;

    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.WebApi
{
    public class Response
    {
        public long RequesterEntityID { get; set; }
        public string RequesterShortGuid { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus Status { get; set; }
        public string BusinessLogicLayerRequestID { get; set; }
        public Dictionary<string, string> ErrorMessage { get; set; } = new Dictionary<string, string>();
    }

    public class Response<TModel>: Response
    {
        public TModel Message { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ViewModels
{
    public class ResponseStatus<TOptions>: ResponseStatus
    {
        public TOptions Option { get; set; }
    }

    public class ResponseStatus
    {
        public Framework.Services.BusinessLogicLayerRequestTypes RequestType { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus Status { get; set; }
        public string StatusMessage { get; set; }
    }
}


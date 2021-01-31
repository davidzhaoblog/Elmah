using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.WebApi
{
    public class Request
    {
        public long RequesterEntityID { get; set; }
        public string RequesterShortGuid { get; set; }
        public string BusinessLogicLayerRequestID { get; set; }

        public T GetResponse<T>()
            where T: Response, new()
        {
            return new T { RequesterEntityID = RequesterEntityID, RequesterShortGuid = RequesterShortGuid, BusinessLogicLayerRequestID = BusinessLogicLayerRequestID };
        }
    }
}


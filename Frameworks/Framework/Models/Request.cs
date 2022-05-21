using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class Request<TStatus, TRequestBody>
    {
        public RequestTypes Type { get; set; }
        public TStatus? Status { get; set; }
        public TRequestBody? RequestBody { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class Response<TStatus, TResponseBody>
    {
        public TStatus? Status { get; set; }
        public TResponseBody? ResponseBody { get; set; }
        public string? StatusMessage { get; set; }
    }
}


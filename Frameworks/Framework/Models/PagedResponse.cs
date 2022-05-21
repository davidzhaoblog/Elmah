using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class PagedResponse<TStatus, TRequestBody> : Request<TStatus, TRequestBody>
    {
        public Pagination? Pagination { get; set; }
    }
}


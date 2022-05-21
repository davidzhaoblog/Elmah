using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class PagedRequest<TStatus, TResponseBody> : Response<TStatus, TResponseBody>
    {
        public PaginationResponse? Paging { get; set; }
    }
}


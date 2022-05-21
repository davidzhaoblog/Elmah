using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class PagedRequest<TStatus, TResponseBody> : Framework.Models.Response<TStatus, TResponseBody>
    {
        public Framework.Models.PaginationResponse? Paging { get; set; }
    }
}


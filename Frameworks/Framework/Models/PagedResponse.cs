using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class PagedResponse<TStatus, TRequestBody> : Framework.Models.Request<TStatus, TRequestBody>
    {
        public Framework.Models.Pagination? Pagination { get; set; }
    }
}


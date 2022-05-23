using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahStatusCodeIdModel : IElmahStatusCodeIdentifier
    {
        public int StatusCode { get; set; }

    }
}


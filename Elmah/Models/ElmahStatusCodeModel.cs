using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahStatusCodeModel : IElmahStatusCode
    {
        public int StatusCode { get; set; }
        public string Name { get; set; }

    }
}


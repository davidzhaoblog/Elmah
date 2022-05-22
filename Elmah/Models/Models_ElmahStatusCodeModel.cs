using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahStatusCodeModel : IElmahStatusCode
    {
        public int StatusCode { get; set; }

        public string Name { get; set; }

    }
}


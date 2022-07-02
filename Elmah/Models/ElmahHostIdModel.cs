using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahHostIdModel : IElmahHostIdentifier
    {
        public string Host { get; set; } = String.Empty;
    }
}


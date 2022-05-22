using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahHostIdentifierModel : IElmahHostIdentifier
    {
        public string Host { get; set; } = null!;

    }
}


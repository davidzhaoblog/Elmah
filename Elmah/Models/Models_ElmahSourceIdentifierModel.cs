using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahSourceIdentifierModel : IElmahSourceIdentifier
    {
        public string Source { get; set; } = null!;

    }
}


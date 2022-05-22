using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahApplicationIdentifierModel : IElmahApplicationIdentifier
    {
        public string Application { get; set; } = null!;

    }
}


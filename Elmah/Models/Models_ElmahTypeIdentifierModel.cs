using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahTypeIdentifierModel : IElmahTypeIdentifier
    {
        public string Type { get; set; } = null!;

    }
}


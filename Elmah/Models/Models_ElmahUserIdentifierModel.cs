using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahUserIdentifierModel : IElmahUserIdentifier
    {
        public string User { get; set; } = null!;

    }
}


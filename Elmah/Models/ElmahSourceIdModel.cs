using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahSourceIdModel : IElmahSourceIdentifier
    {
        public string Source { get; set; } = null!;
    }
}


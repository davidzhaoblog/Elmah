using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahTypeIdModel : IElmahTypeIdentifier
    {
        public string Type { get; set; } = String.Empty;
    }
}


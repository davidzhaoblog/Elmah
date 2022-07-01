using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahUserIdModel : IElmahUserIdentifier
    {
        public string User { get; set; } = String.Empty;
    }
}


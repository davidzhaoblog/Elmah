using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahApplicationIdModel : IElmahApplicationIdentifier
    {
        public string Application { get; set; } = null!;

    }
}


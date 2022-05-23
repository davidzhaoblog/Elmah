using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahApplicationModel : IElmahApplication
    {
        public string Application { get; set; } = null!;

    }
}


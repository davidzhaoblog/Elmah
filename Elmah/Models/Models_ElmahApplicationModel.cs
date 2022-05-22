using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahApplicationModel : IElmahApplication
    {
        public string Application { get; set; } = null!;

    }
}


using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahSourceModel : IElmahSource
    {
        public string Source { get; set; } = null!;

    }
}


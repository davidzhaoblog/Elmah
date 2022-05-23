using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahSourceModel : IElmahSource
    {
        public string Source { get; set; } = null!;

    }
}


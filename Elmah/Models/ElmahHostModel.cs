using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahHostModel : IElmahHost
    {
        public string Host { get; set; } = null!;

    }
}


using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahHostModel : IElmahHost
    {
        public string Host { get; set; } = null!;

    }
}


using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahUserModel : IElmahUser
    {
        public string User { get; set; } = null!;

    }
}


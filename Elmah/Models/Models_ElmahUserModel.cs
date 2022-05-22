using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahUserModel : IElmahUser
    {
        public string User { get; set; } = null!;

    }
}


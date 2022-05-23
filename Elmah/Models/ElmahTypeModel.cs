using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahTypeModel : IElmahType
    {
        public string Type { get; set; } = null!;

    }
}


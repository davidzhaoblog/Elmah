using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahTypeModel : IElmahType
    {
        public string Type { get; set; } = null!;

    }
}


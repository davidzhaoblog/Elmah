using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class Models_ElmahErrorIdentifierModel : IElmahErrorIdentifier
    {
        public System.Guid ErrorId { get; set; }

    }
}


using Elmah.ModelContracts;

namespace Elmah.Models
{
    public partial class ElmahErrorIdModel : IElmahErrorIdentifier
    {
        public System.Guid ErrorId { get; set; } = default(System.Guid);
    }
}



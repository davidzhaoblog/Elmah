using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahHostCompositeModel : CompositeModel<ElmahHostDataModel, ElmahHostCompositeModel.__DataOptions__>
    {
        // 4. ListTable = 4,
        public ElmahErrorDataModel.DefaultView[]? ElmahErrors_Via_Host { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 4. ListTable
            ElmahErrors_Via_Host,

        }
    }
}


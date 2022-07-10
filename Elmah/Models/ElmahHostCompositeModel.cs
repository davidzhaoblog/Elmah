using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahHostCompositeModel : CompositeModel<ElmahHostModel, ElmahHostCompositeModel.__DataOptions__>
    {
        // 4. ListTable = 4,
        public ElmahErrorModel.DefaultView[]? ElmahErrors { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 4. ListTable
            ElmahErrors,

        }
    }
}


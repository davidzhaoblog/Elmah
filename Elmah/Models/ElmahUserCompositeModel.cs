using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahUserCompositeModel : CompositeModel<ElmahUserModel, ElmahUserCompositeModel.__DataOptions__>
    {
        // 4. ListTable = 4,
        public ElmahErrorModel.DefaultView[]? ElmahErrors_Via_User { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 4. ListTable
            ElmahErrors_Via_User,

        }
    }
}


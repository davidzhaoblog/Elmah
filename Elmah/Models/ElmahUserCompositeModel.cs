using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahUserCompositeModel : CompositeModel<ElmahUserModel, ElmahUserCompositeDataOptions>
    {
        // 4. ListTable = 4,
        public ElmahErrorModel.DefaultView[]? ElmahErrors { get; set; }
    }
}


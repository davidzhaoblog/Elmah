using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahApplicationCompositeModel : CompositeModel<ElmahApplicationModel, ElmahApplicationCompositeDataOptions>
    {
        // 4. ListTable = 4,
        public ElmahErrorModel.DefaultView[]? ElmahErrors { get; set; }
    }
}


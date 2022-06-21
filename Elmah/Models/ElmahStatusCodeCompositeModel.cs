using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahStatusCodeCompositeModel : CompositeModel<ElmahStatusCodeModel, ElmahStatusCodeCompositeDataOptions>
    {
        // 4. ListTable = 4,
        public ElmahErrorModel.DefaultView[]? ElmahErrors { get; set; }
    }
}


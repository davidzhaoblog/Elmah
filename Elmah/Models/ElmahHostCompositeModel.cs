using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahHostCompositeModel : CompositeModel<ElmahHostModel, ElmahHostCompositeDataOptions>
    {
        // 4. ListTable = 4,
        public ElmahErrorModel.DefaultView[]? ElmahErrors { get; set; }
    }
}


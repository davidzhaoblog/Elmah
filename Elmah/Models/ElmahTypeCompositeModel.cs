using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahTypeCompositeModel : CompositeModel<ElmahTypeModel, ElmahTypeCompositeDataOptions>
    {
        // 4. ListTable = 4,
        public ElmahErrorModel.DefaultView[]? ElmahErrors { get; set; }
    }
}


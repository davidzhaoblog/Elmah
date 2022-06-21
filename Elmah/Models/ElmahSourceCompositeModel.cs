using Framework.Models;
namespace Elmah.Models
{
    public partial class ElmahSourceCompositeModel : CompositeModel<ElmahSourceModel, ElmahSourceCompositeDataOptions>
    {
        // 4. ListTable = 4,
        public ElmahErrorModel.DefaultView[]? ElmahErrors { get; set; }
    }
}


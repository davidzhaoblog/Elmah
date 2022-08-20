using Elmah.Resx;
using Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Elmah.MvcWebApp.Models
{
    public class OrderBysListHelper
    {
        private readonly Elmah.Resx.IUIStrings _localizor;

        public OrderBysListHelper(Elmah.Resx.IUIStrings localizor)
        {
            _localizor = localizor;
        }
        public List<NameValuePair> GetElmahErrorOrderBys()
        {
            return new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair{ Name = String.Format("{0} A-Z", _localizor.Get("TimeUtc")), Value = "TimeUtc~ASC" },
                new Framework.Models.NameValuePair{ Name = String.Format("{0} Z-A", _localizor.Get("TimeUtc")), Value = "TimeUtc~DESC" },
            });
        }
        public string GetDefaultElmahErrorOrderBys()
        {
            return GetElmahErrorOrderBys().First().Value;
        }
    }
}

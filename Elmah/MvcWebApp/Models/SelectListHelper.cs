using Elmah.Resx;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Elmah.MvcWebApp.Models
{
    public class SelectListHelper
    {
        private readonly IUIStrings _localizor;

        public SelectListHelper(IUIStrings localizor)
        {
            _localizor = localizor;
        }

        public List<SelectListItem> GetDefaultPageSizeList()
        {
            var format_ItemsPerPage = _localizor.Get("Format_ItemsPerPage");

            return new List<SelectListItem>(new[] {
                new SelectListItem{ Text = string.Format(format_ItemsPerPage, 10), Value = "10", Selected=true },
                new SelectListItem{ Text = string.Format(format_ItemsPerPage, 25), Value = "25" },
                new SelectListItem{ Text = string.Format(format_ItemsPerPage, 50), Value = "50" },
                new SelectListItem{ Text = string.Format(format_ItemsPerPage, 100), Value = "100" },
            });
        }
    }
}


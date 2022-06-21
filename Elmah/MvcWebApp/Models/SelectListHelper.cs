using Elmah.Resx;
using Framework.Models;
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

        public List<SelectListItem> GetTextSearchTypeList()
        {
            var format_ItemsPerPage = _localizor.Get("Format_ItemsPerPage");

            return new List<SelectListItem>(new[] {
                new SelectListItem{ Text = _localizor.Get(TextSearchTypes.Contains.ToString()), Value = TextSearchTypes.Contains.ToString(), Selected=true },
                new SelectListItem{ Text = _localizor.Get(TextSearchTypes.StartsWith.ToString()), Value = TextSearchTypes.StartsWith.ToString() },
                new SelectListItem{ Text = _localizor.Get(TextSearchTypes.EndsWith.ToString()), Value = TextSearchTypes.EndsWith.ToString()},
            });
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

        public List<SelectListItem> GetDefaultPredefinedDateTimeRange()
        {
            return new List<SelectListItem>(new[] {
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Custom"), Value = "Custom" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastFiveYears"), Value = "LastFiveYears" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastYear"), Value = "LastYear" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastSixMonths"), Value = "LastSixMonths" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastThreeMonths"), Value = "LastThreeMonths" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastMonth"), Value = "LastMonth" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_LastWeek"), Value = "LastWeek" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Yesterday"), Value = "Yesterday" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Today"), Value = "Today" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Tomorrow"), Value = "Tomorrow" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_ThisWeek"), Value = "ThisWeek" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_ThisMonth"), Value = "ThisMonth" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_ThisYear"), Value = "ThisYear" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextWeek"), Value = "NextWeek" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextMonth"), Value = "NextMonth" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextThreeMonths"), Value = "NextThreeMonths" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextSixMonths"), Value = "NextSixMonths" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextYear"), Value = "NextYear" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextFiveYears"), Value = "NextFiveYears" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_NextTenYears"), Value = "NextTenYears" },
            });
        }
    }
}


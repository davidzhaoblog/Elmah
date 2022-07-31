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

        public List<SelectListItem> GetDefaultTrueFalseBooleanSelectList()
        {
            return new List<SelectListItem>(new[] {
                new SelectListItem{ Text = _localizor.Get("All"), Value = "", Selected=true },
                new SelectListItem{ Text = _localizor.Get("True"), Value = "True"  },
                new SelectListItem{ Text = _localizor.Get("False"), Value = "False"  },
            });
        }

        public List<SelectListItem> GetTextSearchTypeList()
        {
            return new List<SelectListItem>(new[] {
                new SelectListItem{ Text = _localizor.Get(TextSearchTypes.Contains.ToString()), Value = "", Selected=true },
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

        public List<SelectListItem> GetDefaultPredefinedDateTimeRange(bool past = true, bool future = false)
        {
            var result = new List<SelectListItem>(new[] {
                new SelectListItem{ Text = _localizor.Get("AllTime"), Value = "AllTime" },
                new SelectListItem{ Text = _localizor.Get("PreDefinedDateTimeRanges_Custom"), Value = "Custom" } });

            if (future)
            {
                result.AddRange(new[] {
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_NextTenYears"), Value = "NextTenYears" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_NextFiveYears"), Value = "NextFiveYears" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_NextYear"), Value = "NextYear" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_NextSixMonths"), Value = "NextSixMonths" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_NextThreeMonths"), Value = "NextThreeMonths" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_NextMonth"), Value = "NextMonth" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_NextWeek"), Value = "NextWeek" },
                new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_Tomorrow"), Value = "Tomorrow" },
                });
            }

            result.AddRange(new[] {
                //new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_ThisYear"), Value = "ThisYear" },
                //new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_ThisMonth"), Value = "ThisMonth" },
                //new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_ThisWeek"), Value = "ThisWeek" },
                new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_Today"), Value = "Today" },
                });

            if (past)
            {
                result.AddRange(new[] {
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_Yesterday"), Value = "Yesterday" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_LastWeek"), Value = "LastWeek" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_LastMonth"), Value = "LastMonth" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_LastThreeMonths"), Value = "LastThreeMonths" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_LastSixMonths"), Value = "LastSixMonths" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_LastYear"), Value = "LastYear" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_LastFiveYears"), Value = "LastFiveYears" },
                    new SelectListItem { Text = _localizor.Get("PreDefinedDateTimeRanges_LastTenYears"), Value = "LastTenYears" },
                });
            }

            return result;
        }

        public SelectList GetSelectList(List<NameValuePair>? nameValuePairs)
        {
            if (nameValuePairs == null)
                return new SelectList(Enumerable.Empty<SelectListItem>());
            return new SelectList(nameValuePairs, nameof(NameValuePair.Value), nameof(NameValuePair.Name));
        }
    }
}


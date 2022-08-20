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
        
        public List<Framework.Models.NameValuePair> GetDefaultTrueFalseBooleanSelectList()
        {
            return new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair{ Name = _localizor.Get("All"), Value = "", Selected=true },
                new Framework.Models.NameValuePair{ Name = _localizor.Get("True"), Value = "True"  },
                new Framework.Models.NameValuePair{ Name = _localizor.Get("False"), Value = "False"  },
            });
        }

        public List<Framework.Models.NameValuePair> GetTextSearchTypeList()
        {
            return new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair{ Name = _localizor.Get(TextSearchTypes.Contains.ToString()), Value = "", Selected=true },
                new Framework.Models.NameValuePair{ Name = _localizor.Get(TextSearchTypes.StartsWith.ToString()), Value = TextSearchTypes.StartsWith.ToString() },
                new Framework.Models.NameValuePair{ Name = _localizor.Get(TextSearchTypes.EndsWith.ToString()), Value = TextSearchTypes.EndsWith.ToString()},
            });
        }

        public List<Framework.Models.NameValuePair> GetDefaultPageSizeList()
        {
            var format_ItemsPerPage = _localizor.Get("Format_ItemsPerPage");

            return new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair{ Name = string.Format(format_ItemsPerPage, 10), Value = "10", Selected=true },
                new Framework.Models.NameValuePair{ Name = string.Format(format_ItemsPerPage, 25), Value = "25" },
                new Framework.Models.NameValuePair{ Name = string.Format(format_ItemsPerPage, 50), Value = "50" },
                new Framework.Models.NameValuePair{ Name = string.Format(format_ItemsPerPage, 100), Value = "100" },
            });
        }

        public List<Framework.Models.NameValuePair> GetDefaultPredefinedDateTimeRange(bool past = true, bool future = false)
        {
            var result = new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair{ Name = _localizor.Get("AllTime"), Value = "AllTime" },
                new Framework.Models.NameValuePair{ Name = _localizor.Get("PreDefinedDateTimeRanges_Custom"), Value = "Custom" } });

            if (future)
            {
                result.AddRange(new[] {
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextTenYears"), Value = "NextTenYears" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextFiveYears"), Value = "NextFiveYears" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextYear"), Value = "NextYear" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextSixMonths"), Value = "NextSixMonths" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextThreeMonths"), Value = "NextThreeMonths" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextMonth"), Value = "NextMonth" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_NextWeek"), Value = "NextWeek" },
                new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Tomorrow"), Value = "Tomorrow" },
                });
            }

            result.AddRange(new[] {
                //new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisYear"), Value = "ThisYear" },
                //new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisMonth"), Value = "ThisMonth" },
                //new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_ThisWeek"), Value = "ThisWeek" },
                new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Today"), Value = "Today" },
                });

            if (past)
            {
                result.AddRange(new[] {
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_Yesterday"), Value = "Yesterday" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastWeek"), Value = "LastWeek" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastMonth"), Value = "LastMonth" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastThreeMonths"), Value = "LastThreeMonths" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastSixMonths"), Value = "LastSixMonths" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastYear"), Value = "LastYear" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastFiveYears"), Value = "LastFiveYears" },
                    new Framework.Models.NameValuePair { Name = _localizor.Get("PreDefinedDateTimeRanges_LastTenYears"), Value = "LastTenYears" },
                });
            }

            return result;
        }

        public SelectList GetSelectList(List<NameValuePair>? nameValuePairs)
        {
            if (nameValuePairs == null)
                return new SelectList(Enumerable.Empty<Framework.Models.NameValuePair>());
            return new SelectList(nameValuePairs, nameof(Framework.Models.NameValuePair.Value), nameof(Framework.Models.NameValuePair.Name));
        }
    }
}


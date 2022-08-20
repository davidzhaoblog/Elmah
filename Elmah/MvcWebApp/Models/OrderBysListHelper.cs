using Framework.Models;
using Elmah.Resx;

namespace Elmah.MvcWebApp.Models
{
    public class OrderBysListHelper
    {
        private readonly IUIStrings _localizor;

        public OrderBysListHelper(IUIStrings localizor)
        {
            _localizor = localizor;
        }

        public List<NameValuePair> GetElmahErrorOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} A-Z", _localizor.Get("TimeUtc")), Value = "TimeUtc~ASC" },
                new NameValuePair { Name = string.Format("{0} Z-A", _localizor.Get("TimeUtc")), Value = "TimeUtc~DESC" },
            });
        }
        public string GetDefaultElmahErrorOrderBys()
        {
            return GetElmahErrorOrderBys().First().Value;
        }

        public List<NameValuePair> GetElmahApplicationOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} A-Z", _localizor.Get("Application")), Value = "Application~ASC" },
                new NameValuePair { Name = string.Format("{0} Z-A", _localizor.Get("Application")), Value = "Application~DESC" },
            });
        }
        public string GetDefaultElmahApplicationOrderBys()
        {
            return GetElmahApplicationOrderBys().First().Value;
        }

        public List<NameValuePair> GetElmahHostOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} A-Z", _localizor.Get("Host")), Value = "Host~ASC" },
                new NameValuePair { Name = string.Format("{0} Z-A", _localizor.Get("Host")), Value = "Host~DESC" },
            });
        }
        public string GetDefaultElmahHostOrderBys()
        {
            return GetElmahHostOrderBys().First().Value;
        }

        public List<NameValuePair> GetElmahSourceOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} A-Z", _localizor.Get("Source")), Value = "Source~ASC" },
                new NameValuePair { Name = string.Format("{0} Z-A", _localizor.Get("Source")), Value = "Source~DESC" },
            });
        }
        public string GetDefaultElmahSourceOrderBys()
        {
            return GetElmahSourceOrderBys().First().Value;
        }

        public List<NameValuePair> GetElmahStatusCodeOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} A-Z", _localizor.Get("Name")), Value = "Name~ASC" },
                new NameValuePair { Name = string.Format("{0} Z-A", _localizor.Get("Name")), Value = "Name~DESC" },
            });
        }
        public string GetDefaultElmahStatusCodeOrderBys()
        {
            return GetElmahStatusCodeOrderBys().First().Value;
        }

        public List<NameValuePair> GetElmahTypeOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} A-Z", _localizor.Get("Type")), Value = "Type~ASC" },
                new NameValuePair { Name = string.Format("{0} Z-A", _localizor.Get("Type")), Value = "Type~DESC" },
            });
        }
        public string GetDefaultElmahTypeOrderBys()
        {
            return GetElmahTypeOrderBys().First().Value;
        }

        public List<NameValuePair> GetElmahUserOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} A-Z", _localizor.Get("User")), Value = "User~ASC" },
                new NameValuePair { Name = string.Format("{0} Z-A", _localizor.Get("User")), Value = "User~DESC" },
            });
        }
        public string GetDefaultElmahUserOrderBys()
        {
            return GetElmahUserOrderBys().First().Value;
        }

    }
}


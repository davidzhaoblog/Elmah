using System;
using System.Collections.Generic;

namespace Framework.Xaml
{
    public class DomainModel : Framework.Models.PropertyChangedNotifier
    {
        #region Properties

        private string m_Key;
        public string Key
        {
            get { return m_Key; }
            protected set
            {
                Set(nameof(Key), ref m_Key, value);
            }
        }

        private string m_Code;
        /// <summary>
        /// For Industry, it is the IndustrySubClassification.Code, e.g. Educational services->Elementary and secondary schools: 6111
        /// </summary>
        public string Code
        {
            get { return m_Code; }
            protected set
            {
                Set(nameof(Code), ref m_Code, value);
            }
        }

        private Type m_DashboardPage;
        public Type ControllerPage
        {
            get { return m_DashboardPage; }
            protected set
            {
                Set(nameof(ControllerPage), ref m_DashboardPage, value);
            }
        }

        private Type m_DashboardWidget;
        public Type ControllerWidget
        {
            get { return m_DashboardWidget; }
            protected set
            {
                Set(nameof(ControllerWidget), ref m_DashboardWidget, value);
            }
        }

        private Dictionary<string, Type> m_Routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes
        {
            get { return m_Routes; }
            protected set
            {
                Set(nameof(Routes), ref m_Routes, value);
            }
        }

        #endregion Properties

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <param name="hasParameter">this is a AppShell bug, only relative path when has parameters</param>
        public void AddRoute(string key, Type page, bool hasParameter)
        {
            string routeKey = BuildRouteKey(key, hasParameter);
            Routes.Add(routeKey, page);
        }

        public static string BuildRouteKey(string key, bool hasParameter)
        {
            return string.Format("{1}{0}", key, hasParameter ? "" : "//");
        }

        public void AddRouteWithDomanKey(string subkey, Type page, bool hasParameter)
        {
            string routeKey = BuildRouteKeyWithDomanKey(subkey, hasParameter);
            Routes.Add(routeKey, page);
        }

        public string BuildRouteKeyWithDomanKey(string subkey, bool hasParameter)
        {
            return string.Format("{2}{0}_{1}", Key, subkey, hasParameter ? "" : "//");
        }

        public void AddRelativeRoute(string relativeKey, Type page, bool hasParameter)
        {
            string routeKey = BuildRelativeRouteKey(relativeKey, hasParameter);
            Routes.Add(routeKey, page);
        }

        public string BuildRelativeRouteKey(string relativeKey, bool hasParameter)
        {
            return string.Format("{0}/{1}", Key, relativeKey);
            //return string.Format("{2}{0}/{1}", Key, relativeKey, hasParameter ? "" : "//");
        }

        public static DomainModel Create(string key, string code, Type controllerPage, Type controllerWidget)
        {
            var retval = new DomainModel
            {
                ControllerPage = controllerPage, ControllerWidget = controllerWidget, Key = key, Code = code
            };

            return retval;
        }
    }
}


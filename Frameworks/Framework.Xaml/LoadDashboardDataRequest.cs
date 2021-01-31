using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public class LoadDashboardDataRequest
    {
        public string SelectedTabItem { get; set; }

        public Framework.Xaml.DashboardVMUsageOptions DashboardVMUsageOption { get; set; }
        /// <summary>
        /// key is property name or type name, e.g. identifier
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }
    }
}


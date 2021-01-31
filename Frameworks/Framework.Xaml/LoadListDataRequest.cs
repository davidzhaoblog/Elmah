using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public class LoadListDataRequest
    {
        public Framework.Xaml.ListItemViewModes ListItemViewMode { get; set; }
        public bool? BindToGroupedResults { get; set; }
        public string OrderByPropertyName { get; set; }
        public Framework.Queries.QueryOrderDirections? OrderByDirection { get; set; }
        /// <summary>
        /// key is property name
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }
        public Action ActionWhenLaunch { get; set; }
    }
}


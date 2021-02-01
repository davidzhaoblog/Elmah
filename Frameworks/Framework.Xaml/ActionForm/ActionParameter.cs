using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml.ActionForm
{
    /// <summary>
    /// should rename to NavigationActionParameter
    /// </summary>
    public class ActionParameter
    {
        public string Domain { get; set; }
        public string Page { get; set; }
        public Action SendMessage { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}


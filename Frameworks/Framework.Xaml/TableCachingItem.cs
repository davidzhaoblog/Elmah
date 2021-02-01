using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public struct TableCachingItem
    {
        public string TableName;
        public DateTime SyncDateTime;
        public bool ShouldRefresh;
        public bool UIListBindToGroupedResults;
        public List<Framework.Queries.QueryOrderBySetting> UIListQueryOrderBySettings;
    }
}


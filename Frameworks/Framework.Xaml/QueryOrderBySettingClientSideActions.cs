using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public class QueryOrderBySettingClientSideActions<TGroupResult,TItem, TSQLiteItem>: QueryOrderBySettingClientSideActions<TGroupResult, TItem>
    {
        public Func<TableQuery<TSQLiteItem>, Framework.Queries.QueryOrderDirections, TableQuery<TSQLiteItem>> GetSQLiteSortTableQuery;
    }
    public class QueryOrderBySettingClientSideActions<TGroupResult, TItem>
    {
        public Func<List<TItem>, List<TGroupResult>> GetGroupResults;
    }
}


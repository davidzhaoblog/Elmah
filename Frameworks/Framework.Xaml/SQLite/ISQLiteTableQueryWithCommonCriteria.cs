using Framework.Queries;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Xaml.SQLite
{
    public interface ISQLiteTableQueryWithCommonCriteria<TSQLiteItem, TCommonCriteria, TItem>
        where TSQLiteItem : TItem, new()
    {
        Task<int> Count(TCommonCriteria criteria);
        Task<List<TItem>> Load(TCommonCriteria criteria, Framework.Queries.QueryPagingSetting queryPagingSetting, Framework.Queries.QueryOrderBySetting queryOrderBySetting, Func<TableQuery<TSQLiteItem>, Framework.Queries.QueryOrderDirections, TableQuery<TSQLiteItem>> sortFunction);
    }
}

